using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using ResumeBuilder.Core.Schema.v1;
using ResumeBuilder.Core.Template;

namespace ResumeBuilder.Cli.Commands
{
  public class ExportCommand : Command
  {
    public ExportCommand() : base("export", "Exports your resume locally in a stylized HTML or PDF format.")
    {
      AddOption(FileOption);
      AddOption(TemplateOption);
      AddOption(FormatOption);

      Handler = CommandHandler.Create<FileInfo, FileInfo, Format, IConsole>(async (file, template, format, console) =>
      {
        var resume = await GetResume(file);
        var theme = await GetTheme(template);

        var engine = new FluidTemplateEngine();
        var result = await engine.RenderAsync(theme, resume);
        var output = $"resume.{format.ToString()}";

        if (format == Format.html)
        {
          await using var htmlStream = new FileStream(output, FileMode.Create);
          var bytes = Encoding.UTF8.GetBytes(result);
          await htmlStream.WriteAsync(bytes);
        }

        if (format == Format.pdf)
        {
          var fetcher = new BrowserFetcher(new BrowserFetcherOptions
          {
            Path = AppContext.BaseDirectory
          });

          await fetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
          var browser = await Puppeteer.LaunchAsync(new LaunchOptions
          {
            ExecutablePath = fetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision),
            Headless = true
          });

          var page = await browser.NewPageAsync();
          await page.EmulateMediaTypeAsync(MediaType.Print);

          var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(result));
          await page.GoToAsync($"data:text/html;base64,{encoded}", WaitUntilNavigation.Networkidle0);

          await page.PdfAsync(output, new PdfOptions
          {
            Format = PaperFormat.Letter,
            PrintBackground = true
          });
        }

        console.Out.WriteLine("Done...");
      });
    }

    private static async Task<string> GetTheme(FileInfo template)
    {
      if (!template.Exists)
        throw new FileNotFoundException("Template not found", template.FullName);

      using var templateReader = template.OpenText();
      var theme = await templateReader.ReadToEndAsync();

      return theme;
    }

    private static async Task<JsonResumeV1> GetResume(FileInfo file)
    {
      if (!file.Exists)
        throw new FileNotFoundException("Resume not found", file.FullName);

      await using var stream = file.OpenRead();
      using var reader = new StreamReader(stream, Encoding.UTF8);
      var resumeJson = await reader.ReadToEndAsync();
      var resume = JsonResumeV1.FromJson(resumeJson);

      return resume;
    }

    private static Option FileOption => new(new[] {"-f", "--file"}, "Path to a resume.json file")
    {
      Argument = new Argument<FileInfo>(() => new FileInfo(Defaults.ResumePath))
      {
        Name = "filepath",
        Arity = ArgumentArity.ZeroOrOne
      },
      IsRequired = true
    };

    private static Option TemplateOption => new(new[] {"-t", "--template"}, "Path to a template to render the file.")
    {
      Argument = new Argument<FileInfo>(() => new FileInfo(Defaults.Theme))
      {
        Name = "filepath",
        Arity = ArgumentArity.ZeroOrOne
      },
      IsRequired = false
    };

    private static Option FormatOption => new(new[] {"--format"}, "Format in 'html' or 'pdf'.")
    {
      Argument = new Argument<Format>(() => Format.pdf)
      {
        Name = "format",
        Arity = ArgumentArity.ZeroOrOne
      },
      IsRequired = false
    };

    private enum Format
    {
      pdf,
      html
    }
  }
}
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using ResumeBuilder.Cli.Templates;
using ResumeBuilder.Cli.Templates.@default;
using ResumeBuilder.Core.Exporters;
using ResumeBuilder.Core.Schema.v1;
using ResumeBuilder.Core.Template;

namespace ResumeBuilder.Cli.Commands
{
  public class ExportCommand : Command
  {
    public ExportCommand() :
      base("export", "Exports your resume locally in a stylized HTML or PDF format.")
    {
      AddArgument(FileArgument);
      AddOption(TemplateOption);
      AddOption(FormatOption);

      Handler = CommandHandler.Create<FileInfo, string, Format, IConsole>(async (file, template, format, console) =>
      {
        var resume = await GetResume(file);
        var engine = new FluidTemplateBuilder()
          .AddFromAssembly(typeof(DefaultTemplate).Assembly)
          .Build();
        
        var result = await engine.RenderAsync(template, resume);
        var output = $"resume.{format.ToString()}";

        switch (format)
        {
          case Format.html:
          {
            var htmlExporter = new HtmlExporter();
            await htmlExporter.ExportAsync(result, output);
            break;
          }
          case Format.pdf:
          {
            var pdfExporter = new PdfExporter();
            await pdfExporter.ExportAsync(result, output);
            break;
          }
          default:
            throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }

        console.Out.WriteLine("Done...");
      });
    }

    // private static async Task<string> GetTheme(FileInfo template)
    // {
    //   if (!template.Exists)
    //     throw new FileNotFoundException("Template not found", template.FullName);
    //
    //   using var templateReader = template.OpenText();
    //   var theme = await templateReader.ReadToEndAsync();
    //
    //   return theme;
    // }

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

    private static Argument<FileInfo> FileArgument => new("file");

    // private static Option FileOption => new(new[] {"-f", "--file"}, "Path to a resume.json file")
    // {
    //   Argument = new Argument<FileInfo>(() => new FileInfo(Defaults.ResumePath))
    //   {
    //     Name = "filepath",
    //     Arity = ArgumentArity.ZeroOrOne
    //   },
    //   IsRequired = true
    // };

    private static Option TemplateOption => new(new[] {"-t", "--template"}, "Path to a template to render the file.")
    {
      Argument = new Argument<string>(() => Defaults.Theme)
      {
        Name = "theme",
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
  }

  public enum Format
  {
    pdf,
    html
  }
}
using System;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace ResumeBuilder.Core.Exporters
{
  public class PdfExporter : IExporter
  {
    public async Task ExportAsync(string content, string filePath)
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

      var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
      await page.GoToAsync($"data:text/html;base64,{encoded}", WaitUntilNavigation.Networkidle0);
      
      var pdfOptions = new PdfOptions
      {
        Format = PaperFormat.Letter,
        PrintBackground = true,
        Scale = 0.8m
      };
      await page.PdfAsync(filePath, pdfOptions);
    }
  }
}
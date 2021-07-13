using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace ResumeBuilder.Core.Exporters
{
  public class PdfExporter : IExporter
  {
    public async Task ExportAsync(string content, string filePath)
    {
      // Install Chrome before using Playwright.
      // (See: https://github.com/microsoft/playwright-dotnet/issues/1545#issuecomment-865199736)
      Program.Main(new[] {"install", "chromium"});

      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
      {
        Headless = true
      });
      await using var context = await browser.NewContextAsync();

      var page = await context.NewPageAsync();
      await page.EmulateMediaAsync(new PageEmulateMediaOptions {Media = Media.Print});
      var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
      await page.GotoAsync($"data:text/html;base64,{encoded}", new PageGotoOptions {WaitUntil = WaitUntilState.NetworkIdle});

      await page.PdfAsync(new PagePdfOptions
      {
        Path = filePath,
        Format = "Letter",
        PrintBackground = true,
        Scale = 0.8f
      });
    }
  }
}
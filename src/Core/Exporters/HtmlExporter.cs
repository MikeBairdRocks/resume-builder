using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder.Core.Exporters
{
  public class HtmlExporter : IExporter
  {
    public async Task ExportAsync(string content, string filePath)
    {
      await using var htmlStream = new FileStream(filePath, FileMode.Create);
      var bytes = Encoding.UTF8.GetBytes(content);
      await htmlStream.WriteAsync(bytes);
    }
  }
}
using System.Threading.Tasks;

namespace ResumeBuilder.Core.Exporters
{
  public interface IExporter
  {
     Task ExportAsync(string html, string filePath);
  }
}
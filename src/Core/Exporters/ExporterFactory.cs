using System;
using Microsoft.Extensions.DependencyInjection;

namespace ResumeBuilder.Core.Exporters
{
  public class ExporterFactory
  {
    private readonly IServiceProvider _serviceProvider;

    public ExporterFactory(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }
    
    public IExporter GetExporter(ExportFormat format)
    {
      return format switch
      {
        ExportFormat.pdf => _serviceProvider.GetRequiredService<PdfExporter>(),
        ExportFormat.html => _serviceProvider.GetRequiredService<HtmlExporter>(),
        _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
      };
    }
  }
}
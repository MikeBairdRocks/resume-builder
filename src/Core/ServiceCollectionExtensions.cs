using Microsoft.Extensions.DependencyInjection;
using ResumeBuilder.Core.Exporters;
using ResumeBuilder.Core.Schema;
using ResumeBuilder.Core.Template;
using ResumeBuilder.Core.Template.Fluid;

namespace ResumeBuilder.Core
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddResumeBuilder(this IServiceCollection services)
    {
      services.AddScoped<IValidator, Validator>();
      services.AddScoped<ITemplateEngine, FluidTemplateEngine>();
      
      services.AddSingleton<ExporterFactory>();
      services
        .AddScoped<HtmlExporter>()
        .AddScoped<IExporter, HtmlExporter>(s => s.GetService<HtmlExporter>());
      
      services
        .AddScoped<PdfExporter>()
        .AddScoped<IExporter, PdfExporter>(s => s.GetService<PdfExporter>());
      
      return services;
    }
  }
}
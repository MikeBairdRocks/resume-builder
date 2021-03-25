using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ResumeBuilder.Core.Template
{
  public static class TemplateExtensions
  {
    public static async Task<string> GetResourcePathAsString(this Assembly assembly, string resourcePath)
    {
      await using var stream = assembly.GetManifestResourceStream(resourcePath);
      using var reader = new StreamReader(stream);

      var result = await reader.ReadToEndAsync();

      return result;
    }

    public static IReadOnlyCollection<ITemplate> GetTemplatesFromAssembly(this Assembly assembly)
    {
      var templates = assembly.GetTypes()
        .Where(type => typeof(ITemplate).IsAssignableFrom(type) && !type.IsInterface)
        .Select(type => (ITemplate) Activator.CreateInstance(type));

      return templates.ToImmutableList();
    }

    public static async Task<string> GetResourceTemplateAsString<T>(this T template)
      where T : class, ITemplate
    {
      var type = template.GetType();
      var assembly = type.Assembly;
      var resource = $"{type.Namespace}.{template.Template}";

      await using var stream = assembly.GetManifestResourceStream(resource);
      using var reader = new StreamReader(stream);

      var result = await reader.ReadToEndAsync();

      return result;
    }
  }
}
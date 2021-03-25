using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ResumeBuilder.Core.Template
{
  public class FluidTemplateBuilder
  {
    private readonly List<ITemplate> _templates;

    public FluidTemplateBuilder()
    {
      _templates = new List<ITemplate>();
    }
    
    public FluidTemplateBuilder AddFromAssembly(Assembly assembly)
    {
      var templates = assembly.GetTemplatesFromAssembly();

      _templates.AddRange(templates);

      return this;
    }
    
    public FluidTemplateEngine Build()
    {
      return new(_templates);
    }
  }
}
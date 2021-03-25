using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Fluid;
using Fluid.Ast;
using Fluid.Parser;
using ResumeBuilder.Core.Schema.v1;

namespace ResumeBuilder.Core.Template
{
  public class FluidTemplateEngine
  {
    private readonly IReadOnlyList<ITemplate> _templates;

    public FluidTemplateEngine(IReadOnlyList<ITemplate> templates)
    {
      _templates = templates;
    }
    
    public async ValueTask<string> RenderAsync(string templateName, JsonResumeV1 resume)
    {
      if (string.IsNullOrEmpty(templateName))
        throw new ArgumentNullException(nameof(templateName));

      var options = new TemplateOptions();
      options.MemberAccessStrategy.Register<JsonResumeV1>();
      options.MemberAccessStrategy.Register<Award>();
      options.MemberAccessStrategy.Register<Basics>();
      options.MemberAccessStrategy.Register<Education>();
      options.MemberAccessStrategy.Register<Interest>();
      options.MemberAccessStrategy.Register<Language>();
      options.MemberAccessStrategy.Register<Location>();
      options.MemberAccessStrategy.Register<Meta>();
      options.MemberAccessStrategy.Register<Profile>();
      options.MemberAccessStrategy.Register<Project>();
      options.MemberAccessStrategy.Register<Publication>();
      options.MemberAccessStrategy.Register<Reference>();
      options.MemberAccessStrategy.Register<Skill>();
      options.MemberAccessStrategy.Register<Volunteer>();
      options.MemberAccessStrategy.Register<Work>();

      var template = _templates.FirstOrDefault(t => t.Name == templateName);
      if (template == null)
        throw new Exception("template not found");

      var templateFile = await template.GetResourceTemplateAsString();
      
      var parser = new FluidTemplateParser(template);
      if (!parser.TryParse(templateFile, out var liquidTemplate, out var error))
        throw new Exception(error);

      var context = new TemplateContext(resume, options);
      var body = await liquidTemplate.RenderAsync(context);
      
      // If a layout is specified while rendering a template, execute it
      if (context.AmbientValues.TryGetValue("Layout", out var layoutPath))
      {
        var assembly = template.GetType().Assembly;
        var layoutContent = await assembly.GetResourcePathAsString((string)layoutPath);
        
        context.AmbientValues["Body"] = body;
        var layoutTemplate = ParseLiquidFile(layoutContent, parser);

        return await layoutTemplate.RenderAsync(context);
      }

      return body;
    }

    private static IFluidTemplate ParseLiquidFile(string fileContent, FluidTemplateParser parser)
    {
      var statements = new List<Statement>();
      if (!parser.TryParse(fileContent, out var template, out var errors))
        throw new ParseException(errors);

      statements.AddRange(template.Statements);

      return new FluidTemplate(statements);
    }
  }
}
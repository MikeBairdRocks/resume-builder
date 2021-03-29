using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Fluid;
using Fluid.Ast;
using Fluid.Parser;
using Microsoft.Extensions.FileProviders;
using ResumeBuilder.Core.Schema.v1;

namespace ResumeBuilder.Core.Template
{
  public class FluidTemplateEngine : ITemplateEngine
  {
    private readonly IFileProvider _fileProvider;
    //private readonly IReadOnlyList<ITemplate> _templates;

    // public FluidTemplateEngine(IReadOnlyList<ITemplate> templates)
    // {
    //   _templates = templates;
    // }

    public FluidTemplateEngine(IFileProvider fileProvider)
    {
      _fileProvider = fileProvider;
    }
    
    public async ValueTask<string> RenderAsync(string template, JsonResumeV1 resume)
    {
      // if (!template.Exists)
      //   throw new FileNotFoundException("Template not found", template.FullName);
      
      var options = new TemplateOptions
      {
        FileProvider = _fileProvider
      };
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

      var file = _fileProvider.GetFileInfo(template);
      var templateContent = await file.ReadToEndAsync();
      
      var parser = new FluidTemplateParser();
      if (!parser.TryParse(templateContent, out var liquidTemplate, out var error))
        throw new Exception(error);
      
      var context = new TemplateContext(resume, options);
      var body = await liquidTemplate.RenderAsync(context);
      
      // If a layout is specified while rendering a template, execute it
      if (context.AmbientValues.TryGetValue("Layout", out var layoutPath))
      {
        var directory = template.Replace(file.Name, "");
        var path = $"{directory}{layoutPath}";
        var layoutFile = _fileProvider.GetFileInfo(path);
        var layoutContent = await layoutFile.ReadToEndAsync();
        
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
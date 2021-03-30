using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Fluid;
using Fluid.Ast;
using Fluid.Parser;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using ResumeBuilder.Core.Schema.v1;

namespace ResumeBuilder.Core.Template.Fluid
{
  public class FluidTemplateEngine : ITemplateEngine
  {
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<FluidTemplateEngine> _logger;

    public FluidTemplateEngine(IFileProvider fileProvider, ILogger<FluidTemplateEngine> logger)
    {
      _fileProvider = fileProvider;
      _logger = logger;
    }

    public async ValueTask<string> RenderAsync(string template, JsonResumeV1 resume)
    {
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
        var layoutFile = _fileProvider.GetFileInfo((string) layoutPath);
        var layoutContent = await layoutFile.ReadToEndAsync();

        context.AmbientValues["Body"] = body;
        var statements = new List<Statement>();
        if (!parser.TryParse(layoutContent, out var layoutTemplate, out var errors))
          throw new ParseException(errors);

        statements.AddRange(((FluidTemplate) layoutTemplate).Statements);
        var layoutFluidTemplate = new FluidTemplate(statements);

        return await layoutFluidTemplate.RenderAsync(context);
      }

      return body;
    }
  }
}
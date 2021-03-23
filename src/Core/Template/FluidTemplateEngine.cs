using System;
using System.Collections.Generic;
using System.IO;
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
    public async ValueTask<string> RenderAsync(string theme, JsonResumeV1 resume)
    {
      if (string.IsNullOrEmpty(theme))
        throw new ArgumentNullException(nameof(theme));

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

      var parser = new FluidTemplateParser();
      
      if (parser.TryParse(theme, out var template, out var error))
      {
        var context = new TemplateContext(resume, options);
        
        var body = await template.RenderAsync(context);
        
        // If a layout is specified while rendering a template, execute it
        if (context.AmbientValues.TryGetValue("Layout", out var layoutPath))
        {
          context.AmbientValues["Body"] = body;
          var layoutTemplate = ParseLiquidFile((string)layoutPath, parser);

          return await layoutTemplate.RenderAsync(context);
        }

        return body;
      }

      throw new Exception(error);
    }

    private IFluidTemplate ParseLiquidFile(string path, FluidTemplateParser parser)
    {
      var statements = new List<Statement>();
      var fileInfo = new FileInfo(path);

      using var stream = fileInfo.OpenRead();
      using var sr = new StreamReader(stream);
      
      var fileContent = sr.ReadToEnd();
      if (parser.TryParse(fileContent, out var template, out var errors))
      {
        statements.AddRange(template.Statements);

        return new FluidTemplate(statements);
      }
      else
      {
        throw new ParseException(errors);
      }
    }
  }
}
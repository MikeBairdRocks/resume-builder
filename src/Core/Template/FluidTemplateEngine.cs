using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Schema.v1;
using Fluid;
using Fluid.Ast;

namespace Core.Template
{
  public class TemplateEngine
  {
    public ValueTask<string> RenderAsync(string theme, JsonResumeV1 resume)
    {
      if (string.IsNullOrEmpty(theme))
        throw new ArgumentNullException(nameof(theme));

      var options = new TemplateOptions();
      options.MemberAccessStrategy.Register<JsonResumeV1>();
      options.MemberAccessStrategy.Register<Basics>();

      var parser = new FluidParser();
      parser.RegisterExpressionTag("layout", async (pathExpression, writer, encoder, context) =>
      {
        var layoutPath = (await pathExpression.EvaluateAsync(context)).ToStringValue();
        if (!layoutPath.EndsWith(".liquid", StringComparison.OrdinalIgnoreCase))
        {
          layoutPath += ".liquid";
        }

        context.AmbientValues["Layout"] = layoutPath;

        return Completion.Normal;
      });
      
      pasrser

      if (parser.TryParse(theme, out var template, out var error))
      {
        var context = new TemplateContext(resume, options);


        return template.RenderAsync(context);
      }

      throw new Exception(error);
    }
  }
}
using System;
using Fluid;
using Fluid.Ast;

namespace ResumeBuilder.Core.Template
{
  public class FluidTemplateParser : FluidParser
  {
    public FluidTemplateParser(ITemplate template)
    {
      RegisterExpressionTag("layout", async (pathExpression, writer, encoder, context) =>
      {
        var layoutPath = (await pathExpression.EvaluateAsync(context)).ToStringValue();
        if (!layoutPath.EndsWith(".liquid", StringComparison.OrdinalIgnoreCase))
        {
          layoutPath += ".liquid";
        }

        var resource = $"{template.GetType().Namespace}.{layoutPath}";
        context.AmbientValues["Layout"] = resource;

        return Completion.Normal;
      });
      
      RegisterEmptyTag("renderbody", static async (writer, encoder, context) =>
      {
        if (context.AmbientValues.TryGetValue("Body", out var body))
        {
          await writer.WriteAsync((string)body);
        }
        else
        {
          throw new ParseException("Could not render body, Layouts can't be evaluated directly.");
        }

        return Completion.Normal;
      });
    }
  }
}
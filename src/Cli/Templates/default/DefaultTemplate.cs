using ResumeBuilder.Core.Template;

namespace ResumeBuilder.Cli.Templates.@default
{
  public class DefaultTemplate : ITemplate
  {
    public string Name => "default";
    public string Description => "Default Template for Resume Builder.";
    public string Template => "template.liquid";
  }
}
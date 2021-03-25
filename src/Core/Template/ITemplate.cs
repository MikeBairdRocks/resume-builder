namespace ResumeBuilder.Core.Template
{
  public interface ITemplate
  {
    string Name { get; }
    string Description { get; }
    string Template { get; }
  }
}
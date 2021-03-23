using ResumeBuilder.Core.Schema.v1;

namespace ResumeBuilder.Core.Template
{
  public interface ITemplate
  {
    string Name { get; }
    string Description { get; }
    object OnBeforeRender(JsonResumeV1 resume) => resume;
  }
}
using System.Threading.Tasks;
using ResumeBuilder.Core.Schema.v1;

namespace ResumeBuilder.Core.Template
{
  public interface ITemplateEngine
  {
    ValueTask<string> RenderAsync(string template, JsonResumeV1 resume);
  }
}
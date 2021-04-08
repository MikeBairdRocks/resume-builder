using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using ResumeBuilder.Core.Schema.v1;

namespace ResumeBuilder.Core.Schema
{
  public interface IValidator
  {
    Task<(bool IsValid, IList<string> Messages)> ValidateV1(string json);
  }

  public class Validator : IValidator
  {
    private readonly HttpClient _client;

    public Validator() : this(new HttpClient())
    {
    }

    public Validator(HttpClient client)
    {
      _client = client;
    }

    public async Task<(bool IsValid, IList<string> Messages)> ValidateV1(string json)
    {
      var response = await _client.GetAsync(JsonResumeV1.SchemaUrl);
      var resumeSchema = await response.Content.ReadAsStringAsync();
      var schema = JSchema.Parse(resumeSchema);

      var resumeObject = JObject.Parse(json);
      var isValid = resumeObject.IsValid(schema, out IList<string> messages);
      return (isValid, messages);
    }
  }
}
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Json.Schema;
using ResumeBuilder.Core.Schema.v1;

namespace ResumeBuilder.Core.Schema
{
  public class Validator
  {
    private readonly HttpClient _client;

    public Validator() : this(new HttpClient())
    {
    }

    public Validator(HttpClient client)
    {
      _client = client;
    }

    public async Task<ValidationResults> ValidateV1(string json)
    {
      var response = await _client.GetAsync(JsonResumeV1.SchemaUrl);
      var resumeSchema = await response.Content.ReadAsStringAsync();

      var schema = JsonSchema.FromText(resumeSchema);
      var root = JsonDocument.Parse(json).RootElement;
      var validation = schema.Validate(root, ValidationOptions.Default);

      return validation;
    }
  }
}
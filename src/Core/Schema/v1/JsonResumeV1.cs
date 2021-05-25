using System.Text.Json;
using System.Text.Json.Serialization;
using Core;

namespace ResumeBuilder.Core.Schema.v1
{
  public class JsonResumeV1
  {
    public const string SchemaUrl = "https://raw.githubusercontent.com/jsonresume/resume-schema/v1.0.0/schema.json";

    private static JsonSerializerOptions Settings
    {
      get
      {
        var settings = new JsonSerializerOptions
        {
          DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        settings.Converters.Add(new JsonConverterForNullableDateTimeOffset());

        return settings;
      }
    }

    public static JsonResumeV1 FromJson(string json) => JsonSerializer.Deserialize<JsonResumeV1>(json, Settings);
    public string ToJson() => JsonSerializer.Serialize(this, Settings);
    
    /// <summary>
    /// Specify any awards you have received throughout your professional career
    /// </summary>
    [JsonPropertyName("awards")]
    public Award[] Awards { get; set; }

    [JsonPropertyName("basics")]
    public Basics Basics { get; set; }

    [JsonPropertyName("education")]
    public Education[] Education { get; set; }

    [JsonPropertyName("interests")]
    public Interest[] Interests { get; set; }

    /// <summary>
    /// List any other languages you speak
    /// </summary>
    [JsonPropertyName("languages")]
    public Language[] Languages { get; set; }

    /// <summary>
    /// The schema version and any other tooling configuration lives here
    /// </summary>
    [JsonPropertyName("meta")]
    public Meta Meta { get; set; }

    /// <summary>
    /// Specify career projects
    /// </summary>
    [JsonPropertyName("projects")]
    public Project[] Projects { get; set; }

    /// <summary>
    /// Specify your publications through your career
    /// </summary>
    [JsonPropertyName("publications")]
    public Publication[] Publications { get; set; }

    /// <summary>
    /// List references you have received
    /// </summary>
    [JsonPropertyName("references")]
    public Reference[] References { get; set; }

    /// <summary>
    /// List out your professional skill-set
    /// </summary>
    [JsonPropertyName("skills")]
    public Skill[] Skills { get; set; }

    [JsonPropertyName("volunteer")]
    public Volunteer[] Volunteer { get; set; }

    [JsonPropertyName("work")]
    public Work[] Work { get; set; }
  }
}
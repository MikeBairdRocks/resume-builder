using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core
{
  public class JsonResumeV1
  {
    public const string SchemaUrl = "https://raw.githubusercontent.com/jsonresume/resume-schema/v1.0.0/schema.json";

    private static readonly JsonSerializerSettings Settings = new()
    {
      MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
      DateParseHandling = DateParseHandling.None,
      Converters =
      {
        new IsoDateTimeConverter
        {
          DateTimeStyles = DateTimeStyles.AdjustToUniversal,
          DateTimeFormat = "yyyy-MM-dd"
        }
      },
    };

    public static JsonResumeV1 FromJson(StreamReader reader)
    {
      using var jsonReader = new JsonTextReader(reader);
      return new JsonSerializer().Deserialize<JsonResumeV1>(jsonReader);
    }

    public static JsonResumeV1 FromJson(string json) => JsonConvert.DeserializeObject<JsonResumeV1>(json, Settings);
    public string ToJson() => JsonConvert.SerializeObject(this, Settings);
    
    /// <summary>
    /// Specify any awards you have received throughout your professional career
    /// </summary>
    [JsonProperty("awards", NullValueHandling = NullValueHandling.Ignore)]
    public Award[] Awards { get; set; }

    [JsonProperty("basics", NullValueHandling = NullValueHandling.Ignore)]
    public Basics Basics { get; set; }

    [JsonProperty("education", NullValueHandling = NullValueHandling.Ignore)]
    public Education[] Education { get; set; }

    [JsonProperty("interests", NullValueHandling = NullValueHandling.Ignore)]
    public Interest[] Interests { get; set; }

    /// <summary>
    /// List any other languages you speak
    /// </summary>
    [JsonProperty("languages", NullValueHandling = NullValueHandling.Ignore)]
    public Language[] Languages { get; set; }

    /// <summary>
    /// The schema version and any other tooling configuration lives here
    /// </summary>
    [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
    public Meta Meta { get; set; }

    /// <summary>
    /// Specify career projects
    /// </summary>
    [JsonProperty("projects", NullValueHandling = NullValueHandling.Ignore)]
    public Project[] Projects { get; set; }

    /// <summary>
    /// Specify your publications through your career
    /// </summary>
    [JsonProperty("publications", NullValueHandling = NullValueHandling.Ignore)]
    public Publication[] Publications { get; set; }

    /// <summary>
    /// List references you have received
    /// </summary>
    [JsonProperty("references", NullValueHandling = NullValueHandling.Ignore)]
    public Reference[] References { get; set; }

    /// <summary>
    /// List out your professional skill-set
    /// </summary>
    [JsonProperty("skills", NullValueHandling = NullValueHandling.Ignore)]
    public Skill[] Skills { get; set; }

    [JsonProperty("volunteer", NullValueHandling = NullValueHandling.Ignore)]
    public Volunteer[] Volunteer { get; set; }

    [JsonProperty("work", NullValueHandling = NullValueHandling.Ignore)]
    public Work[] Work { get; set; }
  }
}
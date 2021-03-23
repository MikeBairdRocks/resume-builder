using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Skill
  {
    /// <summary>
    /// List some keywords pertaining to this skill
    /// </summary>
    [JsonPropertyName("keywords")]
    public string[] Keywords { get; set; }

    /// <summary>
    /// e.g. Master
    /// </summary>
    [JsonPropertyName("level")]
    public string Level { get; set; }

    /// <summary>
    /// e.g. Web Development
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
  }
}
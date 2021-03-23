using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Interest
  {
    [JsonPropertyName("keywords")]
    public string[] Keywords { get; set; }

    /// <summary>
    /// e.g. Philosophy
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
  }
}
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Profile
  {
    /// <summary>
    /// e.g. Facebook or Twitter
    /// </summary>
    [JsonPropertyName("network")]
    public string Network { get; set; }

    /// <summary>
    /// e.g. http://twitter.example.com/neutralthoughts
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    /// e.g. neutralthoughts
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; }
  }
}
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Publication
  {
    /// <summary>
    /// e.g. The World Wide Web
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// e.g. IEEE, Computer Magazine
    /// </summary>
    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }

    /// <summary>
    /// e.g. 1990-08-01
    /// </summary>
    [JsonPropertyName("releaseDate")]
    public string ReleaseDate { get; set; }

    /// <summary>
    /// Short summary of publication. e.g. Discussion of the World Wide Web, HTTP, HTML.
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// e.g. http://www.computer.org.example.com/csdl/mags/co/1996/10/rx069-abs.html
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }
  }
}
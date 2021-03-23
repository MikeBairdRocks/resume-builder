using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  /// <summary>
  /// The schema version and any other tooling configuration lives here
  /// </summary>
  public class Meta
  {
    /// <summary>
    /// URL (as per RFC 3986) to latest version of this document
    /// </summary>
    [JsonPropertyName("canonical")]
    public string Canonical { get; set; }

    /// <summary>
    /// Using ISO 8601 with YYYY-MM-DDThh:mm:ss
    /// </summary>
    [JsonPropertyName("lastModified")]
    public string LastModified { get; set; }

    /// <summary>
    /// A version field which follows semver - e.g. v1.0.0
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; }
  }
}
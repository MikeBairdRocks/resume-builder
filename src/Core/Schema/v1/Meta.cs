using Newtonsoft.Json;

namespace Core
{
  /// <summary>
  /// The schema version and any other tooling configuration lives here
  /// </summary>
  public class Meta
  {
    /// <summary>
    /// URL (as per RFC 3986) to latest version of this document
    /// </summary>
    [JsonProperty("canonical", NullValueHandling = NullValueHandling.Ignore)]
    public string Canonical { get; set; }

    /// <summary>
    /// Using ISO 8601 with YYYY-MM-DDThh:mm:ss
    /// </summary>
    [JsonProperty("lastModified", NullValueHandling = NullValueHandling.Ignore)]
    public string LastModified { get; set; }

    /// <summary>
    /// A version field which follows semver - e.g. v1.0.0
    /// </summary>
    [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; }
  }
}
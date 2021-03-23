using Newtonsoft.Json;

namespace Core
{
  public class Profile
  {
    /// <summary>
    /// e.g. Facebook or Twitter
    /// </summary>
    [JsonProperty("network", NullValueHandling = NullValueHandling.Ignore)]
    public string Network { get; set; }

    /// <summary>
    /// e.g. http://twitter.example.com/neutralthoughts
    /// </summary>
    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public string Url { get; set; }

    /// <summary>
    /// e.g. neutralthoughts
    /// </summary>
    [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
    public string Username { get; set; }
  }
}
using Newtonsoft.Json;

namespace Core
{
  public class Skill
  {
    /// <summary>
    /// List some keywords pertaining to this skill
    /// </summary>
    [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Keywords { get; set; }

    /// <summary>
    /// e.g. Master
    /// </summary>
    [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
    public string Level { get; set; }

    /// <summary>
    /// e.g. Web Development
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }
  }
}
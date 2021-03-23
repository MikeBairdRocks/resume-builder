using Newtonsoft.Json;

namespace Core
{
  public class Interest
  {
    [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Keywords { get; set; }

    /// <summary>
    /// e.g. Philosophy
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }
  }
}
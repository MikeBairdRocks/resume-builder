using Newtonsoft.Json;

namespace Core
{
  public class Reference
  {
    /// <summary>
    /// e.g. Timothy Cook
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// e.g. Joe blogs was a great employee, who turned up to work at least once a week. He
    /// exceeded my expectations when it came to doing nothing.
    /// </summary>
    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string ReferenceReference { get; set; }
  }
}
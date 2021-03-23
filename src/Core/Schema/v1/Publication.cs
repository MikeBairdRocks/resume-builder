using Newtonsoft.Json;

namespace Core
{
  public class Publication
  {
    /// <summary>
    /// e.g. The World Wide Web
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// e.g. IEEE, Computer Magazine
    /// </summary>
    [JsonProperty("publisher", NullValueHandling = NullValueHandling.Ignore)]
    public string Publisher { get; set; }

    /// <summary>
    /// e.g. 1990-08-01
    /// </summary>
    [JsonProperty("releaseDate", NullValueHandling = NullValueHandling.Ignore)]
    public string ReleaseDate { get; set; }

    /// <summary>
    /// Short summary of publication. e.g. Discussion of the World Wide Web, HTTP, HTML.
    /// </summary>
    [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
    public string Summary { get; set; }

    /// <summary>
    /// e.g. http://www.computer.org.example.com/csdl/mags/co/1996/10/rx069-abs.html
    /// </summary>
    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public string Url { get; set; }
  }
}
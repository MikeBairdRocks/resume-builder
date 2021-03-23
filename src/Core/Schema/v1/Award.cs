using System;
using Newtonsoft.Json;

namespace Core
{
  public class Award
  {
    /// <summary>
    /// e.g. Time Magazine
    /// </summary>
    [JsonProperty("awarder", NullValueHandling = NullValueHandling.Ignore)]
    public string Awarder { get; set; }

    /// <summary>
    /// e.g. 1989-06-12
    /// </summary>
    [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? Date { get; set; }

    /// <summary>
    /// e.g. Received for my work with Quantum Physics
    /// </summary>
    [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
    public string Summary { get; set; }

    /// <summary>
    /// e.g. One of the 100 greatest minds of the century
    /// </summary>
    [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
    public string Title { get; set; }
  }
}
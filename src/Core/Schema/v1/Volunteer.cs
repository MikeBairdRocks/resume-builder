using System;
using Newtonsoft.Json;

namespace Core
{
  public class Volunteer
  {
    /// <summary>
    /// e.g. 2012-06-29
    /// </summary>
    [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// Specify accomplishments and achievements
    /// </summary>
    [JsonProperty("highlights", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Highlights { get; set; }

    /// <summary>
    /// e.g. Facebook
    /// </summary>
    [JsonProperty("organization", NullValueHandling = NullValueHandling.Ignore)]
    public string Organization { get; set; }

    /// <summary>
    /// e.g. Software Engineer
    /// </summary>
    [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
    public string Position { get; set; }

    /// <summary>
    /// resume.json uses the ISO 8601 date standard e.g. 2014-06-29
    /// </summary>
    [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Give an overview of your responsibilities at the company
    /// </summary>
    [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
    public string Summary { get; set; }

    /// <summary>
    /// e.g. http://facebook.example.com
    /// </summary>
    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public Uri Url { get; set; }
  }
}
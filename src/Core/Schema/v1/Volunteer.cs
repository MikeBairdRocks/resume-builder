using System;
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Volunteer
  {
    /// <summary>
    /// e.g. 2012-06-29
    /// </summary>
    [JsonPropertyName("endDate")]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// Specify accomplishments and achievements
    /// </summary>
    [JsonPropertyName("highlights")]
    public string[] Highlights { get; set; }

    /// <summary>
    /// e.g. Facebook
    /// </summary>
    [JsonPropertyName("organization")]
    public string Organization { get; set; }

    /// <summary>
    /// e.g. Software Engineer
    /// </summary>
    [JsonPropertyName("position")]
    public string Position { get; set; }

    /// <summary>
    /// resume.json uses the ISO 8601 date standard e.g. 2014-06-29
    /// </summary>
    [JsonPropertyName("startDate")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Give an overview of your responsibilities at the company
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// e.g. http://facebook.example.com
    /// </summary>
    [JsonPropertyName("url")]
    public Uri Url { get; set; }
  }
}
using System;
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Project
  {
    /// <summary>
    /// Short summary of project. e.g. Collated works of 2017.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// e.g. 2012-06-29
    /// </summary>
    [JsonPropertyName("endDate")]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// Specify the relevant company/entity affiliations e.g. 'greenpeace', 'corporationXYZ'
    /// </summary>
    [JsonPropertyName("entity")]
    public string Entity { get; set; }

    /// <summary>
    /// Specify multiple features
    /// </summary>
    [JsonPropertyName("highlights")]
    public string[] Highlights { get; set; }

    /// <summary>
    /// Specify special elements involved
    /// </summary>
    [JsonPropertyName("keywords")]
    public string[] Keywords { get; set; }

    /// <summary>
    /// e.g. The World Wide Web
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Specify your role on this project or in company
    /// </summary>
    [JsonPropertyName("roles")]
    public string[] Roles { get; set; }

    /// <summary>
    /// resume.json uses the ISO 8601 date standard e.g. 2014-06-29
    /// </summary>
    [JsonPropertyName("startDate")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// e.g. 'volunteering', 'presentation', 'talk', 'application', 'conference'
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// e.g. http://www.computer.org/csdl/mags/co/1996/10/rx069-abs.html
    /// </summary>
    [JsonPropertyName("url")]
    public Uri Url { get; set; }
  }
}
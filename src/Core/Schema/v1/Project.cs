using System;
using Newtonsoft.Json;

namespace Core
{
  public class Project
  {
    /// <summary>
    /// Short summary of project. e.g. Collated works of 2017.
    /// </summary>
    [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; set; }

    /// <summary>
    /// e.g. 2012-06-29
    /// </summary>
    [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// Specify the relevant company/entity affiliations e.g. 'greenpeace', 'corporationXYZ'
    /// </summary>
    [JsonProperty("entity", NullValueHandling = NullValueHandling.Ignore)]
    public string Entity { get; set; }

    /// <summary>
    /// Specify multiple features
    /// </summary>
    [JsonProperty("highlights", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Highlights { get; set; }

    /// <summary>
    /// Specify special elements involved
    /// </summary>
    [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Keywords { get; set; }

    /// <summary>
    /// e.g. The World Wide Web
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// Specify your role on this project or in company
    /// </summary>
    [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Roles { get; set; }

    /// <summary>
    /// resume.json uses the ISO 8601 date standard e.g. 2014-06-29
    /// </summary>
    [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// e.g. 'volunteering', 'presentation', 'talk', 'application', 'conference'
    /// </summary>
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }

    /// <summary>
    /// e.g. http://www.computer.org/csdl/mags/co/1996/10/rx069-abs.html
    /// </summary>
    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public Uri Url { get; set; }
  }
}
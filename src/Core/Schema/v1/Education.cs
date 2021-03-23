using System;
using Newtonsoft.Json;

namespace Core
{
  public class Education
  {
    /// <summary>
    /// e.g. Arts
    /// </summary>
    [JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
    public string Area { get; set; }

    /// <summary>
    /// List notable courses/subjects
    /// </summary>
    [JsonProperty("courses", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Courses { get; set; }

    /// <summary>
    /// e.g. 2012-06-29
    /// </summary>
    [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// grade point average, e.g. 3.67/4.0
    /// </summary>
    [JsonProperty("gpa", NullValueHandling = NullValueHandling.Ignore)]
    public string Gpa { get; set; }

    /// <summary>
    /// e.g. Massachusetts Institute of Technology
    /// </summary>
    [JsonProperty("institution", NullValueHandling = NullValueHandling.Ignore)]
    public string Institution { get; set; }

    /// <summary>
    /// e.g. 2014-06-29
    /// </summary>
    [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// e.g. Bachelor
    /// </summary>
    [JsonProperty("studyType", NullValueHandling = NullValueHandling.Ignore)]
    public string StudyType { get; set; }
  }
}
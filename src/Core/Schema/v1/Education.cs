using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Humanizer;
using Humanizer.Localisation;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Education
  {
    /// <summary>
    /// e.g. Arts
    /// </summary>
    [JsonPropertyName("area")]
    public string Area { get; set; }

    /// <summary>
    /// List notable courses/subjects
    /// </summary>
    [JsonPropertyName("courses")]
    public string[] Courses { get; set; }

    /// <summary>
    /// e.g. 2012-06-29
    /// </summary>
    [JsonPropertyName("endDate")]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// grade point average, e.g. 3.67/4.0
    /// </summary>
    [JsonPropertyName("gpa")]
    public string Gpa { get; set; }

    /// <summary>
    /// e.g. Massachusetts Institute of Technology
    /// </summary>
    [JsonPropertyName("institution")]
    public string Institution { get; set; }

    /// <summary>
    /// e.g. 2014-06-29
    /// </summary>
    [JsonPropertyName("startDate")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// e.g. Bachelor
    /// </summary>
    [JsonPropertyName("studyType")]
    public string StudyType { get; set; }
    
    [JsonIgnore]
    public string DateRange => $"{StartDate ?? DateTimeOffset.Now:MMM, yyyy} - {EndDate ?? DateTimeOffset.Now:MMM, yyyy}";

    [JsonIgnore]
    public string DateDistance
    {
      get
      {
        var range = EndDate.GetValueOrDefault(DateTimeOffset.Now) - StartDate.GetValueOrDefault(DateTimeOffset.Now);
        
        return range.Humanize(2, CultureInfo.CurrentCulture, TimeUnit.Year, TimeUnit.Month);
      }
    }
  }
}
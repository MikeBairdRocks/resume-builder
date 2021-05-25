using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Humanizer;
using Humanizer.Localisation;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Work
  {
    /// <summary>
    /// e.g. Social Media Company
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// e.g. 2012-06-29
    /// </summary>
    [JsonPropertyName("endDate")]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// Specify multiple accomplishments
    /// </summary>
    [JsonPropertyName("highlights")]
    public string[] Highlights { get; set; }

    /// <summary>
    /// e.g. Menlo Park, CA
    /// </summary>
    [JsonPropertyName("location")]
    public string Location { get; set; }

    /// <summary>
    /// e.g. Facebook
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

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

    [JsonIgnore]
    public string DateRange
    {
      get
      {
        var startDate = StartDate != null ? StartDate?.ToString("MMM, yyyy") : DateTimeOffset.Now.ToString("MMM, yyyy");
        var endDate = EndDate != null ? EndDate?.ToString("MMM, yyyy") : "Current";
        
        return $"{startDate} - {endDate}";
      }
    }

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
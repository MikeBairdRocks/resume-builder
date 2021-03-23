using System;
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Award
  {
    /// <summary>
    /// e.g. Time Magazine
    /// </summary>
    [JsonPropertyName("awarder")]
    public string Awarder { get; set; }

    /// <summary>
    /// e.g. 1989-06-12
    /// </summary>
    [JsonPropertyName("date")]
    public DateTimeOffset? Date { get; set; }

    /// <summary>
    /// e.g. Received for my work with Quantum Physics
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// e.g. One of the 100 greatest minds of the century
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }
  }
}
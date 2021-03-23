
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Language
  {
    /// <summary>
    /// e.g. Fluent, Beginner
    /// </summary>
    [JsonPropertyName("fluency")]
    public string Fluency { get; set; }

    /// <summary>
    /// e.g. English, Spanish
    /// </summary>
    [JsonPropertyName("language")]
    public string LanguageLanguage { get; set; }
  }
}
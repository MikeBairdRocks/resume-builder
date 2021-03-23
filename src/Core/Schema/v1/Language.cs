using Newtonsoft.Json;

namespace Core
{
  public class Language
  {
    /// <summary>
    /// e.g. Fluent, Beginner
    /// </summary>
    [JsonProperty("fluency", NullValueHandling = NullValueHandling.Ignore)]
    public string Fluency { get; set; }

    /// <summary>
    /// e.g. English, Spanish
    /// </summary>
    [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
    public string LanguageLanguage { get; set; }
  }
}
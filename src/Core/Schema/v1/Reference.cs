using System.Text.Json.Serialization;

namespace Core
{
  public class Reference
  {
    /// <summary>
    /// e.g. Timothy Cook
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// e.g. Joe blogs was a great employee, who turned up to work at least once a week. He
    /// exceeded my expectations when it came to doing nothing.
    /// </summary>
    [JsonPropertyName("reference")]
    public string ReferenceReference { get; set; }
  }
}
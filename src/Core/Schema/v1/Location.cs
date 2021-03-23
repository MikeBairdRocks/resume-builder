using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Location
  {
    /// <summary>
    /// To add multiple address lines, use
    /// . For example, 1234 Glücklichkeit Straße
    /// Hinterhaus 5. Etage li.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    /// <summary>
    /// code as per ISO-3166-1 ALPHA-2, e.g. US, AU, IN
    /// </summary>
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }

    /// <summary>
    /// The general region where you live. Can be a US state, or a province, for instance.
    /// </summary>
    [JsonPropertyName("region")]
    public string Region { get; set; }
  }
}
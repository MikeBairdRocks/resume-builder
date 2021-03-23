using Newtonsoft.Json;

namespace Core
{
  public class Location
  {
    /// <summary>
    /// To add multiple address lines, use
    /// . For example, 1234 Glücklichkeit Straße
    /// Hinterhaus 5. Etage li.
    /// </summary>
    [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
    public string Address { get; set; }

    [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
    public string City { get; set; }

    /// <summary>
    /// code as per ISO-3166-1 ALPHA-2, e.g. US, AU, IN
    /// </summary>
    [JsonProperty("countryCode", NullValueHandling = NullValueHandling.Ignore)]
    public string CountryCode { get; set; }

    [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
    public string PostalCode { get; set; }

    /// <summary>
    /// The general region where you live. Can be a US state, or a province, for instance.
    /// </summary>
    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public string Region { get; set; }
  }
}
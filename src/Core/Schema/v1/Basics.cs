using System;
using Newtonsoft.Json;

namespace Core
{
  public class Basics
  {
    /// <summary>
    /// e.g. thomas@gmail.com
    /// </summary>
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string Email { get; set; }

    /// <summary>
    /// URL (as per RFC 3986) to a image in JPEG or PNG format
    /// </summary>
    [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
    public string Image { get; set; }

    /// <summary>
    /// e.g. Web Developer
    /// </summary>
    [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
    public string Label { get; set; }

    [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
    public Location Location { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// Phone numbers are stored as strings so use any format you like, e.g. 712-117-2923
    /// </summary>
    [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
    public string Phone { get; set; }

    /// <summary>
    /// Specify any number of social networks that you participate in
    /// </summary>
    [JsonProperty("profiles", NullValueHandling = NullValueHandling.Ignore)]
    public Profile[] Profiles { get; set; }

    /// <summary>
    /// Write a short 2-3 sentence biography about yourself
    /// </summary>
    [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
    public string Summary { get; set; }

    /// <summary>
    /// URL (as per RFC 3986) to your website, e.g. personal homepage
    /// </summary>
    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public Uri Url { get; set; }
  }
}
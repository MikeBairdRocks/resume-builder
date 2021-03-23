using System;
using System.Text.Json.Serialization;

namespace ResumeBuilder.Core.Schema.v1
{
  public class Basics
  {
    /// <summary>
    /// e.g. thomas@gmail.com
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }

    /// <summary>
    /// URL (as per RFC 3986) to a image in JPEG or PNG format
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; set; }

    /// <summary>
    /// e.g. Web Developer
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; }

    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Phone numbers are stored as strings so use any format you like, e.g. 712-117-2923
    /// </summary>
    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    /// <summary>
    /// Specify any number of social networks that you participate in
    /// </summary>
    [JsonPropertyName("profiles")]
    public Profile[] Profiles { get; set; }

    /// <summary>
    /// Write a short 2-3 sentence biography about yourself
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// URL (as per RFC 3986) to your website, e.g. personal homepage
    /// </summary>
    [JsonPropertyName("url")]
    public Uri Url { get; set; }
  }
}
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class MultiAnnotationsResponseData
{
    [JsonProperty("responses")]
    public Response[] Responses { get; set; }
}

public partial class Response
{
    [JsonProperty("localizedObjectAnnotations")]
    public LocalizedObjectAnnotation[] LocalizedObjectAnnotations { get; set; }
}

public partial class LocalizedObjectAnnotation
{
    [JsonProperty("mid")]
    public string Mid { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("boundingPoly")]
    public BoundingPoly BoundingPoly { get; set; }
}

public partial class BoundingPoly
{
    [JsonProperty("normalizedVertices")]
    public NormalizedVertex[] NormalizedVertices { get; set; }
}

public partial class NormalizedVertex
{
    [JsonProperty("x")]
    public double X { get; set; }

    [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
    public double? Y { get; set; }
}

public partial class MultiAnnotationsResponseData
{
    public static MultiAnnotationsResponseData FromJson(string json) => JsonConvert.DeserializeObject<MultiAnnotationsResponseData>(json, CodeBeautify.Converter.Settings);
}

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
}
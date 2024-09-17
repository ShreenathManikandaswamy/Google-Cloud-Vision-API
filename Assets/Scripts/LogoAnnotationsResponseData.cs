using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class LogoAnnotationResponse
{
    [JsonProperty("responses")]
    public Response[] Responses { get; set; }
}

public partial class Response
{
    [JsonProperty("logoAnnotations")]
    public LogoAnnotation[] LogoAnnotations { get; set; }
}

public partial class LogoAnnotation
{
    [JsonProperty("mid")]
    public string Mid { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("boundingPoly")]
    public BoundingPoly BoundingPoly { get; set; }
}

public partial class BoundingPoly
{
    [JsonProperty("vertices")]
    public Vertex[] Vertices { get; set; }
}

public partial class Vertex
{
    [JsonProperty("x")]
    public long X { get; set; }

    [JsonProperty("y")]
    public long Y { get; set; }
}

public partial class LogoAnnotationResponse
{
    public static LogoAnnotationResponse FromJson(string json) => JsonConvert.DeserializeObject<LogoAnnotationResponse>(json, CodeBeautify.Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this LogoAnnotationResponse self) => JsonConvert.SerializeObject(self, CodeBeautify.Converter.Settings);
}

namespace CodeBeautify
{
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
}

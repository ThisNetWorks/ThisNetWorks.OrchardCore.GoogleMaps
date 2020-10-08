using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ThisNetWorks.OrchardCore.GoogleMaps
{
    public static class CamelCaseJsonSerializer
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        public static readonly JsonSerializer Serializer = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }
}

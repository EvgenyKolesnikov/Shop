using Newtonsoft.Json;

namespace Shop.Repository.Response
{
    public class FeatureResponseDTO
    {
        public int FeatureId { get; set; }
        public string? Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Value { get; set; }
    }
}

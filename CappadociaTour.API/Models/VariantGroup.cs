using Newtonsoft.Json;

namespace CappadociaTour.API.Models
{
    public class VariantGroup
    {
        [JsonProperty("TypeName")]
        public string TypeName { get; set; }

        [JsonProperty("VariantName")]
        public string VariantName { get; set; }
    }
}

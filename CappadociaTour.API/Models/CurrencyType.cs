using Newtonsoft.Json;

namespace CappadociaTour.API.Models
{
    public class CurrencyType
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}

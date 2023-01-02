using Newtonsoft.Json;

namespace CappadociaTour.API.Models
{
    public partial class Category
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}

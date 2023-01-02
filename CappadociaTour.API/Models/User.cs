using Newtonsoft.Json;

namespace CappadociaTour.API.Models
{
    public partial class User
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Surname")]
        public string Surname { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }
        [JsonProperty("Role")]
        public string Role { get; set; }
    }
}

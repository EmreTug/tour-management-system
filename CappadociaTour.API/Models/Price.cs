using Newtonsoft.Json;

namespace CappadociaTour.API.Models
{
    public class Price
    {
        public int CategoryId { get; set; }
        public int ReservationTypeId { get; set; }
        public int ReservationVariantId { get; set; }
    }

    public class PriceViewModel
    {
        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}

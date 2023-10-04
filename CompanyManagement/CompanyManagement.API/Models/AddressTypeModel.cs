using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CompanyManagement.API.Models
{
    public class AddressTypeModel
    {
        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("addresses")]
        public HashSet<AddressModel>? Addresses { get; set; }
    }
}

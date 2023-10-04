using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CompanyManagement.API.Models
{
    public class ClientModel
    {
        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("addresses")]
        public HashSet<AddressModel>? Addresses { get; set; }
    }
}

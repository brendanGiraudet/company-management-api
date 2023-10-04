using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.API.Models
{
    public class AddressTypeModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Label { get; set; }
        
        public HashSet<AddressModel>? Addresses { get; set; }
    }
}

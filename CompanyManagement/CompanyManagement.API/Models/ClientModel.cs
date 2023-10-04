using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.API.Models
{
    public class ClientModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public HashSet<AddressModel>? Addresses { get; set; }
    }
}

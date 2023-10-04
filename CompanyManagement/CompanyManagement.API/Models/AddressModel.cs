using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.API.Models
{
    public class AddressModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Street { get; set; }
        
        public string ZipCode { get; set; }
        
        public string City { get; set; }

        public AddressTypeModel AddressType { get; set; }
        
        public ClientModel Client { get; set; }
    }
}

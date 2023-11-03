using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CompanyManagement.API.Models;

public class BillModel
{
    [Key]
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("number")]
    public string Number { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("operationName")]
    public string OperationName { get; set; }

    [JsonPropertyName("client")]
    public ClientModel Client { get; set; }
    
    [JsonPropertyName("services")]
    public HashSet<ServiceModel> Services { get; set; }
}
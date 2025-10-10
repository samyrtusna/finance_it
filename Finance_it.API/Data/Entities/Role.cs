using System.Text.Json.Serialization;

namespace Finance_it.API.Data.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin = 1, 
        User= 2, 
    }
}

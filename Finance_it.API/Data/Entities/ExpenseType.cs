using System.Text.Json.Serialization;

namespace Finance_it.API.Data.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ExpenseType
    {
        Fixed = 1,
        Variable = 2,
        Mixed = 3,
    }
}

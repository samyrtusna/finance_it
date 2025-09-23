using System.Text.Json.Serialization;

namespace Finance_it.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RecommendationCategory
    {
        Budget = 1,
        Saving = 2,
        Debt = 3, 
        Investment = 4,
    }
}

using System.Text.Json.Serialization;

namespace Finance_it.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RecommendationStatus
    {
        Unread = 1, 
        Accepted = 2,
        Ignored = 3,
    }
}

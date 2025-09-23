using System.Text.Json.Serialization;

namespace Finance_it.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GoalStatus
    {
        InProgress = 1, 
        Achieved = 2,
        Failed = 3,
    }
}

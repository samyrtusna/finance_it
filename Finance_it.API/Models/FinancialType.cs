using System.Text.Json.Serialization;

namespace Finance_it.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FinancialType
    {
        Income = 1,
        Expense = 2,
        Debt = 3,
        Saving = 4
    }
}

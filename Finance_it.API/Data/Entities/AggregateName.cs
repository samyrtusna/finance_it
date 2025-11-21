using System.Text.Json.Serialization;

namespace Finance_it.API.Data.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AggregateName
    {
        TotalIncome,
        TotalExpense,
        NetCashFlow,
        NetCashFlowRatio,
        TotalSavings,
        SavingsRate,
        FixedExpenses,
        FixedExpensesRatio,
        VariableExpenses,
        VariableExpensesRatio,
        TotalDebtPayments,
        DebtToIncomeRatio,
        BudgetBalanceScore
    }
}

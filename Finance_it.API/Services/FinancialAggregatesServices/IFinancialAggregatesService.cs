using Finance_it.API.Data.Entities;

namespace Finance_it.API.Services.FinancialAgregatesServices
{
    public interface IFinancialAggregatesService
    {
        decimal TotalIncome(IEnumerable<FinancialEntry> entries);
        decimal TotalExpense(IEnumerable<FinancialEntry> entries);
        decimal NetCashFlow(IEnumerable<FinancialEntry> entries);
        decimal NetCashFlowRatio(IEnumerable<FinancialEntry> entries);
        decimal TotalSavings(IEnumerable<FinancialEntry> entries);
        decimal SavingsRate(IEnumerable<FinancialEntry> entries);
        decimal FixedExpenses(IEnumerable<FinancialEntry> entries);
        decimal FixedExpensesRatio(IEnumerable<FinancialEntry> entries);
        decimal VariableExpenses(IEnumerable<FinancialEntry> entries);
        decimal VariableExpensesRatio(IEnumerable<FinancialEntry> entries);
        decimal TotalDebtPayments(IEnumerable<FinancialEntry> entries);
        decimal DebtToIncomeRatio(IEnumerable<FinancialEntry> entries);
        decimal BudgetBalanceScore(IEnumerable<FinancialEntry> entries);

    }
}

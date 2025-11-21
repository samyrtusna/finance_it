using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Exceptions;

namespace Finance_it.API.Services.FinancialAgregatesServices
{
    public class FinancialAggregatesService: IFinancialAggregatesService
    {
        public decimal TotalIncome (IEnumerable<FinancialEntry> entries)
        {
            return entries.Where(e => e.Category.Type == FinancialType.Income)
                .Sum(e => e.Amount);
        }

        public decimal TotalExpense (IEnumerable<FinancialEntry> entries)
        {
            return entries .Where(e => e.Category.Type == FinancialType.Expense)
                .Sum(e => e.Amount);
        }

        public decimal NetCashFlow (IEnumerable<FinancialEntry> entries)
        {
            return TotalIncome (entries) - TotalExpense (entries);
        }

        public decimal NetCashFlowRatio(IEnumerable<FinancialEntry> entries)
        {
            if(TotalIncome(entries) == 0)
            {
                return 0;
            }
            return NetCashFlow(entries) / TotalIncome(entries) * 100;
        }

        public decimal TotalSavings(IEnumerable<FinancialEntry> entries)
        {
            return entries.Where(e => e.Category.Type == FinancialType.Saving)
                .Sum(e => e.Amount);
        }

        public decimal SavingsRate(IEnumerable<FinancialEntry> entries)
        {

            if (TotalIncome(entries) == 0)
            {
                return 0;
            }
            return TotalSavings(entries) / TotalIncome(entries) * 100;
        }

        public decimal FixedExpenses(IEnumerable<FinancialEntry> entries)
        {
            return entries.Where(e => e.Category.Type == FinancialType.Expense && e.Category.ExpenseType == ExpenseType.Fixed)
                .Sum(e => e.Amount);
        }

        public decimal FixedExpensesRatio(IEnumerable<FinancialEntry> entries)
        {
            if (TotalExpense(entries) == 0)
            {
                return 0;
            }
            return FixedExpenses (entries) / TotalExpense(entries) * 100;
        }

        public decimal VariableExpenses(IEnumerable<FinancialEntry> entries)
        {
            return entries.Where(e => e.Category.Type == FinancialType.Expense && e.Category.ExpenseType == ExpenseType.Variable)
                .Sum(e => e.Amount);
        }

        public decimal VariableExpensesRatio(IEnumerable<FinancialEntry> entries)
        {
            if (TotalExpense(entries) == 0)
            {
                return 0;
            }
            return VariableExpenses(entries) / TotalExpense (entries) * 100;
        }

        public decimal TotalDebtPayments(IEnumerable<FinancialEntry> entries)
        {
            return entries.Where(e => e.Category.Type == FinancialType.Expense && e.Category.Name == "Debt Payment")
                .Sum(e => e.Amount);
        }

        public decimal DebtToIncomeRatio(IEnumerable<FinancialEntry> entries)
        {
            if (TotalIncome(entries) == 0)
            {
                return 0;
            }
            return TotalDebtPayments (entries) / TotalIncome(entries) * 100;
        }

        public decimal BudgetBalanceScore(IEnumerable<FinancialEntry> entries)
        {
            decimal normalizedNetCashFlowRatio = NormalizeNetCashFlow(NetCashFlowRatio(entries));
            decimal normalizedSavingsRate = NormalizeSavingsRate(SavingsRate(entries));
            decimal normalizedFixedExpenseRatio = NormalizeFixedExpenseRatio(FixedExpensesRatio(entries));
            decimal normalizedDebtToIncomeRatio = NormalizeDebtToIncomeRatio(DebtToIncomeRatio(entries));

            return normalizedNetCashFlowRatio * 40 / 100 + normalizedSavingsRate * 30 / 100 + normalizedFixedExpenseRatio * 20 / 100 + normalizedDebtToIncomeRatio * 10 / 100;

        }
        private decimal NormalizeNetCashFlow(decimal ratio)
        {
            if (ratio <= 0)
                return 0;
            if (ratio >= 20)
                return 100;
            return ratio / 20 * 100;
        }

        private decimal NormalizeSavingsRate(decimal rate)
        {
            if (rate < 5)
                return 0;
            if (rate >= 20)
                return 100;
            return (rate - 5) / (20 - 5) * 100;
        }

        private decimal NormalizeFixedExpenseRatio(decimal ratio)
        {
            if (ratio <= 40)
                return 100;
            if (ratio >= 70)
                return 0;
            return 100 - (ratio - 40) / (70 - 40) * 100;
        }

        private decimal NormalizeDebtToIncomeRatio(decimal ratio)
        {
            if (ratio <= 20)
                return 100;
            if (ratio >= 50)
                return 0;
            return 100 - (ratio - 20) / (50 - 20) * 100;
        }
    }
}

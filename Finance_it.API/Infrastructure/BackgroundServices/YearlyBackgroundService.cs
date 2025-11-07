using Finance_it.API.Data.AppDbContext;
using Finance_it.API.Data.Entities;
using Finance_it.API.Services.FinancialAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Infrastructure.BackgroundServices
{
    public class YearlyBackgroundService : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private bool _hasRunThisYear = false;

        public YearlyBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow;
                    var midnight = now.Date.AddDays(1);
                    var delay = midnight - now; 

                    if(now.Day == 1 && now.Month == 1 && !_hasRunThisYear)
                    {
                        await CalculateYearlyAgregates(stoppingToken);
                        _hasRunThisYear = true;
                    }
                    if(now.Day != 1)
                    {
                        _hasRunThisYear = false;
                    }
                    await Task.Delay(delay, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while calculating yearly agregates {ex.Message}");
                }
            }
        }

        private async Task CalculateYearlyAgregates(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var agregatesService = scope.ServiceProvider.GetRequiredService<IFinancialAgregatesService>();

            var users = await dbContext.Users.ToListAsync(cancellationToken);
            var startDate = new DateTime(DateTime.UtcNow.Year, 1, 1).AddYears(-1);
            var endDate = startDate.AddYears(1);

            foreach (var user in users)
            {
                var entries = await dbContext.FinancialEntries
                    .Include(e => e.Category)
                    .Where(e => e.UserId == user.Id && e.TransactionDate >= startDate && e.TransactionDate < endDate)
                    .ToListAsync(cancellationToken);

                if (!entries.Any())
                {
                    continue;
                }

                var agregatesList = new List<YearlyAgregate>
                {
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.TotalIncome, AgregateValue = agregatesService.TotalIncome(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.TotalExpense, AgregateValue = agregatesService.TotalExpense(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.NetCashFlow, AgregateValue = agregatesService.NetCashFlow(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.NetCashFlowRatio, AgregateValue = agregatesService.NetCashFlowRatio(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.TotalSavings, AgregateValue = agregatesService.TotalSavings(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.SavingsRate, AgregateValue = agregatesService.SavingsRate(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.FixedExpenses, AgregateValue = agregatesService.FixedExpenses(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.FixedExpensesRatio, AgregateValue = agregatesService.FixedExpensesRatio(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.VariableExpenses, AgregateValue = agregatesService.VariableExpenses(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.VariableExpensesRatio, AgregateValue = agregatesService.VariableExpensesRatio(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.TotalDebtPayments, AgregateValue = agregatesService.TotalDebtPayments(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.DebtToIncomeRatio, AgregateValue = agregatesService.DebtToIncomeRatio(entries)},
                    new() {UserId = user.Id, Year = startDate.Year, AgregateName = AgregateName.BudgetBalanceScore, AgregateValue = agregatesService.BudgetBalanceScore(entries)}
                };
                await dbContext.YearlyAgregates.AddRangeAsync(agregatesList, cancellationToken);
            }
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

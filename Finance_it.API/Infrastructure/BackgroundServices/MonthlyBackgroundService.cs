using Finance_it.API.Data.AppDbContext;
using Finance_it.API.Data.Entities;
using Finance_it.API.Services.FinancialAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Infrastructure.BackgroundServices
{
    public class MonthlyBackgroundService : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private bool _hasRunThisMonth = false;

        public MonthlyBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow;
                    var nextMidnight = now.Date.AddDays(1);
                    var delay = nextMidnight - now; 

                    if(now.Day == 1 && !_hasRunThisMonth)
                    {
                        await CalculateMonthlyAgregates(stoppingToken);
                        _hasRunThisMonth = true;
                    }
                    if (now.Day != 1)
                    {
                        _hasRunThisMonth = false;
                    }
                    await Task.Delay(delay, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while calculating monthly agregates {ex.Message}");
                }
            }
        }

        private async Task CalculateMonthlyAgregates(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var agregatesService = scope.ServiceProvider.GetRequiredService<IFinancialAgregatesService>();

            var users = await dbContext.Users.ToListAsync(cancellationToken);
            var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1);
            var endDate = startDate.AddMonths(1);

            foreach (var user in users)
            {
                var entries = await dbContext.FinancialEntries
                    .Include(e => e.Category)
                    .Where(e => e.UserId == user.Id && e.TransactionDate >= startDate && e.TransactionDate < endDate)
                    .ToListAsync(cancellationToken);

                if(!entries.Any())
                {
                    continue;
                }
                
                var agregatesList = new List<MonthlyAgregate>
                {
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.TotalIncome, AgregateValue = agregatesService.TotalIncome(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.TotalExpense, AgregateValue = agregatesService.TotalExpense(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.NetCashFlow, AgregateValue = agregatesService.NetCashFlow(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.NetCashFlowRatio, AgregateValue = agregatesService.NetCashFlowRatio(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.TotalSavings, AgregateValue = agregatesService.TotalSavings(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.SavingsRate, AgregateValue = agregatesService.SavingsRate(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.FixedExpenses, AgregateValue = agregatesService.FixedExpenses(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.FixedExpensesRatio, AgregateValue = agregatesService.FixedExpensesRatio(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.VariableExpenses, AgregateValue = agregatesService.VariableExpenses(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.VariableExpensesRatio, AgregateValue = agregatesService.VariableExpensesRatio(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.TotalDebtPayments, AgregateValue = agregatesService.TotalDebtPayments(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.DebtToIncomeRatio, AgregateValue = agregatesService.DebtToIncomeRatio(entries) },
                    new() { UserId = user.Id, Year = startDate.Year, Month = startDate.ToString("MMMM"), AgregateName = AgregateName.BudgetBalanceScore, AgregateValue = agregatesService.BudgetBalanceScore(entries) }
                };

                await dbContext.MonthlyAgregates.AddRangeAsync(agregatesList, cancellationToken);
            }
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

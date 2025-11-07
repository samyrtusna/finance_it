using Finance_it.API.Data.AppDbContext;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Utils;
using Finance_it.API.Services.FinancialAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Infrastructure.BackgroundServices
{
    public class WeeklyBackgroundService : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private bool _hasRunThisWeek = false;

        public WeeklyBackgroundService(IServiceProvider serviceProvider)
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
                    var nextMidnight = now.Date.AddDays(1);
                    var delay = nextMidnight - now;
                    var currentWeekStart = WeekAgregateUtils.GetWeekStartAndEnd(now).weekStart;

                    if (now.Day == currentWeekStart.Day && !_hasRunThisWeek)
                    {
                        await CalculateWeeklyAgregates(stoppingToken);
                        _hasRunThisWeek = true;
                    }
                    if(now.Day != currentWeekStart.Day)
                    {
                        _hasRunThisWeek = false ;
                    }
                    await Task.Delay(delay, stoppingToken);

                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred while calculating weekly agregates {ex.Message}");
                }
            }
        }

        private async Task CalculateWeeklyAgregates(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var agregatesService = scope.ServiceProvider.GetRequiredService<IFinancialAgregatesService>();

            var users = await dbContext.Users.ToListAsync(cancellationToken);

            var yesterday = DateTime.UtcNow.AddDays(-1);
            var (weekStart, weekEnd) = WeekAgregateUtils.GetWeekStartAndEnd(yesterday);

            foreach ( var user in users )
            {
                var entries = await dbContext.FinancialEntries
                    .Include(e => e.Category)
                    .Where(e => e.UserId == user.Id && e.TransactionDate >= weekStart && e.TransactionDate < weekEnd)
                    .ToListAsync(cancellationToken);

                if (!entries.Any())
                {
                    continue;
                }

                var agregatesList = new List<WeeklyAgregate>
                {
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.TotalIncome, AgregateValue = agregatesService.TotalIncome(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.TotalExpense, AgregateValue = agregatesService.TotalExpense(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.NetCashFlow, AgregateValue = agregatesService.NetCashFlow(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.NetCashFlowRatio, AgregateValue = agregatesService.NetCashFlowRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.TotalSavings, AgregateValue = agregatesService.TotalSavings(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.SavingsRate, AgregateValue = agregatesService.SavingsRate(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.FixedExpenses, AgregateValue = agregatesService.FixedExpenses(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.FixedExpensesRatio, AgregateValue = agregatesService.FixedExpensesRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.VariableExpenses, AgregateValue = agregatesService.VariableExpenses(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.VariableExpensesRatio, AgregateValue = agregatesService.VariableExpensesRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.TotalDebtPayments, AgregateValue = agregatesService.TotalDebtPayments(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.DebtToIncomeRatio, AgregateValue = agregatesService.DebtToIncomeRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AgregateName = AgregateName.BudgetBalanceScore, AgregateValue = agregatesService.BudgetBalanceScore(entries)}
                };
                await dbContext.WeeklyAgregates.AddRangeAsync(agregatesList, cancellationToken);
            }
            await dbContext.AddRangeAsync(cancellationToken);
        }
    }
}

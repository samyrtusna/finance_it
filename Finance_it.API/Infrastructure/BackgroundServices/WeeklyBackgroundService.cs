using Finance_it.API.Data.AppDbContext;
using Finance_it.API.Data.Entities;
using Finance_it.API.Infrastructure.Utils;
using Finance_it.API.Services.FinancialAgregatesServices;
using Microsoft.EntityFrameworkCore;

namespace Finance_it.API.Infrastructure.BackgroundServices
{
    public class WeeklyBackgroundService : BackgroundService
    {
        private readonly
            
            IServiceProvider _serviceProvider;
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
                    var currentWeekStart = WeekAggregateUtils.GetWeekStartAndEnd(now).weekStart;

                    if (now.Day == currentWeekStart.Day && !_hasRunThisWeek)
                    {
                        await CalculateWeeklyAgregates(stoppingToken);
                        _hasRunThisWeek = true;
                    }
                    if(now.Day != currentWeekStart.Day && _hasRunThisWeek)
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
            var agregatesService = scope.ServiceProvider.GetRequiredService<IFinancialAggregatesService>();

            var users = await dbContext.Users.ToListAsync(cancellationToken);

            var yesterday = DateTime.UtcNow.AddDays(-1);
            var (weekStart, weekEnd) = WeekAggregateUtils.GetWeekStartAndEnd(yesterday);

            foreach ( var user in users )
            {
                var entries = await dbContext.FinancialEntries
                    .Include(e => e.Category)
                    .Where(e => e.UserId == user.Id && e.TransactionDate >= weekStart && e.TransactionDate < weekEnd)
                    .ToListAsync(cancellationToken);

                if (entries.Count == 0)
                {
                    continue;
                }

                var agregatesList = new List<WeeklyAggregate>
                {
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.TotalIncome, AggregateValue = agregatesService.TotalIncome(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.TotalExpense, AggregateValue = agregatesService.TotalExpense(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.NetCashFlow, AggregateValue = agregatesService.NetCashFlow(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.NetCashFlowRatio, AggregateValue = agregatesService.NetCashFlowRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.TotalSavings, AggregateValue = agregatesService.TotalSavings(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.SavingsRate, AggregateValue = agregatesService.SavingsRate(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.FixedExpenses, AggregateValue = agregatesService.FixedExpenses(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.FixedExpensesRatio, AggregateValue = agregatesService.FixedExpensesRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.VariableExpenses, AggregateValue = agregatesService.VariableExpenses(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.VariableExpensesRatio, AggregateValue = agregatesService.VariableExpensesRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.TotalDebtPayments, AggregateValue = agregatesService.TotalDebtPayments(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.DebtToIncomeRatio, AggregateValue = agregatesService.DebtToIncomeRatio(entries)},
                    new() {UserId = user.Id, WeekStartDate = weekStart, WeekEndDate = weekEnd.AddDays(-1), AggregateName = AggregateName.BudgetBalanceScore, AggregateValue = agregatesService.BudgetBalanceScore(entries)}
                };
                await dbContext.WeeklyAggregates.AddRangeAsync(agregatesList, cancellationToken);
            }
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

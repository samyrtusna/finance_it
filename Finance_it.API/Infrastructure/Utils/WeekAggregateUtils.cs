namespace Finance_it.API.Infrastructure.Utils
{
    public static class WeekAggregateUtils
    {
        public static (DateTime weekStart, DateTime weekEnd) GetWeekStartAndEnd(DateTime date)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            var diff = (7 + (date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek)) % 7;

            var weekStart = date.AddDays(-diff).Date;
            var weekEnd = weekStart.AddDays(7).Date;

            return (weekStart, weekEnd);
        }
    }
}

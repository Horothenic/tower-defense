using System;
using System.Linq;
using System.Collections.Generic;

namespace Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static int NumberOfWorkingDays(this DateTime startDate, DateTime endDate, List<DateTime> holidays)
        {
            return Enumerable.Range(default(int), (endDate - startDate).Days)
                            .Select(iteration => startDate.AddDays(iteration))
                            .Where(day => day.DayOfWeek != DayOfWeek.Sunday)
                            .Where(day => day.DayOfWeek != DayOfWeek.Saturday)
                            .Count(day => !holidays.Any(localDay => localDay == day));
        }

        public static bool IsWorkingDay(this DateTime date, List<DateTime> holidays = null)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            if (holidays != null)
            {
                foreach (DateTime holiday in holidays)
                    if (date.Date == holiday.Date)
                        return false;
            }

            return true;
        }
    }
}

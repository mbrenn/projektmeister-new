using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektMeister.Logic
{
    public static class WorkTimeDefinitions
    {
        /// <summary>
        /// Takes an existing worktime definition and removes some day from the working days
        /// </summary>
        /// <param name="definition">Definition to be modified</param>
        /// <param name="days">Days to remove</param>
        /// <returns>The modified worktime definition</returns>
        public static IWorkTimeDefinition ExcludeDays(
            this IWorkTimeDefinition definition,
            IEnumerable<DateTime> days)
        {
            var daysAsList = days.ToList();
            return new PredicateWorkTimeDefinition(
                x =>
                {
                    if (daysAsList.Contains(x.Date))
                    {
                        return false;
                    }

                    return definition.MayWork(x);
                });
        }

        /// <summary>
        /// Returns a worktime definition which allows the ressources only work on Weekdays
        /// within a certain time
        /// </summary>
        /// <param name="from">Time to start</param>
        /// <param name="to">Time to stop</param>
        /// <returns>The worktime definition</returns>
        public static IWorkTimeDefinition OnWorkDays(double fromHour, double toHour)
        {
            return OnWorkDays(
                TimeSpan.FromHours(fromHour),
                TimeSpan.FromHours(toHour));
        }

        /// <summary>
        /// Returns a worktime definition which allows the ressources only work on Weekdays
        /// within a certain time
        /// </summary>
        /// <param name="from">Time to start</param>
        /// <param name="to">Time to stop</param>
        /// <returns>The worktime definition</returns>
        public static IWorkTimeDefinition OnWorkDays(TimeSpan from, TimeSpan to)
        {
            if (from > to)
            {
                throw new InvalidOperationException("from is smaller than to. No work " +
                                                    "would be possible");
            }
            var func = new Func<DateTime, bool>(
                x =>
                {
                    if (x.DayOfWeek == DayOfWeek.Saturday
                        || x.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return false;
                    }

                    var time = x.TimeOfDay;
                    return time >= from && time < to;
                });

            return new PredicateWorkTimeDefinition(func);
        }
    }
}
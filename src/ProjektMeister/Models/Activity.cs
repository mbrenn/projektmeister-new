using System;
using System.Collections.Generic;
using ProjektMeister.Helper;

namespace ProjektMeister.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ActivityType Type { get; set; }

        public Calculatable<DateTime> StartDate { get; } = new Calculatable<DateTime>();

        public Calculatable<DateTime> EndDate { get; } = new Calculatable<DateTime>();

        /// <summary>
        /// Defines the duration of an activity
        /// This definition is totally independent of the worktime.
        /// </summary>
        public Calculatable<TimeSpan> Duration { get; set; } = new Calculatable<TimeSpan>();

        /// <summary>
        /// Defines the workload of an activity.
        /// This workload is only performed during the worktime.
        /// See also the interface IWorktimeDefinition. 
        /// </summary>
        public Calculatable<TimeSpan> WorkLoad { get; set; } = new Calculatable<TimeSpan>();

        public List<Activity> Dependencies { get; private set; } = new List<Activity>();

        public override string ToString()
        {
            return $"#{Id} - {Name}\r\n - S: {StartDate} \r\n - E: {EndDate}, \r\n - D: {Duration}";
        }
    }
}

using System;
using System.Collections.Generic;

namespace ProjektMeister.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ActivityType Type { get; set; }

        public Calculatable<DateTime> StartDate { get; } = new Calculatable<DateTime>();

        public Calculatable<DateTime> EndDate { get; } = new Calculatable<DateTime>();

        public Calculatable<TimeSpan> Duration { get; set; } = new Calculatable<TimeSpan>();

        public List<Activity> Dependencies { get; private set; } = new List<Activity>();
    }
}

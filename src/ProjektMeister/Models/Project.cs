using System;
using System.Collections.Generic;
using ProjektMeister.Helper;

namespace ProjektMeister.Models
{
    public class Project
    {
        public List<Activity> Activities { get; } = new List<Activity>();

        public Calculatable< DateTime> Start { get; } = new Calculatable<DateTime>();

        public Calculatable<DateTime> EndOfProject { get; } = new Calculatable<DateTime>();

        /// <summary>
        /// Gets all activities in a sorted way
        /// </summary>
        /// <returns></returns>
        public IList<Activity> GetSortedActivities()
        {
            return TopologicalSort.Sort(Activities, x => x.Dependencies);
        }
    }
}
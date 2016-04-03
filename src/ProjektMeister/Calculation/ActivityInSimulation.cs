using System;
using ProjektMeister.Models;

namespace ProjektMeister.Calculation
{
    /// <summary>
    /// Represents the temporary information being used for an acitvity
    /// within a simulation
    /// </summary>
    public class ActivityInSimulation
    {
        private readonly Activity _activity;

        public ActivityInSimulation(Activity activity)
        {
            _activity = activity;
        }

        /// <summary>
        /// Stores whether the activity already has been started
        /// </summary>
         public bool IsStarted { get; set; }

        /// <summary>
        /// Stores the amount of duration that has been done within this activity
        /// </summary>
        public TimeSpan DurationDone { get; set; }

        /// <summary>
        /// Stores the amount of work that has been done within this activity
        /// </summary>
        public TimeSpan WorkDone { get; set; }

        /// <summary>
        /// Gets or sets the information when the work on the activity was started
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the information when the work on the activity was ended
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
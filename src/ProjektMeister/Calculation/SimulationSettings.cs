using System;
using ProjektMeister.Logic;

namespace ProjektMeister.Calculation
{
    public class SimulationSettings
    {
        /// <summary>
        /// Defines the calculation interval for each project step. 
        /// Within each calculation interval, the work is redistributed.
        /// </summary>
        public TimeSpan CalculationInterval { get; set; } = TimeSpan.FromHours(1);

        public DateTime LastSimulationDate { get; set; } = new DateTime(2099, 12, 31, 23, 59, 59);

        public IWorkTimeDefinition WorkTimeDefinition { get; set; }

        /// <summary>
        /// Gets or sets the available resources. 
        /// This defines the number of tasks that can be performed in parallel. 
        /// </summary>
        public double AvailableResources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <c>SimulationSettings</c>
        /// </summary>
        public SimulationSettings()
        {
            AvailableResources = double.MaxValue;
        }
    }
}
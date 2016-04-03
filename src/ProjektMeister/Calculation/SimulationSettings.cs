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
    }
}
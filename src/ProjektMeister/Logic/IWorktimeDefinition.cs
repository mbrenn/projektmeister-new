using System;

namespace ProjektMeister.Logic
{
    /// <summary>
    /// Defines the time, when a person may work or when he may not work
    /// </summary>
    public interface IWorkTimeDefinition
    {
        /// <summary>
        /// Returns true, if the persons may work at the given date
        /// </summary>
        /// <param name="time">Time which shall be evaluated</param>
        /// <returns>true, if the person may work</returns>
        bool MayWork(DateTime time);
    }
}
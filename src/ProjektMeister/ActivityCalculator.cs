using ProjektMeister.Models;

namespace ProjektMeister
{
    public class ActivityCalculator
    {
        private readonly ActivitySettings _settings;

        public ActivityCalculator(ActivitySettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Performs the calculation of the project and returns a new project
        /// where all the calculated information is given
        /// </summary>
        /// <param name="project">Project being calculated</param>
        /// <returns>Project with calculated values</returns>
        public void PlanProject(Project project)
        {
            /*var result = new Project();

            foreach (var activity in project.Activities)
            {
                var calculatedActivity = new Activity();
                result.Activities.Add(calculatedActivityalc);
            }*/
        }
    }
}
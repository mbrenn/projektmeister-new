﻿using System;
using System.Collections.Generic;
using System.Linq;
using ProjektMeister.Helper;
using ProjektMeister.Models;

namespace ProjektMeister.Calculation
{
    /// <summary>
    /// Is capable to perform the simulation which returns the estimated dates and work duration
    /// </summary>
    public class ProjectSimulation
    {
        private readonly SimulationSettings _settings;

        /// <summary>
        /// Stores the list of open activities
        /// </summary>
        private List<Activity> _openActivities;

        /// <summary>
        /// Stores the set of activities that already have been closed
        /// </summary>
        private HashSet<Activity> _closedActivities;

        /// <summary>
        /// Stores the current time of the simulation
        /// </summary>
        private DateTime _currentTime;

        /// <summary>
        /// Stores the project being used for simulation
        /// </summary>
        private Project _project;

        /// <summary>
        /// Stores the information about the activities which are currently in simulation
        /// </summary>
        private Dictionary<Activity, ActivityInSimulation> _activityInformation;

        public ProjectSimulation(SimulationSettings settings)
        {
            _settings = settings;
        }

        private ActivityInSimulation GetActivityInformation(Activity activity)
        {
            ActivityInSimulation result;
            if (_activityInformation.TryGetValue(activity, out result))
            {
                return result;
            }

            result = new ActivityInSimulation(activity);
            _activityInformation[activity] = result;
            return result;
        }

        public SimulationResult Simulate(Project project)
        {
            var result = new SimulationResult();

            if (project == null) throw new ArgumentNullException(nameof(project));
            _project = project;

            // Prepares the activities
            _openActivities = new List<Activity>(
                TopologicalSort.Sort(project.Activities, x=>x.Dependencies));
            _closedActivities = new HashSet<Activity>();
            _activityInformation = new Dictionary<Activity, ActivityInSimulation>();

            _currentTime = project.StartOfProject.Defined;

            // Does the loop 
            while (!AreAllActivitiesDone() && _currentTime < _settings.LastSimulationDate)
            {
                result.Loops++;
                SimulateInterval(_settings.CalculationInterval);
            }

            // Ok, we are done, now transfer the information
            foreach (var pair in _activityInformation)
            {
                var activity = pair.Key;
                var information = pair.Value;
                activity.StartDate.Calculated = information.StartDate;
                activity.EndDate.Calculated = information.EndDate;
                activity.Duration.Calculated = information.DurationDone;
            }

            project.EndOfProject.Calculated = _currentTime;

            return result;
        }

        private static List<Activity> readyActivities = new List<Activity>();

        /// <summary>
        /// Simulates a certain calculation
        /// </summary>
        /// <param name="calculationInterval">The Interval being simulated</param>
        private void SimulateInterval(TimeSpan calculationInterval)
        {
            var timeAtStart = _currentTime;
            _currentTime += calculationInterval;
            var didOne = false;

            // Gets a list of all ready activities. Uses cache, if necessary
            readyActivities.Clear();
            readyActivities.AddRange(GetReadyActivities());
            
            // Determines the workfactor
            var workFactor = 1.0;
            if (readyActivities.Count > _settings.AvailableResources)
            {
                workFactor = _settings.AvailableResources/readyActivities.Count;
            }

            foreach (var activity in readyActivities)
            {
                didOne = true;
                var activityInInformation = GetActivityInformation(activity);

                // Starts the activity because it is ready and was not started before
                if (!activityInInformation.IsStarted)
                {
                    activityInInformation.IsStarted = true;
                    activityInInformation.StartDate = timeAtStart;
                }

                // Progresses the activity
                activityInInformation.DurationDone += calculationInterval;
                if (_settings.WorkTimeDefinition?.MayWork(timeAtStart) == true)
                {
                    // If it is working time, then add it to the work done, 
                    var realWorkDone = TimeSpan.FromSeconds(
                        calculationInterval.TotalSeconds*workFactor);
                    activityInInformation.WorkDone += realWorkDone;
                }

                // Finalizes the activity
                var finalize = activity.Duration.IsDefined
                               && activityInInformation.DurationDone >= activity.Duration.Defined;
                finalize |= activity.EndDate.IsDefined
                            && activity.EndDate.Defined < _currentTime;
                finalize |= activity.WorkLoad.IsDefined
                            && activityInInformation.WorkDone >= activity.WorkLoad.Defined;

                if (finalize)
                {
                    FinishActivity(activity, activityInInformation);
                }
            }

            if (!didOne)
            {
                throw new InvalidOperationException(
                    "We don't have a ready activity, but open activities. Circular dependencies?");
            }
        }

        private void FinishActivity(Activity activity, ActivityInSimulation activityInInformation)
        {
            activityInInformation.EndDate = _currentTime;
            _closedActivities.Add(activity);
            _openActivities.Remove(activity);
        }

        private bool AreAllActivitiesDone()
        {
            return _openActivities.Count == 0;
        }

        /// <summary>
        /// Returns all activities that are ready to be executed
        ///  </summary>
        /// <returns></returns>
        private IEnumerable<Activity> GetReadyActivities()
        {
            foreach (var activity in _openActivities)
            {
                if (IsReady(activity))
                {
                    yield return activity;
                }
            }
        }
        
        /// <summary>
        /// Checks, if the activity is ready to be executed
        /// </summary>
        /// <param name="activity">Activity to be exeucted</param>
        /// <returns>true, if the activity is ready to get exectued</returns>
        private bool IsReady(Activity activity)
        {
            // Returns all activities whose dependencies are satisfied
            var ready = true;
            foreach (var dep in activity.Dependencies)
            {
                ready &= _closedActivities.Contains(dep);
                if (!ready)
                {
                    return false;
                }
            }

            // If the start date is defined, only if the start date is passed
            if (activity.StartDate.IsDefined)
            {
                ready &= activity.StartDate.Defined <= _currentTime;
            }

            return ready;
        }
    }
}
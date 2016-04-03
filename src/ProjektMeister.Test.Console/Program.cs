using System;
using ProjektMeister.Calculation;
using ProjektMeister.Logic;
using ProjektMeister.Models;

namespace ProjektMeister.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var project = new Project();
            project.StartOfProject.Defined = new DateTime(2016,1,1,12,0,0);

            var homeActivity = new Activity
            {
                Id = 1,
                Name = "Hausaufgaben",
                Duration = {Defined = TimeSpan.FromDays(10)}
            };
            project.Add(homeActivity);

            var prepareActivity2 = new Activity
            {
                Id = 4,
                Name = "Vorbereiten 2",
                WorkLoad = { Defined = TimeSpan.FromHours(40) }
            };
            project.Add(prepareActivity2);

            var prepareActivity = new Activity
            {
                Id = 2,
                Name = "Vorbereiten",
                WorkLoad = { Defined = TimeSpan.FromHours(80) }
            };

            homeActivity.Dependencies.Add(prepareActivity);
            homeActivity.Dependencies.Add(prepareActivity2);
            project.Add(prepareActivity);

            var finalizeActivity = new Activity
            {
                Id = 3,
                Name = "Abschließen",
                Duration = { Defined = TimeSpan.FromDays(1) }
            };
            finalizeActivity.Dependencies.AddRange(new [] {homeActivity, prepareActivity});
            project.Add(finalizeActivity);

            var logic = new ProjectSimulation(new SimulationSettings()
            {
                WorkTimeDefinition = WorkTimeDefinitions.OnWorkDays(8, 16),
                AvailableResources = 1.0,
                CalculationInterval = TimeSpan.FromSeconds(1)
            });

            var simulationResult = logic.Simulate(project);
            System.Console.WriteLine($"{prepareActivity}");
            System.Console.WriteLine($"{prepareActivity2}");
            System.Console.WriteLine($"{homeActivity}");
            System.Console.WriteLine($"{finalizeActivity}");

            System.Console.WriteLine();
            System.Console.WriteLine($"Loops: {simulationResult.Loops}");

            System.Console.ReadKey();
        }
    }
}

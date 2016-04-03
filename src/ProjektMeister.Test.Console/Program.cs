using System;
using ProjektMeister.Calculation;
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

            var prepareActivity = new Activity
            {
                Id = 2,
                Name = "Vorbereiten",
                Duration = { Defined = TimeSpan.FromDays(2) }
            };
            homeActivity.Dependencies.Add(prepareActivity);
            project.Add(prepareActivity);

            var finalizeActivity = new Activity
            {
                Id = 3,
                Name = "Abschließen",
                Duration = { Defined = TimeSpan.FromDays(1) }
            };
            finalizeActivity.Dependencies.AddRange(new [] {homeActivity, prepareActivity});
            project.Add(finalizeActivity);

            var logic = new ProjectSimulation(new SimulationSettings());
            logic.Simulate(project);
            System.Console.WriteLine($"{prepareActivity}");
            System.Console.WriteLine($"{homeActivity}");
            System.Console.WriteLine($"{finalizeActivity}");

            System.Console.ReadKey();
        }
    }
}

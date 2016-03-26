using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ProjektMeister.Models;

namespace ProjektMeister.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var project = new Project();
            project.StartOfProject.Defined = new DateTime(2016,1,1,12,0,0);

            var activity = new Activity
            {
                Id = 1,
                Name = "Hausaufgaben",
                Duration = {Defined = TimeSpan.FromDays(10)}
            };
            project.Add(activity);

            var logic = new ProjectTimingCalculator(new ProjectTimingSettings());
            logic.PlanProject(project);
            System.Console.WriteLine($"{activity}");

            System.Console.ReadKey();
        }
    }
}

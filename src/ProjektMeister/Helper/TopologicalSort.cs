using System;
using System.Collections.Generic;

namespace ProjektMeister.Helper
{
    /// <summary>
    /// Performs a topological sort by using the dependencies.
    /// Algorithm is given in: http://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp
    /// </summary>
    public static class TopologicalSort
    {
        /// <summary>
        /// Performs the sorting by giving the list and a method which returns
        /// the corresponding dependencies and returns the sorted list
        /// </summary>
        /// <typeparam name="T">Type to be sorted</typeparam>
        /// <param name="source">List to be sorted</param>
        /// <param name="getDependencies">Function which returns the dependencies for the sorting</param>
        /// <returns>Sorted list</returns>
        public static IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();

            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        private static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found.");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        Visit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }

    }
}
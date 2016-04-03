using System;

namespace ProjektMeister.Logic
{
    public class PredicateWorkTimeDefinition : IWorkTimeDefinition
    {
        private readonly Func<DateTime, bool> _predicate;

        /// <summary>
        /// Initializes a new instance of the <c>PredicateWorkTimeDefinition</c>
        /// </summary>
        /// <param name="predicate">Predicate evaluating whether the 
        /// ressource may work or not</param>
        public PredicateWorkTimeDefinition(Func<DateTime, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            _predicate = predicate;
        }

        public bool MayWork(DateTime time)
        {
            return _predicate(time);
        }
    }
}
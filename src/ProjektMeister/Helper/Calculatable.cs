using System;

namespace ProjektMeister.Helper
{
    public class Calculatable<T>
    {
        private bool _isDefined;
        private T _defined;

        public T Defined
        {
            get
            {
                if (!IsDefined)
                {
                    throw new InvalidOperationException("Value is not defined");
                }

                return _defined;
            }
            set
            {
                _defined = value;
                IsDefined = true;
            }
        }

        public bool IsDefined
        {
            get { return _isDefined; }
            set
            {
                _isDefined = value;
                if (!_isDefined)
                {
                    _defined = default(T);
                }
            }
        }

        public T Calculated { get; set; }

        public void Clear()
        {
            IsDefined = false;
        }

        public void Set(T value)
        {
            Defined = value;
        }

        /// <summary>
        /// Converts the given element to a string
        /// </summary>
        /// <returns>String value</returns>
        public override string ToString()
        {
            return $"Defined: {_defined}, Calculated: {Calculated}";
        }
    }
}
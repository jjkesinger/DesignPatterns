using System;
using System.Collections.Generic;
using System.Linq;

namespace Insperity.Integration.Trucking.Core.Utility
{
    //Null Object Pattern 
    public struct Maybe<T>
    {
        readonly IEnumerable<T> _values;

        public static Maybe<T> Some(T value)
        {
            if (value == null)
            {
                throw new InvalidOperationException();
            }

            return new Maybe<T>(new[] { value });
        }

        public static Maybe<T> None => new Maybe<T>(new T[0]);

        private Maybe(IEnumerable<T> values)
        {
            _values = values;
        }

        public bool HasValue => _values != null && _values.Any();

        public T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException("Maybe does not have a value");
                }

                return _values.Single();
            }
        }
    }
}

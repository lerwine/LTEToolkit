using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Erwine.Leonard.T.Toolkit.WPF.Collections
{
    public class ObjectReferenceEqualityComparer<T> : IEqualityComparer<T>
    {
        private static readonly ObjectReferenceEqualityComparer<T> _default = new ObjectReferenceEqualityComparer<T>();

        public static ObjectReferenceEqualityComparer<T> Default { get { return ObjectReferenceEqualityComparer<T>._default; } }

        public bool Equals(T x, T y)
        {
            return (x == null) ? (y == null) : (y != null && Object.ReferenceEquals(x, y));
        }

        public int GetHashCode(T obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }
    }
}

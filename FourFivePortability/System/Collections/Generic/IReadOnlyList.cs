using System;
using System.Linq;
using System.Text;
using System.Collections;

namespace System.Collections.Generic
{
    // This interface comes with .NET 4.5 and is here for code portability reasons
    public interface IReadOnlyList<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    {
        T this[int index] { get; }
    }
}

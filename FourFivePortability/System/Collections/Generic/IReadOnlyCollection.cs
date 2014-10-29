using System;
using System.Linq;
using System.Text;
using System.Collections;

namespace System.Collections.Generic
{
    public interface IReadOnlyCollection<T> : IEnumerable<T>, IEnumerable
    {
        int Count { get; }
    }
}

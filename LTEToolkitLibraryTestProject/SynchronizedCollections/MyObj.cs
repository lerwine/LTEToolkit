using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LTEToolkitLibraryTestProject.SynchronizedCollections
{
public class MyObj<MyType> : IList<MyType>, IList
{
    private readonly object _syncRoot;
    //private readonly List<MyType> _innerList = new List<MyType>();

    public MyObj(IEnumerable<MyType> collection)
    {
        _syncRoot = new object();
        //_syncRoot = (collection != null && ((IList)_innerList).IsSynchronized) ? (((IList)_innerList).SyncRoot ?? new object()) : new object();
        //if (collection != null)
        //    _innerList.AddRange(collection.Where(i => i != null));
    }

    public MyObj(params MyType[] list) : this((IEnumerable<MyType>)list) { }

    public MyObj() : this(null as IEnumerable<MyType>) { }

    public MyType this[int index]
    {
        get => throw new NotImplementedException(); // _innerList[index];
        set
        {
            if (value == null)
                throw new ArgumentNullException();

Monitor.Enter(_syncRoot);
try { throw new NotImplementedException(); }
//try { _innerList[index] = value; }
finally { Monitor.Exit(_syncRoot); }
        }
    }

    object IList.this[int index] { get => this[index]; set => this[index] = (MyType)value; }

    public int Count => throw new NotImplementedException(); // _innerList.Count;

    protected internal object SyncRoot => _syncRoot;
        
    bool ICollection<MyType>.IsReadOnly => throw new NotImplementedException();

    bool IList.IsReadOnly => false;

    bool IList.IsFixedSize => false;

    object ICollection.SyncRoot => _syncRoot;

    bool ICollection.IsSynchronized => true;

    public void Add(MyType item)
    {
        if (item == null)
            throw new ArgumentNullException("item");

        Monitor.Enter(_syncRoot);
        try { throw new NotImplementedException(); }
        //try { _innerList.Add(item); }
        finally { Monitor.Exit(_syncRoot); }
    }

    int IList.Add(object value)
    {
        if (value == null)
            throw new ArgumentNullException("value");

        //int index;

        Monitor.Enter(_syncRoot);
        try
        {
            throw new NotImplementedException();
            //index = _innerList.Count;
            //Add((MyType)value);
        }
        finally { Monitor.Exit(_syncRoot); }

        //return index;
    }

    public void Clear()
    {
        Monitor.Enter(_syncRoot);
        try { throw new NotImplementedException(); }
        //try { _innerList.Clear(); }
        finally { Monitor.Exit(_syncRoot); }
    }

    public bool Contains(MyType item) => throw new NotImplementedException(); // item != null && _innerList.Contains(item);

    bool IList.Contains(object value) => throw new NotImplementedException(); // value != null && value is MyType && Contains((MyType)value);

    public void CopyTo(MyType[] array, int arrayIndex) => throw new NotImplementedException(); // _innerList.CopyTo(array, arrayIndex);

    void ICollection.CopyTo(Array array, int index) => throw new NotImplementedException(); // _innerList.ToArray().CopyTo(array, index);

    public IEnumerator<MyType> GetEnumerator() => throw new NotImplementedException(); // _innerList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException(); // _innerList.GetEnumerator();

    public int IndexOf(MyType item) => (item == null) ? -1 : throw new NotImplementedException(); // _innerList.IndexOf(item);

    int IList.IndexOf(object value) => (value != null && value is MyType) ? IndexOf((MyType)value) : -1;

    public void Insert(int index, MyType item)
    {
        if (item == null)
            throw new ArgumentNullException("item");

        Monitor.Enter(_syncRoot);
        try { throw new NotImplementedException(); }
        //try { _innerList.Insert(index, item); }
        finally { Monitor.Exit(_syncRoot); }
    }

    void IList.Insert(int index, object value)
    {
        if (value == null)
            throw new ArgumentNullException("value");
        Insert(index, (MyType)value);
    }

    public bool Remove(MyType item)
    {
        if (item == null)
            return false;
        Monitor.Enter(_syncRoot);
        try { throw new NotImplementedException(); }
        //try { return _innerList.Remove(item); }
        finally { Monitor.Exit(_syncRoot); }
    }

    void IList.Remove(object value)
    {
        if (value != null && value is MyType)
            Remove((MyType)value);
    }

    public void RemoveAt(int index)
    {
        Monitor.Enter(_syncRoot);
        try { throw new NotImplementedException(); }
        //try { _innerList.RemoveAt(index); }
        finally { Monitor.Exit(_syncRoot); }
    }
}
}
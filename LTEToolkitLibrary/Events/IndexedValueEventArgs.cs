namespace Erwine.Leonard.T.Toolkit.Events
{
    public class IndexedValueEventArgs<T> : ValueEventArgs<T>
    {
        public int Index { get; set; }

        public IndexedValueEventArgs() : base() { }

        public IndexedValueEventArgs(T value) : base(value) { }

        public IndexedValueEventArgs(int index, T value)
            : base(value)
        {
            this.Index = index;
        }
    }
}

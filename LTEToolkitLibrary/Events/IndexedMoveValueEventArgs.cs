namespace Erwine.Leonard.T.Toolkit.Events
{
    public class IndexedMoveValueEventArgs<T> : ValueEventArgs<T>
    {
        public int OldIndex { get; set; }

        public int NewIndex { get; set; }

        public IndexedMoveValueEventArgs() : base() { }

        public IndexedMoveValueEventArgs(T value) : base(value) { }

        public IndexedMoveValueEventArgs(int newIndex, T value) : this(newIndex, newIndex, value) { }

        public IndexedMoveValueEventArgs(int oldIndex, int newIndex, T value)
            : base(value)
        {
            this.OldIndex = oldIndex;
            this.NewIndex = newIndex;
        }
    }
}

using System;

namespace Erwine.Leonard.T.Toolkit.Events
{
    public class ValueEventArgs<T> : EventArgs
    {
        public T Value { get; set; }

        public ValueEventArgs() : base() { }

        public ValueEventArgs(T value)
            : base()
        {
            this.Value = value;
        }
    }

    public class ValueEventArgs<TValue, TParameter> : ValueEventArgs<TValue>
    {
        public TParameter Parameter { get; set; }

        public ValueEventArgs() : base() { }

        public ValueEventArgs(TValue value) : base(value) { }

        public ValueEventArgs(TValue value, TParameter parameter)
            : base(value)
        {
            this.Parameter = parameter;
        }
    }

    public class ValueEventArgs<TValue, TParameter1, TParameter2> : ValueEventArgs<TValue>
    {
        public TParameter1 Parameter1 { get; set; }

        public TParameter2 Parameter2 { get; set; }

        public ValueEventArgs() : base() { }

        public ValueEventArgs(TValue value) : base(value) { }

        public ValueEventArgs(TValue value, TParameter1 parameter1)
            : base(value)
        {
            this.Parameter1 = parameter1;
        }

        public ValueEventArgs(TValue value, TParameter1 parameter1, TParameter2 parameter2)
            : this(value, parameter1)
        {
            this.Parameter2 = parameter2;
        }
    }
}

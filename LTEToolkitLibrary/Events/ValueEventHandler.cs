namespace Erwine.Leonard.T.Toolkit.Events
{
    public delegate void ValueEventHandler<T>(object sender, ValueEventArgs<T> e);

    public delegate void ValueEventHandler<TValue, TParameter>(object sender, ValueEventArgs<TValue, TParameter> e);

    public delegate void ValueEventHandler<TValue, TParameter1, TParameter2>(object sender, ValueEventArgs<TValue, TParameter1, TParameter2> e);
}

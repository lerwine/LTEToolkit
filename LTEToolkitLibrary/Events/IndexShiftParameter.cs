namespace Erwine.Leonard.T.Toolkit.Events
{
    public struct IndexShiftParameter
    {
        private int _startIndex;
        private int _delta;
        private int _length;

        public int StartIndex { get { return this._startIndex; } }

        public int Delta { get { return this._delta; } }

        public int Length { get { return this._length; } }

        public IndexShiftParameter(int startIndex, int delta, int length)
        {
            this._startIndex = startIndex;
            this._delta = delta;
            this._length = length;
        }
    }
}

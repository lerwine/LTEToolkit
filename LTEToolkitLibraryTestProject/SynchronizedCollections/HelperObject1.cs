namespace LTEToolkitLibraryTestProject.SynchronizedCollections
{
    public class HelperObject1
    {
        private static object _syncRoot = new object();
        private static int _index = 0;

        private int _id;

        public static void ResetCounter()
        {
            lock (HelperObject1._syncRoot)
                HelperObject1._index = 0;
        }

        public int Id { get { return this._id; } }

        public HelperObject1()
        {
            lock (HelperObject1._syncRoot)
            {
                this._id = HelperObject1._index;
                HelperObject1._index++;
            }
        }
    }
}

using System.Threading;

namespace Task05
{
    public class BSTCoarseGrained<T> : BST<T>
    {
        private Mutex coarseMutex = new Mutex( false);

        public override void Insert(int key, T value)
        {
            coarseMutex.WaitOne();
            root = UpdateTree(null, root, key, value);
            coarseMutex.ReleaseMutex();
        }
        
        public override T Search(int key)
        {
            coarseMutex.WaitOne();
            T result = base.Search(key);
            coarseMutex.ReleaseMutex();
            return result;
        }

        public override void Delete(int key)
        {
            coarseMutex.WaitOne();
            base.Delete(key);
            coarseMutex.ReleaseMutex();
        }
    }
}
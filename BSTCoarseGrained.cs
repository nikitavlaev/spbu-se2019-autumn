using System.Collections.Generic;
using System.Threading;

namespace Task5
{
    public class BSTCoarseGrained<T> : BST<T>
    {
        private Mutex coarseMutex = new Mutex(false);

        public BSTCoarseGrained()
        {
        }
        public BSTCoarseGrained(IEnumerable<(int, T)> elements)
        {
            foreach (var pair in elements)
            {
                Insert(pair.Item1, pair.Item2);
            }
        }

        public override void Insert(int key, T value)
        {
            coarseMutex.WaitOne();
            UpdateTree(null, root, key, value, true);
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
namespace Task05
{
    public class Node<T>
    {
        public int key;
        public T value;
        public Node<T> left;
        public Node<T> right;
        public Node<T> parent;
        public Node (Node<T> parent, int key, T value)
        {
            this.key = key;
            this.value = value;
            left = null;
            right = null;
            this.parent = parent;
        }
    }
}
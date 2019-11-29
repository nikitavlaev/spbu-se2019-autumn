using System;
using System.Collections.Generic;

namespace Task05
{
    public class BST<T>
    {
        private Node<T> root = null;

        public void Insert(int key, T value)
        {
            root = UpdateTree(null, root, key, value);
        }

        private Node<T> UpdateTree(Node<T> parent, Node<T> currentNode, int key, T value)
        {
            if (currentNode == null)
            {
                return new Node<T>(parent, key, value);
            }
            else
            {
                if (currentNode.key > key)
                {
                    currentNode.left = UpdateTree(currentNode, currentNode.left, key, value);
                    return currentNode;
                }
                else
                {
                    currentNode.right = UpdateTree(currentNode, currentNode.right, key, value);
                    return currentNode;
                }
            }
        }

        public T Search(int key)
        {
            var node = FindInTree(root, key);
            if (node == null)
            {
                return default(T);
            }
            else
            {
                return node.value;
            }
        }

        public void Delete(int key)
        {
            Node<T> node = FindInTree(root, key);
            if (node == null)
                return;


            var replacement = FindLeftMost(node.right);
            bool foundLeftMost = true;

            if (replacement == null)
            {
                replacement = FindRightMost(node.left);
                foundLeftMost = false;
            }

            if (replacement == null)
            {
                if (node.parent == null) //is root
                {
                    root = null;
                }
                else
                {
                    if (node.parent.left != null)
                    {
                        if (node.parent.left.key == node.key)
                        {
                            //left son
                            node.parent.left = null;
                        }
                        else
                        {
                            node.parent.right = null;
                        }
                    }
                    else node.parent.right = null;
                }
            }
            else
            {
                //replacement parent is not null, because it is not root
                //remove replacement and with fixing links
                
                if (replacement.parent.left != null)
                {
                    if (replacement.parent.left.key == replacement.key)
                    {
                        //left son
                        if (foundLeftMost)
                        {
                            replacement.parent.left = replacement.right;
                        }
                        else
                        {
                            replacement.parent.left = replacement.left;
                        }
                    }
                    else
                    {
                        if (foundLeftMost)
                        {
                            replacement.parent.right = replacement.right;
                        }
                        else
                        {
                            replacement.parent.right = replacement.left;
                        }
                    }
                }
                else
                {
                    if (foundLeftMost)
                    {
                        replacement.parent.right = replacement.right;
                    }
                    else
                    {
                        replacement.parent.right = replacement.left;
                    }
                }

                //insert as root
                if (node.parent == null)
                {
                    root.key = replacement.key;
                    root.value = replacement.value;
                }
                else
                {
                    node.key = replacement.key;
                    node.value = replacement.value;
                }
            }
        }

        private Node<T> FindRightMost(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.right == null)
            {
                return node;
            }

            return FindRightMost(node.right);
        }

        private Node<T> FindLeftMost(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.left == null)
            {
                return node;
            }

            return FindLeftMost(node.left);
        }

        private Node<T> FindInTree(Node<T> currentNode, int key)
        {
            if (currentNode == null)
            {
                return null;
            }

            if (currentNode.key == key)
            {
                return currentNode;
            }
            else
            {
                if (currentNode.key > key)
                {
                    return FindInTree(currentNode.left, key);
                }
                else
                {
                    return FindInTree(currentNode.right, key);
                }
            }
        }
    }
}
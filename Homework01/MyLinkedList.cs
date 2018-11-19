using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework01
{
    class MyLinkedList<T> : IEnumerator<T>, IEnumerable<T>
    {
        public int Count { get; private set; }
        Node<T> header;
        Node<T> tail;
        int position = -1;

        public MyLinkedList()
        {
            header = null;
            tail = header;
            Count = 0;
        }

        public MyLinkedList(T data)
        {
            header = new Node<T>(data, null, null);
            tail = header;
            Count++;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    //return default(T);
                    throw new IndexOutOfRangeException();
                }
                Node<T> currentNode = header;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                return currentNode.Data;
            }

            set
            {
                if (index < 0 || index >= Count)
                {
                    //return default(T);
                    throw new IndexOutOfRangeException();
                }
                Node<T> currentNode = header;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Data = value;
            }
        }

        public void Add(T data)
        {
            if (Count == 0)
            {
                header = new Node<T>(data, null, null);
                tail = header;
            }
            else
            {
                Node<T> newElement = new Node<T>(data, null, tail);
                tail.Next = newElement;
                tail = newElement;
            }
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            if (index == 0)
            {
                Node<T> oldHeader = header;
                header = oldHeader.Next;
                oldHeader.Next = null;
                Count--;
                return;
            }
            Node<T> currentNode = header;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
            Node<T> prevNode = currentNode.Prev;
            Node<T> nextNode = currentNode.Next;
            prevNode.Next = nextNode;
            nextNode.Prev = prevNode;
            currentNode.Prev = null;
            currentNode.Next = null;
            Count--;
        }

        public void InsertAt(int index, T value)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                Node<T> oldHeader = header;
                header = new Node<T>(value, oldHeader, null);
                oldHeader.Prev = header;
                Count++;
                return;
            }
            Node<T> currentNode = header;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
            Node<T> prevNode = currentNode.Prev;
            Node<T> newNode = new Node<T>(value, currentNode, prevNode);
            prevNode.Next = newNode;
            currentNode.Prev = newNode;
            Count++;
        }

        public T[] ToArray()
        {
            List<T> resultList = new List<T>();

            Node<T> currentNode = header;
            while (currentNode != null)
            {
                resultList.Add(currentNode.Data);
                currentNode = currentNode.Next;
            }

            return resultList.ToArray();
        }

        public T Current => this[position];

        object IEnumerator.Current => this[position];

        public void Dispose()
        {
            Reset();
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                if (position < Count - 1)
                {
                    position++;
                    yield return this[position];
                }
                else
                {
                    Reset();
                    yield break;
                }
            }
        }

        public bool MoveNext()
        {
            if (position < Count - 1)
            {
                position++;
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            position = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            while (true)
            {
                if (position < Count - 1)
                {
                    position++;
                    yield return this[position];
                }
                else
                {
                    Reset();
                    yield break;
                }
            }
        }


        public class Node<K>
        {
            public K Data { get; set; }
            public Node<K> Next { get; set; }
            public Node<K> Prev { get; set; }

            public Node (K data, Node<K> next, Node<K> prev)
            {
                Data = data;
                Next = next;
                Prev = prev;
            }
        }
    }
}

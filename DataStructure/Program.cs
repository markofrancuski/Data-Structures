using System;
using System.Collections.Generic;

namespace DataStructure
{
    class DataStructure
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var o1 = new object();
            var o2 = new object();
            var o3 = new object();
            var o4 = new object();
            var o5 = new object();

            MyList<object> objects = new MyList<object>(10);

            objects.Add(o1);
            objects.Add(o2);
            objects.Add(o3);

            var obj = objects[1];
            objects[2] = o5;

            MyLinkedList<object> myLinkedList = new MyLinkedList<object>();
  
        }

        protected class MyList<T> where T : class
        {

            public int Count { get; private set; } = 0;
            public int Capacity => _array.Length;
            private T[] _array;

            // Time complexity is O(n)
            private void Reallocate()
            {
                T[] newArray = new T[_array.Length * 2];

                for (int i = 0; i < _array.Length; i++)
                {
                    newArray[i] = _array[i];
                }

                _array = newArray;
            }
            public MyList(int size = 0)
            {
                if (size <= 0) throw new ArgumentException("Size must be positive");

                _array = new T[size];
                Count = 0;
            }

            public T this[int index]
            {
                get
                {
                    if (index >= Count) throw new IndexOutOfRangeException();
                    return _array[index];
                }
                set
                {
                    if (index >= Count) throw new IndexOutOfRangeException();
                    _array[index] = value;
                }
            }
            // Time complexity is O(1)
            public void Add(T newValue)
            {
                if (Count >= _array.Length) Reallocate();

                _array[Count] = newValue;

                Count++;
            }
            // Time complexity is O(n).
            public void Remove(T value)
            {
                int searchIndex = IndexOf(value);

                if (searchIndex == -1) return;

                RemoveAt(searchIndex);
            }
            // Time complexity is O(n/2).
            public void RemoveAt(int index)
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                for (int i = index; i < Count - 1; i++)
                {
                    _array[i] = _array[i + 1];
                }
            }
            // Time complexity is O(n/2).
            public int IndexOf(T value)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (_array[i] == value)
                    {
                        return i;
                    }
                }

                return -1;
            }
            // Time complexity is O(n + n/2).
            public void InsertAt(int index, T value)
            {
                if (Count >= _array.Length) Reallocate();

                // Example
                // [0, 1, 2, 3, 4, ?, ? , ? , ? , ? ]
                // [0, 1, 2, 3, 4, 4, ? , ? , ? , ? ]
                // [0, 1, 2, 3, 3, 4, ? , ? , ? , ? ]
                // [0, 1, 2, 2, 3, 4, ? , ? , ? , ? ]
                // [0, 1, x, 2, 3, 4, ? , ? , ? , ? ]
                for (int i = Count - 1; i >= index; i--)
                {
                    _array[i + 1] = _array[i];
                }

                _array[index] = value;
            }
            // Time complexity is O(n/2).
            public void Reverse()
            {
                for (int i = 0; i < Count / 2; i++)
                {
                    T temp = _array[i];
                    _array[i] = _array[Count - i - 1];
                    _array[Count - i - 1] = temp;
                }
            }

            // Time complexity is O(n).
            public MyList<T> GetRange(int startIndex, int count)
            {
                if (count <= 0 ||  startIndex < 0 || startIndex + count > _array.Length) throw new IndexOutOfRangeException();

                MyList<T> result = new MyList<T>(count);

                for (int i = startIndex; i < startIndex + count; i++)
                {
                    result.Add(_array[i]);
                }

                return result;
            }


            // Time complexity is O(2n).
            public static MyList<T> Merge(MyList<T> l1, MyList<T> l2)
            {
                MyList<T> result = new MyList<T>(l1.Count + l2.Count);

                for (int i = 0; i < l1.Count; i++)
                {
                    result.Add(l1[i]);
                }
                for (int i = 0; i < l2.Count; i++)
                {
                    result.Add(l2[i]);
                }

                return result;
            }
        }


        protected class MyLinkedList<T> where T : class
        {
            public class Node
            {
                public Node NextNode { get; private set; }
                public T Value { get; private set; }
                public Node()
                {
                    NextNode = null;
                }
                public Node(T value, Node next = null)
                {
                    Value = value;
                    NextNode = next;
                }

                public void SetNext(Node next)
                {
                    NextNode = next;
                }

                public void SetValue(T value)
                {
                    Value = value;
                }

            }

            public int Count { get; private set; } = 0;
            public Node First { get; private set; }
            public Node Last { get; private set; }

            public MyLinkedList(Node first = null, Node last = null)
            {
                First = first;
                Last = last;
            }

            public void AddFirst(T value)
            {
                if(First == null)
                {
                    Node currentNode = new Node();
                    currentNode.SetValue(value);
                    First = currentNode;
                    if(Last == null) Last = currentNode;
                }
                else
                {
                    Node newFirst = new Node();
                    newFirst.SetNext(First);
                    newFirst.SetValue(value);

                    First.SetNext(First.NextNode);
                }
                Count++;
            }
            public void RemoveFirst()
            {
                if (First == null) throw new Exception("First Node is null");
                if (Count <= 0) throw new Exception("List is empty! ");

                if(Count == 1)
                {
                    Count--;
                    First = Last = null;
                    return;
                }

                Node newFirst = First;
                First = newFirst.NextNode;
                First.SetNext(null);
                Count--;
            }

            public void AddLast(T value)
            {
                Node newNode = new Node(null, value);
                if(Last == null)
                {
                    Last = newNode;
                    Last.SetNext(newNode);

                    if (First == null) First = newNode;
                }
                else
                {
                    Last.SetNext(newNode);
                    Last = newNode;
                }
                Count++;
            }

            public void RemoveLast()
            {
                if (Last == null) throw new Exception("Last Node is null! ");
                if (Count <= 0) throw new Exception("List is empty! ");

                if(Count == 1)
                {
                    Count--;
                    First = Last = null;
                    return;
                }

                Node node = First;
                while(node.NextNode != Last)
                {
                    node = node.NextNode;
                }

                Last = node;
                Last.SetNext(null);

                Count--;

            }
        }
    
    }
}

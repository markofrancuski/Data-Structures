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
            //Time complexity is O(n/2).
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
            // Tiem complexity is O(n + n/2).
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
    }
}

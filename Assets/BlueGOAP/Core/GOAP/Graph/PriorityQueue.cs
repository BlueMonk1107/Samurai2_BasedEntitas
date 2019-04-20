
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueGOAP
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        IComparer<T> comparer;
        T[] heap;

        public int Count { get; private set; }

        public PriorityQueue() : this(null) { }
        public PriorityQueue(int capacity) : this(capacity, null) { }
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }

        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            this.heap = new T[capacity];
            Count = 0;
        }

        public void Push(T v)
        {
            if (Count >= heap.Length)
                Array.Resize(ref heap, Count * 2);

            heap[Count] = v;
            SiftUp(Count++);
        }

        public T Pop()
        {
            var v = Top();
            heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }

        public T Top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("优先队列为空");
        }

        private void SiftUp(int n)
        {
            var v = heap[n];
            for (var i = n / 2; n > 0 && comparer.Compare(v, heap[i]) > 0; n = i, i /= 2)
                heap[n] = heap[i];
            
            heap[n] = v;
        }

        private void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
                if (comparer.Compare(v, heap[n2]) >= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CustomEnumerator<T>(heap, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CustomEnumerator<T> : IEnumerator<T>
    {
        private int _index;
        private T[] _array;
        private int _count;

        public CustomEnumerator(T[] array,int count)
        {
            _count = count;
            _array = array;
            Reset();
        }

        public bool MoveNext()
        {
            _index++;
            return _index < _count;
        }

        public void Reset()
        {
            _index = -1;
        }

        public T Current
        {
            get
            {
                if (_index >= 0 && _index < _count)
                {
                    return _array[_index];
                }
                else
                {
                    return default(T);
                }
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            Reset();
            _array = null;
        }
    }
}

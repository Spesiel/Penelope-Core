﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penelope.Collections
{
    public class LastInFirstOut<T>
    {
        #region Fields + Properties

        public int Count => _Count;
        public int PeakCount => _PeakCount;
        private T[] _Array;
        private int _Count;
        private int _GrowthRate;
        private int _PeakCount;

        #endregion Fields + Properties

        #region To show the full queue

        public ReadOnlyCollection<T> Stack
        {
            get
            {
                T[] result = new T[Count];
                Array.Copy(_Array, result, Count);
                return new ReadOnlyCollection<T>(result);
            }
        }

        #endregion To show the full queue

        #region Delegates + Events

        public event EventHandler<EventArgs> Added = delegate { };

        public event EventHandler<EventArgs> Empty = delegate { };

        #endregion Delegates + Events

        #region Constructors

        public LastInFirstOut() : this(16, 4, null)
        {
        }

        public LastInFirstOut(int capacity) : this(capacity, 4, null)
        {
        }

        public LastInFirstOut(int capacity, int growthRate) : this(capacity, growthRate, null)
        {
        }

        public LastInFirstOut(ICollection<T> collection) : this(0, 4, collection)
        {
        }

        public LastInFirstOut(ICollection<T> collection, int growthRate) : this(0, growthRate, collection)
        {
        }

        public LastInFirstOut(int capacity, int growthRate, ICollection<T> collection)
        {
            if (collection != null) capacity = _Count = collection.Count;
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            if (growthRate < 1) throw new ArgumentOutOfRangeException(nameof(growthRate));

            _Array = new T[capacity];
            _GrowthRate = growthRate;

            if (collection != null)
            {
                collection.CopyTo(_Array, 0);
            }
            else
            {
                _Count = 0;
            }
        }

        #endregion Constructors

        #region Queuing and Removing

        public T Pop()
        {
            if (Count == 0)
            {
                return default(T);
            }

            T ans = _Array[--_Count];
            if (Count == 0) Empty(this, new EventArgs());

            if (_Array.Length - Count >= _GrowthRate + 2)
            {
                ResizeGrowOrShrink();
            }

            return ans;
        }

        public void Push(T value)
        {
            if (_Array.Length - Count <= _GrowthRate + 2)
            {
                ResizeGrowOrShrink();
            }

            _Array[_Count++] = value;
            if (Count > PeakCount) _PeakCount = Count;

            Added(this, new EventArgs());
        }

        public bool Remove(T value)
        {
            bool ans = false;

            int index = -1;
            for (int i = 0; i < Count; i++)
            {
                if (_Array[i].Equals(value))
                {
                    index = i;
                    ans = true;
                    break;
                }
            }

            Array.Copy(_Array, index + 1, _Array, index, (--_Count) - index);
            if (Count == 0) Empty(this, new EventArgs());

            return ans;
        }

        #endregion Queuing and Removing

        #region Peeking

        public T Peek() => Count > 0 ? _Array[Count - 1] : default(T);

        #endregion Peeking

        #region SubArray

        private T[] ResizeGrowOrShrink()
        {
            T[] result = new T[Count + _GrowthRate];
            Array.Copy(_Array, result, Count);
            _Array = result;
            return result;
        }

        #endregion SubArray
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EKRLib
{
    class CollectionEnumerator<T> : IEnumerator<T> where T : Item
    {
        private List<T> items;
        private int position = -1;

        public CollectionEnumerator(List<T> items)
        {
            this.items = items.Where(x => Math.Abs(x.Weight) > 0.001).ToList();
        }
        
        public T Current
        {
            get
            {
                try
                {
                    return items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;
        
        public bool MoveNext() => ++position < items.Count;

        public void Reset()
        {
            position = -1;
        }
        
        public void Dispose()
        {
        }
    }
}
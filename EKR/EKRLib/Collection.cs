using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EKRLib
{
    [DataContract]
    public class Collection<T> : IEnumerable<T> where T : Item
    {
        [DataMember]
        private List<T> items;

        public Collection()
        {
            items = new List<T>();
        }

        public void Add(T item) => items.Add(item);
        
        public IEnumerator<T> GetEnumerator() => new CollectionEnumerator<T>(items);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
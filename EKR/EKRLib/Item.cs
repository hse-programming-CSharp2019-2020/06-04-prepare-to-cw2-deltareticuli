using System;
using System.Runtime.Serialization;

namespace EKRLib
{
    [DataContract]
    public class Item : IComparable<Item>
    {
        private double _weight;

        [DataMember]
        public double Weight
        {
            get => _weight;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Отрицательный параметр Weight.");
                _weight = value;
            }
        }

        public static explicit operator double(Item item) => item.Weight;

        public override string ToString() => $"Weight: {Weight:f2}";
        public int CompareTo(Item other) => Weight.CompareTo(other.Weight);
    }
}
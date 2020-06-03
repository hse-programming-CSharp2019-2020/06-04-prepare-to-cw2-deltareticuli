using System;
using System.Runtime.Serialization;

namespace EKRLib
{
    [DataContract]
    public class Box : Item
    {
        private double _a;
        private double _b;
        private double _c;

        [DataMember]
        public double A
        {
            get => _a;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Отрицательный параметр A.");
                _a = value;
            }
        }

        [DataMember]
        public double B
        {
            get => _b;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Отрицательный параметр B.");
                _b = value;
            }
        }

        [DataMember]
        public double C
        {
            get => _c;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Отрицательный параметр C.");
                _c = value;
            }
        }

        public double GetLongestSideSize() => Math.Max(A, Math.Max(B, C));
        public override string ToString() => $"{base.ToString()} A: {A:f2} B: {B:f2} C: {C:f2}";
    }
}
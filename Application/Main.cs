using System;

namespace MemoryDemo
{
    // Value type
    public struct ValueType
    {
        public int Value;
    }

    // Reference type
    public class ReferenceType
    {
        public int Value;
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Value type demo
            ValueType value1 = new ValueType { Value = 42 };
            Console.WriteLine($"Value1: {value1.Value}"); // 42

            PassByValue(value1);
            Console.WriteLine($"Value1 after PassByValue: {value1.Value}"); // 42

            // Reference type demo
            ReferenceType ref1 = new ReferenceType { Value = 42 };
            Console.WriteLine($"Ref1: {ref1.Value}"); // 42

            PassByReference(ref ref1);
            Console.WriteLine($"Ref1 after PassByReference: {ref1.Value}"); // 100

            // Stack vs Heap demo
            ValueType valueOnStack;  // Value type on stack
            ReferenceType refOnHeap = new ReferenceType(); // Reference type on heap

            // Garbage Collector demo
            ReferenceType refForGC = new ReferenceType();
            refForGC = null; // Make the object eligible for GC
            GC.Collect(); // Request a garbage collection
        }

        public static void PassByValue(ValueType value)
        {
            value.Value = 100;
            Console.WriteLine($"Value in PassByValue: {value.Value}"); // 100
        }

        public static void PassByReference(ref ReferenceType refParam)
        {
            refParam.Value = 100;
            Console.WriteLine($"RefParam in PassByReference: {refParam.Value}"); // 100
        }
    }
}

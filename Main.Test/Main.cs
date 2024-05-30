using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MemoryDemo;
using System.Collections.Generic;

namespace Main.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PassByValue_ShouldNotModifyOriginalValue()
        {
            MemoryDemo.ValueType value = new MemoryDemo.ValueType { Value = 42 };
            MemoryDemo.Program.PassByValue(value);
            Assert.AreEqual(42, value.Value);
        }

        [TestMethod]
        public void ValueTypeInstance_ShouldBeCreatedOnStack()
        {
            long initialHeapSize = GC.GetTotalMemory(true);

            MemoryDemo.ValueType value;
            value = new MemoryDemo.ValueType();

            long finalHeapSize = GC.GetTotalMemory(true);

            Assert.AreEqual(initialHeapSize, finalHeapSize, "ValueType instances should be created on the stack, not the heap.");
        }

        [TestMethod]
        public void PassByReference_ShouldModifyOriginalValue()
        {
            MemoryDemo.ReferenceType refType = new MemoryDemo.ReferenceType { Value = 42 };
            MemoryDemo.Program.PassByReference(ref refType);
            Assert.AreEqual(100, refType.Value);
        }

        [TestMethod]
        public void ReferenceTypeInstance_ShouldBeCreatedOnHeap()
        {
            long initialHeapSize = GC.GetTotalMemory(true);

            MemoryDemo.ReferenceType refType = new MemoryDemo.ReferenceType();

            long finalHeapSize = GC.GetTotalMemory(true);

            Assert.IsTrue(finalHeapSize > initialHeapSize, "ReferenceType instances should be created on the heap.");
        }

        [TestMethod]
        public void GarbageCollector_ShouldCollectUnusedObjects()
        {
            List<MemoryDemo.ReferenceType> objects = new List<MemoryDemo.ReferenceType>();

            for (int i = 0; i < 1000; i++)
            {
                objects.Add(new MemoryDemo.ReferenceType());
            }

            long heapSizeBeforeGC = GC.GetTotalMemory(true);

            objects.Clear();
            GC.Collect();
            long heapSizeAfterGC = GC.GetTotalMemory(true);

            Assert.IsTrue(heapSizeAfterGC < heapSizeBeforeGC, "Heap size should decrease after garbage collection.");
        }
    }
}

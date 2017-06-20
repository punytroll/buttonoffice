using System;
using System.Diagnostics;

namespace ButtonOffice.UnitTest
{
    [TestCollection]
    internal class PriorityQueue
    {
        [Test]
        internal static void Test_001_Empty()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueue<Object, Single>();

            Debug.Assert(ReferencePriorityQueue.Count == 0);
        }

        [Test]
        internal static void Test_002_EmptyDequeuesNull()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueue<Object, Single>();

            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
        }

        [Test]
        internal static void Test_003_EnqueueObject()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueue<Object, Single>();
            var First = new Object();

            ReferencePriorityQueue.Enqueue(First, 1.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 1);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 0);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
        }

        [Test]
        internal static void Test_004_EnqueueTwoObjects()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueue<Object, Single>();
            var First = new Object();
            var Second = new Object();

            ReferencePriorityQueue.Enqueue(First, 1.0f);
            ReferencePriorityQueue.Enqueue(Second, 2.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Second);
            Debug.Assert(ReferencePriorityQueue.Count == 1);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 0);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
        }

        [Test]
        internal static void Test_005_EnqueueTwoObjects()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueue<Object, Single>();
            var First = new Object();
            var Second = new Object();

            ReferencePriorityQueue.Enqueue(Second, 2.0f);
            ReferencePriorityQueue.Enqueue(First, 1.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Second);
            Debug.Assert(ReferencePriorityQueue.Count == 1);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 0);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
        }
    }
}

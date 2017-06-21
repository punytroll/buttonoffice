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
            var ReferencePriorityQueue = new ReferencePriorityQueueByList<Object, Single>();

            Debug.Assert(ReferencePriorityQueue.Count == 0);
        }

        [Test]
        internal static void Test_002_EmptyDequeuesNull()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueueByList<Object, Single>();

            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
        }

        [Test]
        internal static void Test_003_EnqueueObject()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueueByList<Object, Single>();
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
            var ReferencePriorityQueue = new ReferencePriorityQueueByList<Object, Single>();
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
            var ReferencePriorityQueue = new ReferencePriorityQueueByList<Object, Single>();
            var First = new Object();
            var Second = new Object();

            ReferencePriorityQueue.Enqueue(First, 2.0f);
            ReferencePriorityQueue.Enqueue(Second, 1.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 1);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Second);
            Debug.Assert(ReferencePriorityQueue.Count == 0);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
        }

        [Test]
        internal static void Test_005_EnqueueDequeueInterleaved()
        {
            var ReferencePriorityQueue = new ReferencePriorityQueueByList<Object, Single>();
            var First = new Object();
            var Second = new Object();
            var Third = new Object();
            var Fourth = new Object();
            var Fifth = new Object();
            var Sixth = new Object();

            ReferencePriorityQueue.Enqueue(First, 5.0f);
            ReferencePriorityQueue.Enqueue(Second, 3.0f);
            ReferencePriorityQueue.Enqueue(Third, 1.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 3);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            ReferencePriorityQueue.Enqueue(First, 6.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 3);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Second);
            Debug.Assert(ReferencePriorityQueue.Count == 1);
            ReferencePriorityQueue.Enqueue(First, 2.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 1);
            ReferencePriorityQueue.Enqueue(First, 2.0f);
            ReferencePriorityQueue.Enqueue(Second, 1.4f);
            Debug.Assert(ReferencePriorityQueue.Count == 3);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == First);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            ReferencePriorityQueue.Enqueue(Fourth, 0.4f);
            Debug.Assert(ReferencePriorityQueue.Count == 3);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Second);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            ReferencePriorityQueue.Enqueue(Fifth, 0.3f);
            ReferencePriorityQueue.Enqueue(Sixth, 0.0f);
            Debug.Assert(ReferencePriorityQueue.Count == 4);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Third);
            Debug.Assert(ReferencePriorityQueue.Count == 3);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Fourth);
            Debug.Assert(ReferencePriorityQueue.Count == 2);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Fifth);
            Debug.Assert(ReferencePriorityQueue.Count == 1);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == Sixth);
            Debug.Assert(ReferencePriorityQueue.Count == 0);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
            Debug.Assert(ReferencePriorityQueue.Dequeue() == null);
        }
    }
}

using System;
using System.Collections.Generic;

namespace ButtonOffice
{
    public class ReferencePriorityQueue<ItemType, PriorityType>
        where ItemType : class
        where PriorityType : IComparable<PriorityType>
    {
        private class Node
        {
            public ItemType Item;
            public PriorityType Priority;
        }

        private readonly List<Node> _Nodes;
        private readonly IComparer<PriorityType> _Comparer;

        public Int32 Count => _Nodes.Count;

        public ReferencePriorityQueue()
            : this(Comparer<PriorityType>.Default)
        {
        }

        public ReferencePriorityQueue(IComparer<PriorityType> Comparer)
        {
            _Nodes = new List<Node>();
            _Comparer = Comparer;
        }

        public void Enqueue(ItemType Item, PriorityType Priority)
        {
            _Nodes.Add(new Node
                       {
                           Item = Item,
                           Priority = Priority
                       });
        }

        public ItemType Dequeue()
        {
            Node Best = null;
            Int32? BestIndex = null;

            for(var Index = 0; Index < _Nodes.Count; ++Index)
            {
                var Node = _Nodes[Index];

                if((Best == null) || (_Comparer.Compare(Node.Priority, Best.Priority) > 0))
                {
                    Best = Node;
                    BestIndex = Index;
                }
            }
            if(Best != null)
            {
                _Nodes.RemoveAt(BestIndex.Value);

                return Best.Item;
            }
            else
            {
                return null;
            }
        }
    }
}

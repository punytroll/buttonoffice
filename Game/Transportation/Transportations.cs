using ButtonOffice.AI.Goals;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ButtonOffice.Transportation
{
    internal delegate Goal CreateUseGoalDelegate(Edge Edge);
    internal delegate TravelAction CreateTravelActionDelegate(Edge Edge);

    internal class CostPriority : IComparer<Double>
    {
        Int32 IComparer<Double>.Compare(Double One, Double Two)
        {
            if(One < Two)
            {
                return 1;
            }
            else if(One > Two)
            {
                return -1;
            }
            
            return 0;
        }
    }
    
    internal class Transportation
    {
        private readonly List<Node> _Nodes;
        
        internal Transportation()
        {
            _Nodes = new List<Node>();
        }
        
        internal Goal CreateWalkOnSameFloorGoal(Edge Edge)
        {
            Debug.Assert(Edge != null);
            Debug.Assert(Edge.To != null);
            
            var Result = new WalkOnSameFloor();
            
            Result.SetX(Edge.To.X);
            
            return Result;
        }
        
        internal TravelAction CreateWalkOnSameFloorTravelAction(Edge Edge)
        {
            Debug.Assert(Edge != null);
            Debug.Assert(Edge.To != null);
            
            var Result = new TravelActionWalkOnSameFloor();
            
            Result.X = Edge.To.X;
            
            return Result;
        }
        
        internal void AddNode(Node NewNode)
        {
            Node LeftNode = null;
            Node RightNode = null;
            
            foreach(var Node in _Nodes)
            {
                if(Node.Floor == NewNode.Floor)
                {
                    if(Node.X < NewNode.X)
                    {
                        if((LeftNode == null) || (LeftNode.X < Node.X))
                        {
                            LeftNode = Node;
                        }
                    }
                    else if(Node.X > NewNode.X)
                    {
                        if((RightNode == null) || (RightNode.X > Node.X))
                        {
                            RightNode = Node;
                        }
                    }
                }
            }
            if((LeftNode != null) && (RightNode != null))
            {
                Edge.RemoveEdge(LeftNode, RightNode);
                Edge.RemoveEdge(RightNode, LeftNode);
            }
            if(LeftNode != null)
            {
                Edge.AddEdge(LeftNode, NewNode, NewNode.X - LeftNode.X, CreateWalkOnSameFloorGoal, CreateWalkOnSameFloorTravelAction);
                Edge.AddEdge(NewNode, LeftNode, NewNode.X - LeftNode.X, CreateWalkOnSameFloorGoal, CreateWalkOnSameFloorTravelAction);
            }
            if(RightNode != null)
            {
                Edge.AddEdge(RightNode, NewNode, RightNode.X - NewNode.X, CreateWalkOnSameFloorGoal, CreateWalkOnSameFloorTravelAction);
                Edge.AddEdge(NewNode, RightNode, RightNode.X - NewNode.X, CreateWalkOnSameFloorGoal, CreateWalkOnSameFloorTravelAction);
            }
            _Nodes.Add(NewNode);
        }
        
        internal void RemoveNode(Node DeleteNode)
        {
            _Nodes.Remove(DeleteNode);
            
            Node LeftNode = null;
            Node RightNode = null;
            foreach(var Node in _Nodes)
            {
                if(Node.Floor == DeleteNode.Floor)
                {
                    if(Node.X < DeleteNode.X)
                    {
                        if((LeftNode == null) || (LeftNode.X < Node.X))
                        {
                            LeftNode = Node;
                        }
                    }
                    else if(Node.X > DeleteNode.X)
                    {
                        if((RightNode == null) || (RightNode.X > Node.X))
                        {
                            RightNode = Node;
                        }
                    }
                }
            }
            if(LeftNode != null)
            {
                Edge.RemoveEdge(LeftNode, DeleteNode);
                Edge.RemoveEdge(DeleteNode, LeftNode);
            }
            if(RightNode != null)
            {
                Edge.RemoveEdge(RightNode, DeleteNode);
                Edge.RemoveEdge(DeleteNode, RightNode);
            }
            if((LeftNode != null) && (RightNode != null))
            {
                Edge.AddEdge(LeftNode, RightNode, RightNode.X - LeftNode.X, CreateWalkOnSameFloorGoal, CreateWalkOnSameFloorTravelAction);
                Edge.AddEdge(RightNode, LeftNode, RightNode.X - LeftNode.X, CreateWalkOnSameFloorGoal, CreateWalkOnSameFloorTravelAction);
            }
        }
        
        private Dictionary<Node, Pair<Edge, Double>> _VisitNodes(Node FromNode, Node ToNode)
        {
            var VisitedNodes = new Dictionary<Node, Pair<Edge, Double>>();
            var FrontierPaths = new ReferencePriorityQueueByList<Edge, Double>(new CostPriority());
            
            foreach(var OutgoingEdge in FromNode.OutgoingEdges)
            {
                FrontierPaths.Enqueue(OutgoingEdge, OutgoingEdge.Weight);
            }
            while(FrontierPaths.Count > 0)
            {
                var FrontierEdge = FrontierPaths.Dequeue();
                
                if(VisitedNodes.ContainsKey(FrontierEdge.First.To) == false)
                {
                    VisitedNodes.Add(FrontierEdge.First.To, new Pair<Edge, Double>(FrontierEdge.First, FrontierEdge.Second));
                    if(FrontierEdge.First.To == ToNode)
                    {
                        break;
                    }
                    else
                    {
                        foreach(var Edge in FrontierEdge.First.To.OutgoingEdges)
                        {
                            FrontierPaths.Enqueue(Edge, FrontierEdge.Second + Edge.Weight);
                        }
                    }
                }
            }
            
            return VisitedNodes;
        }
        
        internal List<Edge> GetShortestPath(Vector2 FromLocation, Vector2 ToLocation)
        {
            var FromNode = new Node(FromLocation.X, FromLocation.Y.GetNearestInt32());
            
            AddNode(FromNode);
            
            var ToNode = new Node(ToLocation.X, ToLocation.Y.GetNearestInt32());
            
            AddNode(ToNode);
            
            var VisitedNodes = _VisitNodes(FromNode, ToNode);
            
            List<Edge> Result = null;
            
            if(VisitedNodes.ContainsKey(ToNode) == true)
            {
                Result = new List<Edge>();
                
                var BackwardNode = ToNode;
                
                while(BackwardNode != FromNode)
                {
                    Debug.Assert(VisitedNodes.ContainsKey(BackwardNode) == true);
                    Result.Insert(0, VisitedNodes[BackwardNode].First);
                    BackwardNode = VisitedNodes[BackwardNode].First.From;
                }
            }
            RemoveNode(ToNode);
            RemoveNode(FromNode);
            
            return Result;
        }
        
        internal Double? GetShortestPathPathWeight(Vector2 FromLocation, Vector2 ToLocation)
        {
            var FromNode = new Node(FromLocation.X, FromLocation.Y.GetNearestInt32());
            
            AddNode(FromNode);
            
            var ToNode = new Node(ToLocation.X, ToLocation.Y.GetNearestInt32());
            
            AddNode(ToNode);
            
            var VisitedNodes = _VisitNodes(FromNode, ToNode);
            Double? Result = null;
            
            if(VisitedNodes.ContainsKey(ToNode) == true)
            {
                Result = VisitedNodes[ToNode].Second;
            }
            RemoveNode(ToNode);
            RemoveNode(FromNode);
            
            return Result;
        }
    }
}

using ButtonOffice.AI.Goals;
using System;
using System.Diagnostics;

namespace ButtonOffice.Transportation
{
    internal class Edge
    {
        private CreateUseGoalDelegate _CreateUseGoalFunction;
        private CreateTravelActionDelegate _CreateTravelActionFunction;
        internal Node From;
        internal Node To;
        internal Double Weight;
        
        internal Goal CreateUseGoal()
        {
            Debug.Assert(_CreateUseGoalFunction != null);
            
            return _CreateUseGoalFunction(this);
        }
        
        internal TravelAction CreateTravelAction()
        {
            Debug.Assert(_CreateTravelActionFunction != null);
            
            return _CreateTravelActionFunction(this);
        }
        
        internal static void AddEdge(Node FromNode, Node ToNode, Double Weight, CreateUseGoalDelegate CreateUseGoalFunction, CreateTravelActionDelegate CreateTravelActionFunction)
        {
            var Edge = new Edge();
            
            Edge._CreateUseGoalFunction = CreateUseGoalFunction;
            Edge._CreateTravelActionFunction = CreateTravelActionFunction;
            Edge.From = FromNode;
            Edge.To = ToNode;
            Edge.Weight = Weight;
            FromNode.OutgoingEdges.Add(Edge);
            ToNode.IncomingEdges.Add(Edge);
        }
        
        internal static void RemoveEdge(Node FromNode, Node ToNode)
        {
            for(var EdgeIndex = 0; EdgeIndex < ToNode.IncomingEdges.Count; ++EdgeIndex)
            {
                var Edge = ToNode.IncomingEdges[EdgeIndex];
                
                if(Edge.From == FromNode)
                {
                    Debug.Assert(Edge.To == ToNode);
                    ToNode.IncomingEdges.RemoveAt(EdgeIndex);
                    
                    break;
                }
            }
            for(var EdgeIndex = 0; EdgeIndex < FromNode.OutgoingEdges.Count; ++EdgeIndex)
            {
                var Edge = FromNode.OutgoingEdges[EdgeIndex];
                
                if(Edge.To == ToNode)
                {
                    Debug.Assert(Edge.From == FromNode);
                    FromNode.OutgoingEdges.RemoveAt(EdgeIndex);
                    
                    break;
                }
            }
        }
    }
}

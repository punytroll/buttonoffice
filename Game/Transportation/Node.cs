using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ButtonOffice.Transportation
{
	internal class Node
	{
		internal Int32 _Floor;
		internal List<Edge> _IncomingEdges;
		internal List<Edge> _OutgoingEdges;
		internal Double _X;

		internal Int32 Floor => _Floor;

		internal List<Edge> OutgoingEdges => _OutgoingEdges;

		internal Double X => _X;

		internal Node(Double X, Int32 Floor)
		{
			_Floor = Floor;
			_IncomingEdges = new List<Edge>();
			_OutgoingEdges = new List<Edge>();
			_X = X;
		}

		internal void AddEdgeTo(Node Node, Double Weight, CreateUseGoalDelegate CreateUseGoalFunction)
		{
			var Edge = new Edge();

			Edge.CreateUseGoalFunction = CreateUseGoalFunction;
			Edge.From = this;
			Edge.To = Node;
			Edge.Weight = Weight;
			_OutgoingEdges.Add(Edge);
			Node._IncomingEdges.Add(Edge);
		}

		internal void AddEdgeFrom(Node Node, Double Weight, CreateUseGoalDelegate CreateUseGoalFunction)
		{
			var Edge = new Edge();

			Edge.CreateUseGoalFunction = CreateUseGoalFunction;
			Edge.From = Node;
			Edge.To = this;
			Edge.Weight = Weight;
			_IncomingEdges.Add(Edge);
			Node._OutgoingEdges.Add(Edge);
		}

		internal void RemoveEdgeFrom(Node Node)
		{
			for(var EdgeIndex = 0; EdgeIndex < _IncomingEdges.Count; ++EdgeIndex)
			{
				var Edge = _IncomingEdges[EdgeIndex];

				if(Edge.From == Node)
				{
					Debug.Assert(Edge.To == this);
					_IncomingEdges.RemoveAt(EdgeIndex);

					break;
				}
			}
			for(var EdgeIndex = 0; EdgeIndex < Node._OutgoingEdges.Count; ++EdgeIndex)
			{
				var Edge = Node._OutgoingEdges[EdgeIndex];

				if(Edge.To == this)
				{
					Debug.Assert(Edge.From == Node);
					Node._OutgoingEdges.RemoveAt(EdgeIndex);

					break;
				}
			}
		}

		internal void RemoveEdgeTo(Node Node)
		{
			for(var EdgeIndex = 0; EdgeIndex < _OutgoingEdges.Count; ++EdgeIndex)
			{
				var Edge = _OutgoingEdges[EdgeIndex];

				if(Edge.To == Node)
				{
					Debug.Assert(Edge.From == this);
					_OutgoingEdges.RemoveAt(EdgeIndex);

					break;
				}
			}
			for(var EdgeIndex = 0; EdgeIndex < Node._IncomingEdges.Count; ++EdgeIndex)
			{
				var Edge = Node._IncomingEdges[EdgeIndex];

				if(Edge.From == this)
				{
					Debug.Assert(Edge.To == Node);
					Node._IncomingEdges.RemoveAt(EdgeIndex);

					break;
				}
			}
		}
	}
}

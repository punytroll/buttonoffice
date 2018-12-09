using ButtonOffice.Goals;
using ButtonOffice.Transportation;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ButtonOffice
{
    public class Stairs : Building
    {
        private readonly List<Node> _TransportationNodes;

        public Stairs()
        {
            _BackgroundColor = Data.StairsBackgroundColor;
            _BorderColor = Data.StairsBorderColor;
            _TransportationNodes = new List<Node>();
        }

        public override Boolean CanDestroy()
        {
            return false;
        }

        private Goal _CreateUseStairsGoal(Edge Edge)
        {
            Debug.Assert(Edge != null);
            Debug.Assert(Edge.To != null);

            var Result = new UseStairs();

            Result.SetStairs(this);
            Result.SetTargetFloor(Edge.To.Floor);

            return Result;
        }

        public void ExpandDownwards(Game Game)
        {
            if(Game.CanBuild(Data.StairsExpansionCost, Left, Width, Floor - 1.0f, 1.0f) == true)
            {
                Floor = Floor - 1.0;
                Height = Height + 1.0;
                Game.UpdateBuilding(Data.StairsExpansionCost, this);

                var NewNode = new Node(Left + Width / 2.0, Floor.GetNearestInt32());
                Node LowestNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if((LowestNode == null) || (Node.Floor < LowestNode.Floor))
                    {
                        LowestNode = Node;
                    }
                }
                Debug.Assert(LowestNode != null);
                LowestNode.AddEdgeTo(NewNode, Data.StairsWeightDownwards, _CreateUseStairsGoal);
                LowestNode.AddEdgeFrom(NewNode, Data.StairsWeightUpwards, _CreateUseStairsGoal);
                _TransportationNodes.Add(NewNode);
                Game.Transportation.AddNode(NewNode);
            }
        }

        public void ExpandUpwards(Game Game)
        {
            if(Game.CanBuild(Data.StairsExpansionCost, Left, Width, Floor + Height, 1.0) == true)
            {
                Height = Height + 1.0;
                Game.UpdateBuilding(Data.StairsExpansionCost, this);

                var NewNode = new Node(Left + Width / 2.0, (Floor + Height - 1.0).GetNearestInt32());
                Node HighestNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if((HighestNode == null) || (Node.Floor > HighestNode.Floor))
                    {
                        HighestNode = Node;
                    }
                }
                Debug.Assert(HighestNode != null);
                HighestNode.AddEdgeTo(NewNode, Data.StairsWeightUpwards, _CreateUseStairsGoal);
                HighestNode.AddEdgeFrom(NewNode, Data.StairsWeightDownwards, _CreateUseStairsGoal);
                _TransportationNodes.Add(NewNode);
                Game.Transportation.AddNode(NewNode);
            }
        }

        public void UpdateTransportation(Game Game)
        {
            Node LowerNode = null;

            for(var FloorCurrent = Floor.GetNearestInt32(); FloorCurrent < (Floor + Height).GetNearestInt32(); ++FloorCurrent)
            {
                var NewNode = new Node(Left + Width / 2.0, FloorCurrent);

                if(LowerNode != null)
                {
                    LowerNode.AddEdgeTo(NewNode, Data.StairsWeightUpwards, _CreateUseStairsGoal);
                    LowerNode.AddEdgeFrom(NewNode, Data.StairsWeightDownwards, _CreateUseStairsGoal);
                }
                _TransportationNodes.Add(NewNode);
                Game.Transportation.AddNode(NewNode);
                LowerNode = NewNode;
            }
        }
    }
}

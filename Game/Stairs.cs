using ButtonOffice.Goals;
using ButtonOffice.Transportation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

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
            if(Game.CanBuild(Data.StairsExpansionCost, _Rectangle.X, _Rectangle.Width, _Rectangle.Y - 1.0f, 1.0f) == true)
            {
                SetY(GetY() - 1.0f);
                SetHeight(GetHeight() + 1.0f);
                Game.UpdateBuilding(Data.StairsExpansionCost, this);

                var NewNode = new Node(_Rectangle.X + _Rectangle.Width / 2.0, _Rectangle.Y.GetNearestInt32());
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
            if(Game.CanBuild(Data.StairsExpansionCost, _Rectangle.X, _Rectangle.Width, _Rectangle.Y + _Rectangle.Height, 1.0f) == true)
            {
                SetHeight(GetHeight() + 1.0f);
                Game.UpdateBuilding(Data.StairsExpansionCost, this);

                var NewNode = new Node(_Rectangle.X + _Rectangle.Width / 2.0, (_Rectangle.Y + _Rectangle.Height - 1.0).GetNearestInt32());
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

            for(var Floor = _Rectangle.Y.GetNearestInt32(); Floor < (_Rectangle.Y + _Rectangle.Height).GetNearestInt32(); ++Floor)
            {
                var NewNode = new Node(_Rectangle.X + _Rectangle.Width / 2.0, Floor);

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

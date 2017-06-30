using ButtonOffice.Goals;
using ButtonOffice.Transportation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public class Stairs : Building
    {
        private List<Node> _TransportationNodes;

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
            var ExpansionRectangle = new RectangleF(_Rectangle.X, _Rectangle.Y - 1.0f, _Rectangle.Width, 1.0f);

            if(Game.CanBuild(Data.StairsExpansionCost, ExpansionRectangle) == true)
            {
                SetY(GetY() - 1.0f);
                SetHeight(GetHeight() + 1.0f);
                Game.UpdateBuilding(Data.StairsExpansionCost, this);

                var NewNode = new Node(_Rectangle.X + _Rectangle.Width / 2.0, _Rectangle.Y.GetNearestInt32());

                Game.Transportation.AddNode(NewNode);

                Node LowestNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if(Node.Floor < LowestNode.Floor)
                    {
                        LowestNode = Node;
                    }
                }
                Debug.Assert(LowestNode != null);
                LowestNode.AddEdgeTo(NewNode, Data.StairsWeightDownwards, _CreateUseStairsGoal);
                LowestNode.AddEdgeFrom(NewNode, Data.StairsWeightUpwards, _CreateUseStairsGoal);
                _TransportationNodes.Add(NewNode);
            }
        }

        public void ExpandUpwards(Game Game)
        {
            var ExpansionRectangle = new RectangleF(_Rectangle.X, _Rectangle.Y + _Rectangle.Height, _Rectangle.Width, 1.0f);

            if(Game.CanBuild(Data.StairsExpansionCost, ExpansionRectangle) == true)
            {
                SetHeight(GetHeight() + 1.0f);
                Game.UpdateBuilding(Data.StairsExpansionCost, this);

                var NewNode = new Node(_Rectangle.X + _Rectangle.Width / 2.0, _Rectangle.Y.GetNearestInt32());

                Node HighestNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if(Node.Floor > HighestNode.Floor)
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

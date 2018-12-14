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

        public void AddLowestFloor(Game Game)
        {
            if(Game.CanBuild(Data.StairsAddFloorCost, Left, Width, Floor - 1.0f, 1.0f) == true)
            {
                Floor -= 1.0;
                Height += 1.0;
                Game.UpdateBuilding(Data.StairsAddFloorCost, this);

                var NewNode = new Node(Left + Width / 2.0, Floor.GetNearestInt32());
                Node LowestFloorNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if((LowestFloorNode == null) || (Node.Floor < LowestFloorNode.Floor))
                    {
                        LowestFloorNode = Node;
                    }
                }
                Debug.Assert(LowestFloorNode != null);
                Edge.AddEdge(LowestFloorNode, NewNode, Data.StairsWeightDownwards, _CreateUseStairsGoal);
                Edge.AddEdge(NewNode, LowestFloorNode, Data.StairsWeightUpwards, _CreateUseStairsGoal);
                _TransportationNodes.Add(NewNode);
                Game.Transportation.AddNode(NewNode);
            }
        }

        public void AddHighestFloor(Game Game)
        {
            if(Game.CanBuild(Data.StairsAddFloorCost, Left, Width, Floor + Height, 1.0) == true)
            {
                Height += 1.0;
                Game.UpdateBuilding(Data.StairsAddFloorCost, this);

                var NewNode = new Node(Left + Width / 2.0, (Floor + Height - 1.0).GetNearestInt32());
                Node HighestFloorNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if((HighestFloorNode == null) || (Node.Floor > HighestFloorNode.Floor))
                    {
                        HighestFloorNode = Node;
                    }
                }
                Debug.Assert(HighestFloorNode != null);
                Edge.AddEdge(HighestFloorNode, NewNode, Data.StairsWeightUpwards, _CreateUseStairsGoal);
                Edge.AddEdge(NewNode, HighestFloorNode, Data.StairsWeightDownwards, _CreateUseStairsGoal);
                _TransportationNodes.Add(NewNode);
                Game.Transportation.AddNode(NewNode);
            }
        }

        public void RemoveHighestFloor(Game Game)
        {
            if((Height > 2.0) && (Game.CanSpend(Data.StairsRemoveFloorCost) == true))
            {
                Height -= 1.0;
                Game.FreeSpace(Left, Width, Floor + Height, 1.0);
                Game.UpdateBuilding(Data.StairsRemoveFloorCost, this);

                Node HighestFloorNode = null;
                Node SecondHighestFloorNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if(HighestFloorNode == null)
                    {
                        HighestFloorNode = Node;
                    }
                    else if(SecondHighestFloorNode == null)
                    {
                        SecondHighestFloorNode = Node;
                    }
                    else if(Node.Floor > SecondHighestFloorNode.Floor)
                    {
                        if(Node.Floor > HighestFloorNode.Floor)
                        {
                            SecondHighestFloorNode = HighestFloorNode;
                            HighestFloorNode = Node;
                        }
                        else
                        {
                            SecondHighestFloorNode = Node;
                        }
                    }
                }
                Debug.Assert(HighestFloorNode != null);
                Debug.Assert(SecondHighestFloorNode != null);
                Edge.RemoveEdge(HighestFloorNode, SecondHighestFloorNode);
                Edge.RemoveEdge(SecondHighestFloorNode, HighestFloorNode);
                _TransportationNodes.Remove(HighestFloorNode);
                Game.Transportation.RemoveNode(HighestFloorNode);
            }
        }

        public void RemoveLowestFloor(Game Game)
        {
            if((Height > 2.0) && (Game.CanSpend(Data.StairsRemoveFloorCost) == true))
            {
                Floor += 1.0;
                Height -= 1.0;
                Game.FreeSpace(Left, Width, Floor - 1.0, 1.0);
                Game.UpdateBuilding(Data.StairsRemoveFloorCost, this);

                Node LowestFloorNode = null;
                Node SecondLowestFloorNode = null;

                foreach(var Node in _TransportationNodes)
                {
                    if(LowestFloorNode == null)
                    {
                        LowestFloorNode = Node;
                    }
                    else if(SecondLowestFloorNode == null)
                    {
                        SecondLowestFloorNode = Node;
                    }
                    else if(Node.Floor > SecondLowestFloorNode.Floor)
                    {
                        if(Node.Floor > LowestFloorNode.Floor)
                        {
                            SecondLowestFloorNode = LowestFloorNode;
                            LowestFloorNode = Node;
                        }
                        else
                        {
                            SecondLowestFloorNode = Node;
                        }
                    }
                }
                Debug.Assert(LowestFloorNode != null);
                Debug.Assert(SecondLowestFloorNode != null);
                Edge.RemoveEdge(LowestFloorNode, SecondLowestFloorNode);
                Edge.RemoveEdge(SecondLowestFloorNode, LowestFloorNode);
                _TransportationNodes.Remove(LowestFloorNode);
                Game.Transportation.RemoveNode(LowestFloorNode);
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
                    Edge.AddEdge(LowerNode, NewNode, Data.StairsWeightUpwards, _CreateUseStairsGoal);
                    Edge.AddEdge(NewNode, LowerNode, Data.StairsWeightDownwards, _CreateUseStairsGoal);
                }
                _TransportationNodes.Add(NewNode);
                Game.Transportation.AddNode(NewNode);
                LowerNode = NewNode;
            }
        }
    }
}

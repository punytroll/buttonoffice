using ButtonOffice.Goals;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ButtonOffice
{
    public class Stairs : Building
    {
        public Stairs()
        {
            _BackgroundColor = Data.StairsBackgroundColor;
            _BorderColor = Data.StairsBorderColor;
        }

        public List<Int32> GetFloors()
        {
            var Result = new List<Int32>();

            for(var Floor = _Rectangle.Y.GetNearestInt32(); Floor < (_Rectangle.Y + _Rectangle.Height).GetNearestInt32(); ++Floor)
            {
                Result.Add(Floor);
            }

            return Result;
        }

        public Goal CreateUseGoal(Double ToX, Double ToY)
        {
            var Result = new UseStairs();

            Result.SetStairs(this);
            Result.SetTargetFloor(ToY.GetNearestInt32());

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
            }
        }

        public void ExpandUpwards(Game Game)
        {
            var ExpansionRectangle = new RectangleF(_Rectangle.X, _Rectangle.Y + _Rectangle.Height, _Rectangle.Width, 1.0f);

            if(Game.CanBuild(Data.StairsExpansionCost, ExpansionRectangle) == true)
            {
                SetHeight(GetHeight() + 1.0f);
                Game.UpdateBuilding(Data.StairsExpansionCost, this);
            }
        }
    }
}

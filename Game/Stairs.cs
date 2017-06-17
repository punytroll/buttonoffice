using ButtonOffice.Goals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            for(var Floor = _Rectangle.Y.GetTruncatedAsInt32(); Floor < (_Rectangle.Y + _Rectangle.Height).GetTruncatedAsInt32(); ++Floor)
            {
                Console.WriteLine(Floor);
                Result.Add(Floor);
            }

            return Result;
        }

        public Goal CreateUseGoal()
        {
            var Result = new UseStairs();

            Result.SetStairs(this);

            return Result;
        }
    }
}

using ButtonOffice.Goals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public class Stairs : Building
    {
        public List<Int32> Floors
        {
            get
            {
                return new List<Int32>() { _Rectangle.Y.GetTruncatedAsInt32(), _Rectangle.Y.GetTruncatedAsInt32() + 1 };
            }
        }

        public Stairs()
        {
            _BackgroundColor = Data.StairsBackgroundColor;
            _BorderColor = Data.StairsBorderColor;
        }

        public Goal CreateUseGoal()
        {
            var Result = new UseStairs();

            Result.SetStairs(this);

            return Result;
        }
    }
}

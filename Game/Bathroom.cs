using System;
using System.Drawing;

namespace ButtonOffice
{
    public class Bathroom : Building
    {
        public Bathroom()
        {
            _BackgroundColor = Data.BathroomBackgroundColor;
            _BorderColor = Data.BathroomBorderColor;
        }
    }
}

using System;

namespace ButtonOffice
{
    public class Bathroom : Building
    {
        public Bathroom()
        {
            _BackgroundColor = Data.BathroomBackgroundColor;
            _BorderColor = Data.BathroomBorderColor;
        }

        public override Boolean CanDestroy()
        {
            return true;
        }
    }
}

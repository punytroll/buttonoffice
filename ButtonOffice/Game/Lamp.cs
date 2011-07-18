namespace ButtonOffice
{
    internal class Lamp
    {
        private System.Single _MinutesUntilBroken;
        private System.Drawing.RectangleF _Rectangle;

        public Lamp()
        {
            _Rectangle.Height = ButtonOffice.Data.LampHeight;
            _Rectangle.Width = ButtonOffice.Data.LampWidth;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Single GetMinutesUntilBroken()
        {
            return _MinutesUntilBroken;
        }

        public System.Drawing.RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public System.Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public System.Boolean IsBroken()
        {
            return _MinutesUntilBroken < 0.0f;
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(System.Single X, System.Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetMinutesUntilBroken(System.Single MinutesUntilBroken)
        {
            _MinutesUntilBroken = MinutesUntilBroken;
        }

        public void SetWidth(System.Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(System.Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(System.Single Y)
        {
            _Rectangle.Y = Y;
        }
    }
}

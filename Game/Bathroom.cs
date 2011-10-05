namespace ButtonOffice
{
    public class Bathroom : ButtonOffice.PersistentObject
    {
        private System.Drawing.Color _BackgroundColor;
        private System.Drawing.Color _BorderColor;
        private System.Drawing.RectangleF _Rectangle;

        public Bathroom()
        {
            _BackgroundColor = ButtonOffice.Data.BathroomBackgroundColor;
            _BorderColor = ButtonOffice.Data.BathroomBorderColor;
        }

        public System.Drawing.Color GetBackgroundColor()
        {
            return _BackgroundColor;
        }

        public System.Drawing.Color GetBorderColor()
        {
            return _BorderColor;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Drawing.PointF GetMidLocation()
        {
            return _Rectangle.GetMidPoint();
        }

        public System.Drawing.RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public System.Single GetRight()
        {
            return _Rectangle.Right;
        }

        public System.Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public System.Single GetX()
        {
            return _Rectangle.X;
        }

        public System.Single GetY()
        {
            return _Rectangle.Y;
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetRectangle(System.Drawing.RectangleF Rectangle)
        {
            _Rectangle = Rectangle;
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

        public virtual System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = GameSaver.CreateElement("bathroom");

            Result.AppendChild(GameSaver.CreateProperty("background-color", _BackgroundColor));
            Result.AppendChild(GameSaver.CreateProperty("border-color", _BorderColor));
            Result.AppendChild(GameSaver.CreateProperty("rectangle", _Rectangle));

            return Result;
        }

        public virtual void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            _BackgroundColor = GameLoader.LoadColorProperty(Element, "background-color");
            _BorderColor = GameLoader.LoadColorProperty(Element, "border-color");
            _Rectangle = GameLoader.LoadRectangleProperty(Element, "rectangle");
        }
    }
}

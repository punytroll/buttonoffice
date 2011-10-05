namespace ButtonOffice
{
    public class Lamp : ButtonOffice.PersistentObject
    {
        private System.Single _MinutesUntilBroken;
        private System.Drawing.RectangleF _Rectangle;

        public Lamp()
        {
            _MinutesUntilBroken = ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp);
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

        public System.Single GetX()
        {
            return _Rectangle.X;
        }

        public System.Single GetY()
        {
            return _Rectangle.Y;
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

        public virtual System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = GameSaver.CreateElement("lamp");

            Result.AppendChild(GameSaver.CreateProperty("minutes-until-broken", _MinutesUntilBroken));
            Result.AppendChild(GameSaver.CreateProperty("rectangle", _Rectangle));

            return Result;
        }

        public virtual void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            _MinutesUntilBroken = GameLoader.LoadSingleProperty(Element, "minutes-until-broken");
            _Rectangle = GameLoader.LoadRectangleProperty(Element, "rectangle");
        }
    }
}

namespace ButtonOffice
{
    internal class Desk : ButtonOffice.IPersistentObject
    {
        private ButtonOffice.Janitor _Janitor;
        private System.Single _MinutesUntilComputerBroken;
        private System.Drawing.RectangleF _Rectangle ;
        private ButtonOffice.Office _Office;
        private ButtonOffice.Person _Person;
        private System.Single _TrashLevel;

        public ButtonOffice.Janitor Janitor
        {
            get
            {
                return _Janitor;
            }
            set
            {
                _Janitor = value;
            }
        }

        public ButtonOffice.Office Office
        {
            get
            {
                return _Office;
            }
            set
            {
                _Office = value;
            }
        }

        public System.Single TrashLevel
        {
            get
            {
                return _TrashLevel;
            }
            set
            {
                _TrashLevel = value;
            }
        }

        public Desk()
        {
            _Rectangle.Height = ButtonOffice.Data.DeskHeight;
            _Rectangle.Width = ButtonOffice.Data.DeskWidth;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Drawing.PointF GetLocation()
        {
            return _Rectangle.Location;
        }

        public System.Drawing.PointF GetMidLocation()
        {
            return new System.Drawing.PointF(_Rectangle.X + _Rectangle.Width / 2.0f, _Rectangle.Y + _Rectangle.Height / 2.0f);
        }

        public System.Single GetMinutesUntilComputerBroken()
        {
            return _MinutesUntilComputerBroken;
        }

        public ButtonOffice.Person GetPerson()
        {
            return _Person;
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

        public System.Boolean IsComputerBroken()
        {
            return _MinutesUntilComputerBroken < 0.0f;
        }

        public System.Boolean IsFree()
        {
            return _Person == null;
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

        public void SetMinutesUntilComputerBroken(System.Single MinutesUntilComputerBroken)
        {
            _MinutesUntilComputerBroken = MinutesUntilComputerBroken;
        }

        public void SetPerson(ButtonOffice.Person Person)
        {
            if(Person == null)
            {
                System.Diagnostics.Debug.Assert(_Person != null);
                _Person = null;
            }
            else
            {
                System.Diagnostics.Debug.Assert(_Person == null);
                _Person = Person;
            }
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
            // save referenced objects
            GameSaver.Save(_Janitor);
            GameSaver.Save(_Office);
            GameSaver.Save(_Person);

            // save own properties
            System.Xml.XmlElement Result = GameSaver.CreateElement("desk");

            Result.AppendChild(GameSaver.CreateProperty("janitor-identifier", _Janitor));
            Result.AppendChild(GameSaver.CreateProperty("minutes-until-computer-broken", _MinutesUntilComputerBroken));
            Result.AppendChild(GameSaver.CreateProperty("office-identifier", _Office));
            Result.AppendChild(GameSaver.CreateProperty("person-identifier", _Person));
            Result.AppendChild(GameSaver.CreateProperty("rectangle", _Rectangle));
            Result.AppendChild(GameSaver.CreateProperty("trash-level", _TrashLevel));

            return Result;
        }
    }
}

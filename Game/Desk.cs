namespace ButtonOffice
{
    public class Desk : ButtonOffice.PersistentObject
    {
        private ButtonOffice.Computer _Computer;
        private ButtonOffice.Janitor _Janitor;
        private System.Drawing.RectangleF _Rectangle ;
        private ButtonOffice.Office _Office;
        private ButtonOffice.Person _Person;
        private System.Single _TrashLevel;

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
            _Computer = new ButtonOffice.Computer();
            _Rectangle.Height = ButtonOffice.Data.DeskHeight;
            _Rectangle.Width = ButtonOffice.Data.DeskWidth;
        }

        public ButtonOffice.Computer GetComputer()
        {
            return _Computer;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public ButtonOffice.Janitor GetJanitor()
        {
            return _Janitor;
        }

        public System.Drawing.PointF GetLocation()
        {
            return _Rectangle.Location;
        }

        public System.Drawing.PointF GetMidLocation()
        {
            return new System.Drawing.PointF(_Rectangle.X + _Rectangle.Width / 2.0f, _Rectangle.Y + _Rectangle.Height / 2.0f);
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

        public System.Boolean IsFree()
        {
            return _Person == null;
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetJanitor(ButtonOffice.Janitor Janitor)
        {
            _Janitor = Janitor;
        }

        public void SetLocation(System.Single X, System.Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
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

        public virtual void Save(ButtonOffice.GameSaver GameSaver, System.Xml.XmlElement Element)
        {
            Element.AppendChild(GameSaver.CreateProperty("computer", _Computer));
            Element.AppendChild(GameSaver.CreateProperty("janitor", _Janitor));
            Element.AppendChild(GameSaver.CreateProperty("office", _Office));
            Element.AppendChild(GameSaver.CreateProperty("person", _Person));
            Element.AppendChild(GameSaver.CreateProperty("rectangle", _Rectangle));
            Element.AppendChild(GameSaver.CreateProperty("trash-level", _TrashLevel));
        }

        public virtual void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            _Computer = GameLoader.LoadComputerProperty(Element, "computer");
            _Janitor = GameLoader.LoadJanitorProperty(Element, "janitor");
            _Office = GameLoader.LoadOfficeProperty(Element, "office");
            _Person = GameLoader.LoadPersonProperty(Element, "person");
            _Rectangle = GameLoader.LoadRectangleProperty(Element, "rectangle");
            _TrashLevel = GameLoader.LoadSingleProperty(Element, "trash-level");
        }
    }
}

using System;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public class Desk : PersistentObject
    {
        private Computer _Computer;
        private Janitor _Janitor;
        private RectangleF _Rectangle ;
        private Office _Office;
        private Person _Person;
        private Double _TrashLevel;

        public Office Office
        {
            get
            {
                return _Office;
            }
            set
            {
                if(value == null)
                {
                    Debug.Assert(_Office != null);
                    _Office = null;
                }
                else
                {
                    Debug.Assert(_Office == null);
                    _Office = value;
                }
            }
        }

        public Double TrashLevel
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
            _Computer = new Computer();
            _Office = null;
            _Rectangle.Height = Data.DeskHeight;
            _Rectangle.Width = Data.DeskWidth;
            _TrashLevel = 0.0;
        }

        public Computer GetComputer()
        {
            return _Computer;
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public Janitor GetJanitor()
        {
            return _Janitor;
        }

        public PointF GetLocation()
        {
            return _Rectangle.Location;
        }

        public PointF GetMidLocation()
        {
            return new PointF(_Rectangle.X + _Rectangle.Width / 2.0f, _Rectangle.Y + _Rectangle.Height / 2.0f);
        }

        public Person GetPerson()
        {
            return _Person;
        }

        public RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public Single GetX()
        {
            return _Rectangle.X;
        }

        public Single GetY()
        {
            return _Rectangle.Y;
        }

        public Boolean IsFree()
        {
            return _Person == null;
        }

        public void SetHeight(Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetJanitor(Janitor Janitor)
        {
            _Janitor = Janitor;
        }

        public void SetLocation(Single X, Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetPerson(Person Person)
        {
            if(Person == null)
            {
                Debug.Assert(_Person != null);
                _Person = null;
            }
            else
            {
                Debug.Assert(_Person == null);
                _Person = Person;
            }
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(Single Y)
        {
            _Rectangle.Y = Y;
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("computer", _Computer);
            ObjectStore.Save("janitor", _Janitor);
            ObjectStore.Save("office", _Office);
            ObjectStore.Save("person", _Person);
            ObjectStore.Save("rectangle", _Rectangle);
            ObjectStore.Save("trash-level", _TrashLevel);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Computer = ObjectStore.LoadComputerProperty("computer");
            _Janitor = ObjectStore.LoadJanitorProperty("janitor");
            _Office = ObjectStore.LoadOfficeProperty("office");
            _Person = ObjectStore.LoadPersonProperty("person");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
            _TrashLevel = ObjectStore.LoadDoubleProperty("trash-level");
        }
    }
}

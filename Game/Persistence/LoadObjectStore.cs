using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace ButtonOffice
{
    public class LoadObjectStore
    {
        private readonly XmlElement _Element;
        private readonly GameLoader _GameLoader;

        public LoadObjectStore(GameLoader GameLoader, XmlElement Element)
        {
            _Element = Element;
            _GameLoader = GameLoader;
        }


        #region "public loader functions"

        public List<Accountant> LoadAccountants(String ListName)
        {
            var Result = new List<Accountant>();

            foreach(XmlNode Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadAccountant(Node as XmlElement));
            }

            return Result;
        }

        public ActionState LoadActionStateProperty(String PropertyName)
        {
            return (ActionState)Enum.Parse(typeof(ActionState), _GetPropertyValue(_Element, PropertyName, "ButtonOffice.ActionState"));
        }

        public AnimationState LoadAnimationStateProperty(String PropertyName)
        {
            return (AnimationState)Enum.Parse(typeof(AnimationState), _GetPropertyValue(_Element, PropertyName, "ButtonOffice.AnimationState"));
        }

        public List<Bathroom> LoadBathrooms(String ListName)
        {
            var Result = new List<Bathroom>();

            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadBathroom(Node as XmlElement));
            }

            return Result;
        }

        public Boolean LoadBooleanProperty(String PropertyName)
        {
            return Convert.ToBoolean(_GetPropertyValue(_Element, PropertyName, "System.Boolean"));
        }

        public List<Pair<Office, BrokenThing>> LoadBrokenThings(String PropertyName)
        {
            var Result = new List<Pair<Office, BrokenThing>>();

            foreach(var Node in _GetPropertyElements(_Element, PropertyName, "item"))
            {
                Result.Add(_LoadBrokenThing(Node as XmlElement));
            }

            return Result;
        }

        public Pair<Office, BrokenThing> LoadBrokenThingProperty(String PropertyName)
        {
            return _LoadBrokenThing(_GetPropertyElement(_Element, PropertyName));
        }

        public List<Building> LoadBuildings(String ListName)
        {
            var Result = new List<Building>();

            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadBuilding(Node as XmlElement));
            }

            return Result;
        }

        public Cat LoadCatProperty(String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(_Element, PropertyName)) as Cat;
        }

        public Color LoadColorProperty(String PropertyName)
        {
            var PropertyElement = _GetPropertyElement(_Element, PropertyName);

            return Color.FromArgb(Convert.ToByte(255.0f * _LoadSingleProperty(PropertyElement, "opacity")), Convert.ToByte(255.0f * _LoadSingleProperty(PropertyElement, "red")), Convert.ToByte(255.0f * _LoadSingleProperty(PropertyElement, "green")), Convert.ToByte(255.0f * _LoadSingleProperty(PropertyElement, "blue")));
        }

        public Computer LoadComputerProperty(String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(_Element, PropertyName)) as Computer;
        }

        public List<Desk> LoadDesks(String ListName)
        {
            var Result = new List<Desk>();

            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadDesk(Node as XmlElement));
            }

            return Result;
        }

        public Desk LoadDeskProperty(String PropertyName)
        {
            return _LoadDesk(_GetPropertyElement(_Element, PropertyName));
        }

        public Double LoadDoubleProperty(String PropertyName)
        {
            return Convert.ToDouble(_GetPropertyValue(_Element, PropertyName, "System.Double"), _GameLoader.CultureInfo);
        }

        public Goal LoadGoalProperty(String PropertyName)
        {
            return _LoadGoal(_GetPropertyElement(_Element, PropertyName));
        }

        public Int32 LoadInt32Property(String PropertyName)
        {
            return Convert.ToInt32(_GetPropertyValue(_Element, PropertyName, "System.Int32"), _GameLoader.CultureInfo);
        }

        public Int64 LoadInt64Property(String PropertyName)
        {
            return Convert.ToInt64(_GetPropertyValue(_Element, PropertyName, "System.Int64"), _GameLoader.CultureInfo);
        }

        public List<Goal> LoadGoals(String ListName)
        {
            var Result = new List<Goal>();

            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadGoal(Node as XmlElement));
            }

            return Result;
        }

        public GoalState LoadGoalState(String PropertyName)
        {
            return (GoalState)Enum.Parse(typeof(GoalState), _GetPropertyValue(_Element, PropertyName, "ButtonOffice.GoalState"));
        }

        public Janitor LoadJanitorProperty(String PropertyName)
        {
            return _LoadPersonProperty(_Element, PropertyName) as Janitor;
        }

        public Lamp LoadLampProperty(String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(_Element, PropertyName)) as Lamp;
        }

        public LivingSide LoadLivingSideProperty(String PropertyName)
        {
            return (LivingSide)Enum.Parse(typeof(LivingSide), _GetPropertyValue(_Element, PropertyName, "ButtonOffice.LivingSide"));
        }

        public Mind LoadMindProperty(String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(_Element, PropertyName)) as Mind;
        }

        public List<Office> LoadOffices(String ListName)
        {
            var Result = new List<Office>();

            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadOffice(Node as XmlElement));
            }

            return Result;
        }

        public Office LoadOfficeProperty(String PropertyName)
        {
            return _LoadOffice(_GetPropertyElement(_Element, PropertyName));
        }

        public List<Person> LoadPersons(String ListName)
        {
            var Result = new List<Person>();

            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadPerson(Node as XmlElement));
            }

            return Result;
        }

        public Person LoadPersonProperty(String PropertyName)
        {
            return _LoadPerson(_GetPropertyElement(_Element, PropertyName));
        }

        public PointF LoadPointProperty(String PropertyName)
        {
            var PropertyElement = _GetPropertyElement(_Element, PropertyName);

            _AssertElementAndType(PropertyElement, typeof(PointF).FullName);

            return new PointF(_LoadSingleProperty(PropertyElement, "x"), _LoadSingleProperty(PropertyElement, "y"));
        }

        public RectangleF LoadRectangleProperty(String PropertyName)
        {
            var PropertyElement = _GetPropertyElement(_Element, PropertyName);

            _AssertElementAndType(PropertyElement, typeof(RectangleF).FullName);

            return new RectangleF(_LoadSingleProperty(PropertyElement, "x"), _LoadSingleProperty(PropertyElement, "y"), _LoadSingleProperty(PropertyElement, "width"), _LoadSingleProperty(PropertyElement, "height"));
        }

        public Single LoadSingleProperty(String PropertyName)
        {
            return _LoadSingleProperty(_Element, PropertyName);
        }

        public String LoadStringProperty(String PropertyName)
        {
            return _GetPropertyValue(_Element, PropertyName, "System.String");
        }

        public List<Stairs> LoadStairs(String ListName)
        {
            var Result = new List<Stairs>();

            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadStairs(Node as XmlElement));
            }

            return Result;
        }

        public Stairs LoadStairsProperty(String PropertyName)
        {
            return _LoadStairs(_GetPropertyElement(_Element, PropertyName));
        }

        public UInt32 LoadUInt32Property(String PropertyName)
        {
            return Convert.ToUInt32(_GetPropertyValue(_Element, PropertyName, "System.UInt32"), _GameLoader.CultureInfo);
        }

        public UInt64 LoadUInt64Property(String PropertyName)
        {
            return Convert.ToUInt64(_GetPropertyValue(_Element, PropertyName, "System.UInt64"), _GameLoader.CultureInfo);
        }

        #endregion


        #region "internal loader functions"

        private Accountant _LoadAccountant(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Accountant;
        }

        private Bathroom _LoadBathroom(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Bathroom;
        }

        public Pair<Office, BrokenThing> _LoadBrokenThing(XmlElement Element)
        {
            Pair<Office, BrokenThing> Result = null;

            if(Element.ChildNodes.Count > 0)
            {
                Result = new Pair<Office, BrokenThing>(_LoadOffice(_GetPropertyElement(Element, "office")), _LoadBrokenThingProperty(Element, "broken-thing"));
            }

            return Result;
        }

        public BrokenThing _LoadBrokenThingProperty(XmlElement Element, String PropertyName)
        {
            return (BrokenThing)Enum.Parse(typeof(BrokenThing), _GetPropertyValue(Element, PropertyName, "ButtonOffice.BrokenThing"));
        }

        private Byte _LoadByteProperty(XmlElement Element, String PropertyName)
        {
            return Convert.ToByte(_GetPropertyValue(Element, PropertyName, "System.Byte"), _GameLoader.CultureInfo);
        }

        private Building _LoadBuilding(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Building;
        }

        private Desk _LoadDesk(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Desk;
        }

        private Goal _LoadGoal(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Goal;
        }

        public Office _LoadOffice(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Office;
        }

        private PersistentObject _LoadPersistentObject(XmlElement PropertyElement)
        {
            PersistentObject Result = null;

            if(PropertyElement.InnerText.Trim().Length > 0)
            {
                var Identifier = _LoadUInt32(PropertyElement);

                Result = _GameLoader.GetPersistentObject(Identifier);
                if(Result == null)
                {
                    var ObjectElement = _GameLoader.GetObjectElement(Identifier);

                    Result = Activator.CreateInstance(System.Type.GetType(_AssertElementAndGetType(ObjectElement))) as PersistentObject;
                    if(Result == null)
                    {
                        throw new FormatException();
                    }
                    _GameLoader.AddPersistentObject(Identifier, Result);

                    var LoadObjectStore = new LoadObjectStore(_GameLoader, ObjectElement);

                    Result.Load(LoadObjectStore);
                }
            }

            return Result;
        }

        private Person _LoadPerson(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Person;
        }

        private Person _LoadPersonProperty(XmlElement Element, String PropertyName)
        {
            return _LoadPerson(_GetPropertyElement(Element, PropertyName));
        }

        private Stairs _LoadStairs(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Stairs;
        }

        private Single _LoadSingleProperty(XmlElement Element, String PropertyName)
        {
            return Convert.ToSingle(_GetPropertyValue(Element, PropertyName, "System.Single"), _GameLoader.CultureInfo);
        }

        private UInt32 _LoadUInt32(XmlElement Element)
        {
            return Convert.ToUInt32(_GetTypeSafeValue(Element, "System.UInt32"), _GameLoader.CultureInfo);
        }

        #endregion


        #region "helper functions"

        private static String _GetNodePath(XmlNode Node)
        {
            String Result = null;

            while(Node != null)
            {
                if(Result != null)
                {
                    Result = "/" + Result;
                }
                Result = Node.Name + Result;
                Node = Node.ParentNode;
            }

            return Result;
        }

        private static String _AssertElementAndGetType(XmlElement Element)
        {
            if(Element == null)
            {
                throw new FormatException();
            }
            if(Element.Attributes["type"] == null)
            {
                throw new FormatException();
            }

            return Element.Attributes["type"].Value;
        }

        private static void _AssertElementAndType(XmlElement Element, String Type)
        {
            if(_AssertElementAndGetType(Element) != Type)
            {
                throw new FormatException();
            }
        }

        private static XmlElement _GetPropertyElement(XmlElement ObjectElement, String PropertyName)
        {
            return ObjectElement.SelectSingleNode(PropertyName) as XmlElement;
        }

        private static XmlNodeList _GetPropertyElements(XmlElement ObjectElement, String PropertyName, String ListElementName)
        {
            return ObjectElement.SelectNodes(PropertyName + "/" + ListElementName);
        }

        private static String _GetPropertyValue(XmlElement ObjectElement, String PropertyName, String PropertyType)
        {
            var PropertyElement = _GetPropertyElement(ObjectElement, PropertyName);

            if(PropertyElement == null)
            {
                throw new FormatException("The property \"" + PropertyName + "\" is not defined on the element \"" + _GetNodePath(ObjectElement) + "\".");
            }

            return _GetTypeSafeValue(PropertyElement, PropertyType);
        }

        private static String _GetTypeSafeValue(XmlElement Element, String Type)
        {
            _AssertElementAndType(Element, Type);

            return Element.InnerText;
        }

        #endregion
    }
}

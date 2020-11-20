using ButtonOffice.AI;
using ButtonOffice.AI.Goals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Mind = ButtonOffice.AI.Mind;

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
        
        public ActionState LoadActionStateProperty(String PropertyName)
        {
            return (ActionState)Enum.Parse(typeof(ActionState), _GetPropertyValue(_Element, PropertyName, typeof(ActionState)));
        }
        
        public AnimationState LoadAnimationStateProperty(String PropertyName)
        {
            return (AnimationState)Enum.Parse(typeof(AnimationState), _GetPropertyValue(_Element, PropertyName, typeof(AnimationState)));
        }
        
        public Boolean LoadBooleanProperty(String PropertyName)
        {
            return Convert.ToBoolean(_GetPropertyValue(_Element, PropertyName, typeof(Boolean)));
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
            return _LoadDoubleProperty(_Element, PropertyName);
        }
        
        public Goal LoadGoalProperty(String PropertyName)
        {
            return _LoadGoal(_GetPropertyElement(_Element, PropertyName));
        }
        
        public Int32 LoadInt32Property(String PropertyName)
        {
            return Convert.ToInt32(_GetPropertyValue(_Element, PropertyName, typeof(Int32)), _GameLoader.CultureInfo);
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
            return (GoalState)Enum.Parse(typeof(GoalState), _GetPropertyValue(_Element, PropertyName, typeof(GoalState)));
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
            return (LivingSide)Enum.Parse(typeof(LivingSide), _GetPropertyValue(_Element, PropertyName, typeof(LivingSide)));
        }
        
        public Memory LoadMemoryProperty(String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(_Element, PropertyName)) as Memory;
        }
        
        public Mind LoadMindProperty(String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(_Element, PropertyName)) as Mind;
        }
        
        public PersistentObject LoadObjectProperty(String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(_Element, PropertyName));
        }
        
        public List<PersistentObject> LoadObjects(String ListName)
        {
            var Result = new List<PersistentObject>();
            
            foreach(var Node in _GetPropertyElements(_Element, ListName, "item"))
            {
                Result.Add(_LoadPersistentObject(Node as XmlElement));
            }
            
            return Result;
        }
        
        public Office LoadOfficeProperty(String PropertyName)
        {
            return _LoadOffice(_GetPropertyElement(_Element, PropertyName));
        }
        
        public Person LoadPersonProperty(String PropertyName)
        {
            return _LoadPerson(_GetPropertyElement(_Element, PropertyName));
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
        
        public RectangleF LoadRectangleProperty(String PropertyName)
        {
            var PropertyElement = _GetPropertyElement(_Element, PropertyName);
            
            _AssertElementAndType(PropertyElement, typeof(RectangleF));
            
            return new RectangleF(_LoadSingleProperty(PropertyElement, "x"), _LoadSingleProperty(PropertyElement, "y"), _LoadSingleProperty(PropertyElement, "width"), _LoadSingleProperty(PropertyElement, "height"));
        }
        
        public String LoadStringProperty(String PropertyName)
        {
            return _GetPropertyValue(_Element, PropertyName, typeof(String));
        }
        
        public Stairs LoadStairsProperty(String PropertyName)
        {
            return _LoadStairs(_GetPropertyElement(_Element, PropertyName));
        }
        
        public UInt32 LoadUInt32Property(String PropertyName)
        {
            return Convert.ToUInt32(_GetPropertyValue(_Element, PropertyName, typeof(UInt32)), _GameLoader.CultureInfo);
        }
        
        public UInt64 LoadUInt64Property(String PropertyName)
        {
            return Convert.ToUInt64(_GetPropertyValue(_Element, PropertyName, typeof(UInt64)), _GameLoader.CultureInfo);
        }
        
        public Vector2 LoadVector2Property(String PropertyName)
        {
            var PropertyElement = _GetPropertyElement(_Element, PropertyName);
            
            _AssertElementAndType(PropertyElement, typeof(Vector2));
            
            return new Vector2(_LoadDoubleProperty(PropertyElement, "x"), _LoadDoubleProperty(PropertyElement, "y"));
        }
        
        public void LoadForEach(String PropertyName, Action<LoadObjectStore> LoadFunction)
        {
            foreach(var Node in _GetPropertyElements(_Element, PropertyName))
            {
                LoadFunction(new LoadObjectStore(_GameLoader, Node as XmlElement));
            }
        }
        
        #endregion
        
        
        #region "internal loader functions"
        
        private Building _LoadBuilding(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Building;
        }
        
        private Desk _LoadDesk(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Desk;
        }
        
        private Double _LoadDoubleProperty(XmlElement Element, String PropertyName)
        {
            return Convert.ToDouble(_GetPropertyValue(Element, PropertyName, typeof(Double)), _GameLoader.CultureInfo);
        }
        
        private Goal _LoadGoal(XmlElement Element)
        {
            return _LoadPersistentObject(Element) as Goal;
        }
        
        private Office _LoadOffice(XmlElement Element)
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
                    
                    Result = Activator.CreateInstance(_AssertElementAndGetType(ObjectElement)) as PersistentObject;
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
            return Convert.ToSingle(_GetPropertyValue(Element, PropertyName, typeof(Single)), _GameLoader.CultureInfo);
        }
        
        private UInt32 _LoadUInt32(XmlElement Element)
        {
            return Convert.ToUInt32(_GetTypeSafeValue(Element, typeof(UInt32)), _GameLoader.CultureInfo);
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
        
        private static Type _AssertElementAndGetType(XmlElement Element)
        {
            if(Element == null)
            {
                throw new FormatException();
            }
            if(Element.Attributes["type"] == null)
            {
                throw new FormatException();
            }
            
            return Type.GetType(Element.Attributes["type"].Value);
        }
        
        private static void _AssertElementAndType(XmlElement Element, Type ExpectingType)
        {
            var ReadingType = _AssertElementAndGetType(Element);
            
            if(ReadingType != ExpectingType)
            {
                throw new FormatException($"Type error for a \"{Element.Name}\" element: expecting \"{ExpectingType.AssemblyQualifiedName}\" but got a \"{ReadingType.AssemblyQualifiedName}\".");
            }
        }
        
        private static XmlElement _GetPropertyElement(XmlElement ObjectElement, String PropertyName)
        {
            return ObjectElement.SelectSingleNode(PropertyName) as XmlElement;
        }
        
        private static XmlNodeList _GetPropertyElements(XmlElement ObjectElement, String PropertyName)
        {
            return ObjectElement.SelectNodes(PropertyName);
        }
        
        private static XmlNodeList _GetPropertyElements(XmlElement ObjectElement, String PropertyName, String ListElementName)
        {
            return ObjectElement.SelectNodes($"{PropertyName}/{ListElementName}");
        }
        
        private static String _GetPropertyValue(XmlElement ObjectElement, String PropertyName, Type PropertyType)
        {
            var PropertyElement = _GetPropertyElement(ObjectElement, PropertyName);
            
            if(PropertyElement == null)
            {
                throw new FormatException("The property \"{PropertyName}\" is not defined on the element \"{_GetNodePath(ObjectElement)}\".");
            }
            
            return _GetTypeSafeValue(PropertyElement, PropertyType);
        }
        
        private static String _GetTypeSafeValue(XmlElement Element, Type Type)
        {
            _AssertElementAndType(Element, Type);
            
            return Element.InnerText;
        }
        
        #endregion
    }
}

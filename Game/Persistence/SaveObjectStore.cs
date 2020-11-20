using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Xml;

namespace ButtonOffice
{
    public class SaveObjectStore
    {
        private readonly XmlElement _Element;
        private readonly GameSaver _GameSaver;
        
        public SaveObjectStore(GameSaver GameSaver, XmlElement Element)
        {
            _Element = Element;
            _GameSaver = GameSaver;
        }
        
        #region "public save functions"}
        
        public void Save(String PropertyName, ActionState Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString());
        }
        
        public void Save(String PropertyName, AnimationState Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString());
        }
        
        public void Save(String PropertyName, Boolean Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString(_GameSaver.CultureInfo));
        }
        
        public void Save(String PropertyName, Color Value)
        {
            var Result = _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType());
            
            _AppendProperty(Result, "red", Convert.ToSingle(Value.R) / 255.0f);
            _AppendProperty(Result, "green", Convert.ToSingle(Value.G) / 255.0f);
            _AppendProperty(Result, "blue", Convert.ToSingle(Value.B) / 255.0f);
            _AppendProperty(Result, "opacity", Convert.ToSingle(Value.A) / 255.0f);
        }
        
        public void Save(String PropertyName, Double Value)
        {
            _AppendProperty(_Element, PropertyName, Value);
        }
        
        public void Save(String PropertyName, GoalState Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString());
        }
        
        public void Save(String PropertyName, Int32 Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString(_GameSaver.CultureInfo));
        }
        
        public void Save(String PropertyName, LivingSide Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString());
        }
        
        public void Save(String PropertyName, PersistentObject Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value);
        }
        
        public void Save<ObjectType>(String PropertyName, IEnumerable<ObjectType> PersistentObjects) where ObjectType : PersistentObject
        {
            var ListElement = _GameSaver.CreateElement(_Element, PropertyName);
        
            foreach(var PersistentObject in PersistentObjects)
            {
                _GameSaver.CreateChildElement(ListElement, "item", PersistentObject);
            }
        }
        
        public void Save(String PropertyName, RectangleF Value)
        {
            var Result = _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType());
            
            _AppendProperty(Result, "x", Value.X);
            _AppendProperty(Result, "y", Value.Y);
            _AppendProperty(Result, "width", Value.Width);
            _AppendProperty(Result, "height", Value.Height);
        }
        
        public void Save(String PropertyName, Single Value)
        {
            _AppendProperty(_Element, PropertyName, Value);
        }
        
        public void Save(String PropertyName, String Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value);
        }
        
        public void Save(String PropertyName, UInt32 Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString(_GameSaver.CultureInfo));
        }
        
        public void Save(String PropertyName, UInt64 Value)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType(), Value.ToString(_GameSaver.CultureInfo));
        }
        
        public void Save(String PropertyName, Vector2 Value)
        {
            var Result = _GameSaver.CreateChildElement(_Element, PropertyName, Value.GetType());
            
            _AppendProperty(Result, "x", Value.X);
            _AppendProperty(Result, "y", Value.Y);
        }
        
        public void Save(String PropertyName, Object Value)
        {
            Debug.Assert(Value is PersistentObject);
            _GameSaver.CreateChildElement(_Element, PropertyName, Value as PersistentObject);
        }
        
        public SaveObjectStore Save(String PropertyName)
        {
            var ListElement = _GameSaver.CreateElement(_Element, PropertyName);
            
            return new SaveObjectStore(_GameSaver, ListElement);
        }
        
        #endregion
        
        
        #region "private helper functions"
        
        private void _AppendProperty(XmlElement ParentElement, String Name, Double Value)
        {
            _GameSaver.CreateChildElement(ParentElement, Name, Value.GetType(), Value.ToString(_GameSaver.CultureInfo));
        }
        
        private void _AppendProperty(XmlElement ParentElement, String Name, Single Value)
        {
            _GameSaver.CreateChildElement(ParentElement, Name, Value.GetType(), Value.ToString(_GameSaver.CultureInfo));
        }
        
        #endregion
    }
}

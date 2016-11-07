using System;
using System.Collections.Generic;
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

        public void Save(String PropertyName, ActionState ActionState)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, ActionState));
        }

        public void Save(String PropertyName, AnimationState AnimationState)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, AnimationState));
        }

        public void Save(String PropertyName, Boolean Boolean)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, Boolean));
        }

        public void Save(String PropertyName, Pair<Office, BrokenThing> BrokenThing)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, BrokenThing));
        }

        public void Save(String PropertyName, IEnumerable<Pair<Office, BrokenThing>> BrokenThings)
        {
            var ListElement = _GameSaver.CreateElement(_Element, PropertyName);

            foreach(var BrokenThing in BrokenThings)
            {
                ListElement.AppendChild(_GameSaver.CreateProperty("item", BrokenThing));
            }
        }

        public void Save(String PropertyName, Color Color)
        {
            var Result = _GameSaver.CreateChildElement(_Element, PropertyName, typeof(Color).FullName);

            _AppendProperty(Result, "red", Convert.ToSingle(Color.R) / 255.0f);
            _AppendProperty(Result, "green", Convert.ToSingle(Color.G) / 255.0f);
            _AppendProperty(Result, "blue", Convert.ToSingle(Color.B) / 255.0f);
            _AppendProperty(Result, "opacity", Convert.ToSingle(Color.A) / 255.0f);
        }

        public void Save(String ProperyName, GoalState GoalState)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(ProperyName, GoalState));
        }

        public void Save(String PropertyName, Int32 Int32)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, Int32));
        }

        public void Save(String PropertyName, LivingSide LivingSide)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, LivingSide));
        }

        public void Save(String PropertyName, PersistentObject PersistentObject)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, PersistentObject));
        }

        public void Save<ObjectType>(String PropertyName, IEnumerable<ObjectType> PersistentObjects) where ObjectType : PersistentObject
        {
            var ListElement = _GameSaver.CreateElement(_Element, PropertyName);

            foreach(var PersistentObject in PersistentObjects)
            {
                ListElement.AppendChild(_GameSaver.CreateProperty("item", PersistentObject));
            }
        }

        public void Save(String PropertyName, PointF PointF)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, PointF));
        }

        public void Save(String PropertyName, RectangleF RectangleF)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, RectangleF));
        }

        public void Save(String PropertyName, Single Single)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, Single));
        }

        public void Save(String PropertyName, String String)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, String));
        }

        public void Save(String PropertyName, UInt32 UInt32)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, UInt32));
        }

        public void Save(String PropertyName, UInt64 UInt64)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, UInt64));
        }

        #endregion

        #region "private helper functions"

        private void _AppendProperty(XmlElement ParentElement, String Name, Single Single)
        {
            _GameSaver.CreateChildElement(ParentElement, Name, typeof(Single).FullName, Single.ToString(_GameSaver.CultureInfo));
        }

        #endregion
    }
}

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
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(ActionState).FullName, ActionState.ToString());
        }

        public void Save(String PropertyName, AnimationState AnimationState)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(AnimationState).FullName, AnimationState.ToString());
        }

        public void Save(String PropertyName, Boolean Boolean)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(Boolean).FullName, Boolean.ToString(_GameSaver.CultureInfo));
        }

        public void Save(String PropertyName, Color Color)
        {
            var Result = _GameSaver.CreateChildElement(_Element, PropertyName, typeof(Color).FullName);

            _AppendProperty(Result, "red", Convert.ToSingle(Color.R) / 255.0f);
            _AppendProperty(Result, "green", Convert.ToSingle(Color.G) / 255.0f);
            _AppendProperty(Result, "blue", Convert.ToSingle(Color.B) / 255.0f);
            _AppendProperty(Result, "opacity", Convert.ToSingle(Color.A) / 255.0f);
        }

        public void Save(String PropertyName, GoalState GoalState)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(GoalState).FullName, GoalState.ToString());
        }

        public void Save(String PropertyName, Int32 Int32)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(Int32).FullName, Int32.ToString(_GameSaver.CultureInfo));
        }

        public void Save(String PropertyName, LivingSide LivingSide)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(LivingSide).FullName, LivingSide.ToString());
        }

        public void Save(String PropertyName, PersistentObject PersistentObject)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, PersistentObject);
        }

        public void Save<ObjectType>(String PropertyName, IEnumerable<ObjectType> PersistentObjects) where ObjectType : PersistentObject
        {
            var ListElement = _GameSaver.CreateElement(_Element, PropertyName);

            foreach(var PersistentObject in PersistentObjects)
            {
                _GameSaver.CreateChildElement(ListElement, "item", PersistentObject);
            }
        }

        public void Save(String PropertyName, PointF PointF)
        {
            var Result = _GameSaver.CreateChildElement(_Element, PropertyName, typeof(PointF).FullName);

            _AppendProperty(Result, "x", PointF.X);
            _AppendProperty(Result, "y", PointF.Y);
        }

        public void Save(String PropertyName, RectangleF RectangleF)
        {
            var Result = _GameSaver.CreateChildElement(_Element, PropertyName, typeof(RectangleF).FullName);

            _AppendProperty(Result, "x", RectangleF.X);
            _AppendProperty(Result, "y", RectangleF.Y);
            _AppendProperty(Result, "width", RectangleF.Width);
            _AppendProperty(Result, "height", RectangleF.Height);
        }

        public void Save(String PropertyName, Single Single)
        {
            _AppendProperty(_Element, PropertyName, Single);
        }

        public void Save(String PropertyName, Double Double)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(Double).FullName, Double.ToString(_GameSaver.CultureInfo));
        }

        public void Save(String PropertyName, String String)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(String).FullName, String);
        }

        public void Save(String PropertyName, UInt32 UInt32)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(UInt32).FullName, UInt32.ToString(_GameSaver.CultureInfo));
        }

        public void Save(String PropertyName, UInt64 UInt64)
        {
            _GameSaver.CreateChildElement(_Element, PropertyName, typeof(UInt64).FullName, UInt64.ToString(_GameSaver.CultureInfo));
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

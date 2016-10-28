using System;
using System.Collections.Generic;
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

        #region "public save functions"

        public void Save(String PropertyName, IEnumerable<Accountant> Accountants)
        {
            var AccountantListElement = _GameSaver.CreateElement(_Element, PropertyName);

            foreach(var Accountant in Accountants)
            {
                AccountantListElement.AppendChild(_GameSaver.CreateProperty("item", Accountant));
            }
        }

        public void Save(String PropertyName, IEnumerable<Bathroom> Bathrooms)
        {
            var BathroomListElement = _GameSaver.CreateElement(_Element, PropertyName);

            foreach(var Bathroom in Bathrooms)
            {
                BathroomListElement.AppendChild(_GameSaver.CreateProperty("item", Bathroom));
            }
        }

        public void Save(String PropertyName, IEnumerable<Pair<Office, BrokenThing>> BrokenThings)
        {
            var BrokenThingListElement = _GameSaver.CreateElement(_Element, PropertyName);

            foreach(var BrokenThing in BrokenThings)
            {
                BrokenThingListElement.AppendChild(_GameSaver.CreateProperty("item", BrokenThing));
            }
        }

        public void Save(String PropertyName, Cat Cat)
        {
            _Element.AppendChild(_GameSaver.CreateProperty(PropertyName, Cat));
        }

        #endregion
    }
}

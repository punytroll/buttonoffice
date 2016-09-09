using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace ButtonOffice
{
    public class GameLoader
    {
        public CultureInfo CultureInfo
        {
            get
            {
                return _CultureInfo;
            }
        }

        readonly CultureInfo _CultureInfo;
        readonly XmlDocument _Document;
        readonly Dictionary<UInt32, PersistentObject> _Objects;

        public GameLoader(String FileName)
        {
            _CultureInfo = CultureInfo.InvariantCulture;
            _Document = new XmlDocument();
            _Document.Load(FileName);
            _Objects = new Dictionary<UInt32, PersistentObject>();
        }

        public PersistentObject GetPersistentObject(UInt32 Identifier)
        {
            PersistentObject Result;

            if(_Objects.TryGetValue(Identifier, out Result) == true)
            {
                return Result;
            }
            else
            {
                return null;
            }
        }

        public void AddPersistentObject(UInt32 Identifier, PersistentObject PersistentObject)
        {
            _Objects.Add(Identifier, PersistentObject);
        }

        public XmlElement GetObjectElement(UInt32 Identifier)
        {
            return _Document.SelectSingleNode("//button-office/*[@identifier='" + Identifier.ToString(_CultureInfo) + "']") as System.Xml.XmlElement;
        }

        public void Load(Game Game)
        {
            var ButtonOfficeElement = _Document.DocumentElement;

            if(ButtonOfficeElement == null)
            {
                throw new FormatException();
            }
            if(ButtonOfficeElement.Attributes["version"] == null)
            {
                throw new FormatException();
            }
            if(ButtonOfficeElement.Attributes["version"].Value != Data.SaveGameFileVersion)
            {
                throw new FormatException();
            }

            var GameElement = ButtonOfficeElement.SelectSingleNode("game") as XmlElement;
            var ObjectStore = new LoadObjectStore(this, GameElement);

            // _AssertElementAndType(GameElement, "ButtonOffice.Game");
            Game.Load(ObjectStore);
        }
    }
}

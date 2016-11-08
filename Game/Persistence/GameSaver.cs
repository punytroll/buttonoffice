using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace ButtonOffice
{
    public class GameSaver
    {
        public CultureInfo CultureInfo
        {
            get
            {
                return _CultureInfo;
            }
        }

        private readonly CultureInfo _CultureInfo;
        private readonly XmlDocument _Document;
        private readonly Dictionary<PersistentObject, Pair<Boolean, UInt32>> _Objects;

        public GameSaver()
        {
            _CultureInfo = CultureInfo.InvariantCulture;
            _Document = new XmlDocument();
            _Document.AppendChild(_Document.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\""));

            var ButtonOfficeElement = _Document.CreateElement("button-office");

            ButtonOfficeElement.Attributes.Append(_CreateAttribute("version", Data.SaveGameFileVersion));
            _Document.AppendChild(ButtonOfficeElement);
            _Objects = new Dictionary<PersistentObject, Pair<Boolean, UInt32>>();
        }

        public void Save(Game Game)
        {
            _Save(Game);
        }

        public void WriteToFile(String FileName)
        {
            _Document.Save(FileName);
        }

        public XmlElement CreateChildElement(XmlElement ParentElement, String Name, String Type)
        {
            var Result = _Document.CreateElement(Name);

            ParentElement.AppendChild(Result);
            Result.Attributes.Append(_CreateAttribute("type", Type));

            return Result;
        }

        public XmlElement CreateChildElement(XmlElement ParentElement, String Name, String Type, String Value)
        {
            var Result = CreateChildElement(ParentElement, Name, Type);

            Result.AppendChild(_Document.CreateTextNode(Value));

            return Result;
        }

        public void CreateChildElement(XmlElement ParentElement, String Name, PersistentObject PersistentObject)
        {
            _Save(PersistentObject);
            ParentElement.AppendChild(_CreateReference(Name, PersistentObject));
        }

        private System.Xml.XmlAttribute _CreateAttribute(System.String Name, System.String Value)
        {
            System.Xml.XmlAttribute Result = _Document.CreateAttribute(Name);

            Result.Value = Value;

            return Result;
        }

        private System.Xml.XmlElement _CreateProperty(System.String Name, System.String Type, System.String Value)
        {
            System.Xml.XmlElement Result = _Document.CreateElement(Name);

            Result.Attributes.Append(_CreateAttribute("type", Type));
            Result.AppendChild(_Document.CreateTextNode(Value));

            return Result;
        }

        private System.Xml.XmlElement _CreateReference(System.String Name, PersistentObject PersistentObject)
        {
            if(PersistentObject != null)
            {
                return _CreateProperty(Name, "System.UInt32", _GetIdentifier(PersistentObject).ToString(_CultureInfo));
            }
            else
            {
                return _CreateProperty(Name, "System.UInt32", "");
            }
        }

        public XmlElement CreateElement(XmlElement ParentElement, String Name)
        {
            var Result = _Document.CreateElement(Name);

            ParentElement.AppendChild(Result);

            return Result;
        }

        private System.UInt32 _GetIdentifier(PersistentObject PersistentObject)
        {
            if(_Objects.ContainsKey(PersistentObject) == false)
            {
                var Identifier = _Objects.Count.ToUInt32();

                _Objects.Add(PersistentObject, new System.Pair<System.Boolean, System.UInt32>(false, Identifier));
            }

            return _Objects[PersistentObject].Second;
        }

        private void _Save(PersistentObject PersistentObject)
        {
            if(PersistentObject != null)
            {
                if(_Objects.ContainsKey(PersistentObject) == false)
                {
                    var Identifier = _Objects.Count.ToUInt32();

                    _Objects.Add(PersistentObject, new System.Pair<System.Boolean, System.UInt32>(false, Identifier));
                }
                if(_Objects[PersistentObject].First == false)
                {
                    _Objects[PersistentObject].First = true;

                    var Element = _Document.CreateElement("object");

                    Element.Attributes.Append(_CreateAttribute("identifier", _GetIdentifier(PersistentObject).ToString(_CultureInfo)));
                    Element.Attributes.Append(_CreateAttribute("type", PersistentObject.GetType().FullName));

                    var ObjectStore = new SaveObjectStore(this, Element);

                    PersistentObject.Save(ObjectStore);
                    Debug.Assert(_Document.DocumentElement != null);
                    _Document.DocumentElement.AppendChild(Element);
                }
            }
        }
    }
}

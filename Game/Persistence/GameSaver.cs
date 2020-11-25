using ButtonOffice.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace ButtonOffice
{
    public class GameSaver
    {
        public CultureInfo CultureInfo => _CultureInfo;
        
        private readonly CultureInfo _CultureInfo;
        private readonly XmlDocument _Document;
        private readonly Dictionary<PersistentObject, Pair<Boolean, ObjectReference>> _Objects;
        
        public GameSaver()
        {
            _CultureInfo = CultureInfo.InvariantCulture;
            _Document = new XmlDocument();
            _Document.AppendChild(_Document.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\""));
            
            var ButtonOfficeElement = _Document.CreateElement("button-office");
            
            ButtonOfficeElement.Attributes.Append(_CreateAttribute("version", Data.SaveGameFileVersion));
            _Document.AppendChild(ButtonOfficeElement);
            _Objects = new Dictionary<PersistentObject, Pair<Boolean, ObjectReference>>();
        }
        
        public void Save(Game Game)
        {
            _Save(Game);
        }
        
        public void WriteToFile(String FileName)
        {
            _Document.Save(FileName);
        }
        
        public XmlElement CreateChildElement(XmlElement ParentElement, String Name, Type Type)
        {
            var Result = _Document.CreateElement(Name);
            
            ParentElement.AppendChild(Result);
            Result.Attributes.Append(_CreateTypeAttribute(Type));
            
            return Result;
        }
        
        public XmlElement CreateChildElement(XmlElement ParentElement, String Name, Type Type, String Value)
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
        
        private XmlAttribute _CreateTypeAttribute(Type Value)
        {
            return _CreateAttribute("type", Value.AssemblyQualifiedName);
        }
        
        private XmlAttribute _CreateAttribute(String Name, String Value)
        {
            var Result = _Document.CreateAttribute(Name);
            
            Result.Value = Value;
            
            return Result;
        }
        
        private XmlElement _CreateProperty(String Name, String Type, String Value)
        {
            var Result = _Document.CreateElement(Name);
            
            Result.Attributes.Append(_CreateAttribute("type", Type));
            Result.AppendChild(_Document.CreateTextNode(Value));
            
            return Result;
        }
        
        private XmlElement _CreateReference(String Name, PersistentObject PersistentObject)
        {
            if(PersistentObject != null)
            {
                return _CreateProperty(Name, typeof(ObjectReference).AssemblyQualifiedName, _GetIdentifier(PersistentObject).Identifier.ToString(_CultureInfo));
            }
            else
            {
                return _CreateProperty(Name, typeof(ObjectReference).AssemblyQualifiedName, "");
            }
        }
        
        public XmlElement CreateElement(XmlElement ParentElement, String Name)
        {
            var Result = _Document.CreateElement(Name);
            
            ParentElement.AppendChild(Result);
            
            return Result;
        }
        
        private ObjectReference _GetIdentifier(PersistentObject PersistentObject)
        {
            if(_Objects.ContainsKey(PersistentObject) == false)
            {
                var Identifier = _Objects.Count.ToUInt32();
                
                _Objects.Add(PersistentObject, new Pair<Boolean, ObjectReference>(false, new ObjectReference(Identifier)));
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
                    
                    _Objects.Add(PersistentObject, new Pair<Boolean, ObjectReference>(false, new ObjectReference(Identifier)));
                }
                if(_Objects[PersistentObject].First == false)
                {
                    _Objects[PersistentObject].First = true;
                    
                    var Element = _Document.CreateElement("object");
                    
                    Element.Attributes.Append(_CreateAttribute("identifier", _GetIdentifier(PersistentObject).Identifier.ToString(_CultureInfo)));
                    Element.Attributes.Append(_CreateTypeAttribute(PersistentObject.GetType()));
                    
                    var ObjectStore = new SaveObjectStore(this, Element);
                    
                    PersistentObject.Save(ObjectStore);
                    Debug.Assert(_Document.DocumentElement != null);
                    _Document.DocumentElement.AppendChild(Element);
                }
            }
        }
    }
}

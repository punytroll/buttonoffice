namespace ButtonOffice
{
    internal class SaveGameProcessor
    {
        private System.Globalization.CultureInfo _CultureInfo;
        private System.Xml.XmlDocument _Document;
        private System.Collections.Generic.Dictionary<System.Object, System.Xml.XmlElement> _Elements;
        private System.String _FileName;
        private System.Collections.Generic.Dictionary<System.Object, System.UInt32> _LookupTable;

        public SaveGameProcessor(System.String FileName)
        {
            _CultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            _Document = new System.Xml.XmlDocument();
            _Elements = new System.Collections.Generic.Dictionary<System.Object, System.Xml.XmlElement>();
            _FileName = FileName;
            _LookupTable = new System.Collections.Generic.Dictionary<System.Object, System.UInt32>();
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

        private System.Xml.XmlElement _CreateReference(System.String Name, System.Object Object)
        {
            if(Object != null)
            {
                return _CreateProperty(Name, "System.UInt32", _GetIdentifier(Object).ToString(_CultureInfo));
            }
            else
            {
                return _CreateProperty(Name, "System.UInt32", "");
            }
        }

        public System.Xml.XmlElement CreateElement(System.String Name)
        {
            return _Document.CreateElement(Name);
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.ActionState ActionState)
        {
            return _CreateProperty(Name, "ButtonOffice.ActionState", ActionState.ToString());
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.AnimationState AnimationState)
        {
            return _CreateProperty(Name, "ButtonOffice.AnimationState", AnimationState.ToString());
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.BrokenThing BrokenThing)
        {
            return _CreateProperty(Name, "ButtonOffice.BrokenThing", BrokenThing.ToString());
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.Cat Cat)
        {
            return _CreateReference(Name, Cat);
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.Desk Desk)
        {
            return _CreateReference(Name, Desk);
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.Lamp Lamp)
        {
            return _CreateReference(Name, Lamp);
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.LivingSide LivingSide)
        {
            return _CreateProperty(Name, "ButtonOffice.LivingSide", LivingSide.ToString());
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.Office Office)
        {
            return _CreateReference(Name, Office);
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.Person Person)
        {
            return _CreateReference(Name, Person);
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, ButtonOffice.Type Type)
        {
            return _CreateProperty(Name, "ButtonOffice.Type", Type.ToString());
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.Byte Byte)
        {
            return _CreateProperty(Name, "System.Byte", Byte.ToString(_CultureInfo));
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.Drawing.Color Color)
        {
            System.Xml.XmlElement Result = _Document.CreateElement(Name);

            Result.Attributes.Append(_CreateAttribute("type", "System.Drawing.Color"));
            Result.AppendChild(CreateProperty("red", Color.R));
            Result.AppendChild(CreateProperty("green", Color.G));
            Result.AppendChild(CreateProperty("blue", Color.B));
            Result.AppendChild(CreateProperty("alpha", Color.A));

            return Result;
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.Drawing.PointF PointF)
        {
            System.Xml.XmlElement Result = _Document.CreateElement(Name);

            Result.Attributes.Append(_CreateAttribute("type", "System.Drawing.PointF"));
            Result.AppendChild(CreateProperty("x", PointF.X));
            Result.AppendChild(CreateProperty("y", PointF.Y));

            return Result;
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.Drawing.RectangleF Rectangle)
        {
            System.Xml.XmlElement Result = _Document.CreateElement(Name);

            Result.Attributes.Append(_CreateAttribute("type", "System.Drawing.RectangleF"));
            Result.AppendChild(CreateProperty("x", Rectangle.X));
            Result.AppendChild(CreateProperty("y", Rectangle.Y));
            Result.AppendChild(CreateProperty("width", Rectangle.Width));
            Result.AppendChild(CreateProperty("height", Rectangle.Height));

            return Result;
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.Single Single)
        {
            return _CreateProperty(Name, "System.Single", Single.ToString(_CultureInfo));
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.String String)
        {
            return _CreateProperty(Name, "System.String", String);
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.UInt32 UInt32)
        {
            return _CreateProperty(Name, "System.UInt32", UInt32.ToString(_CultureInfo));
        }

        public System.Xml.XmlElement CreateProperty(System.String Name, System.UInt64 UInt64)
        {
            return _CreateProperty(Name, "System.UInt64", UInt64.ToString(_CultureInfo));
        }

        private System.UInt32 _GetIdentifier(System.Object Object)
        {
            if(_LookupTable.ContainsKey(Object) == false)
            {
                System.UInt32 Identifier = _LookupTable.Count.ToUInt32();

                _LookupTable.Add(Object, Identifier);
            }

            return _LookupTable[Object];
        }

        public void Save(ButtonOffice.Game Game)
        {
            _Document.AppendChild(_Document.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\""));
            _Document.AppendChild(_Document.CreateElement("button-office"));

            System.Xml.XmlElement GameElement = Game.Save(this);

            GameElement.Attributes.Append(_CreateAttribute("version", "1.0"));
            foreach(System.Xml.XmlElement Element in _Elements.Values)
            {
                GameElement.AppendChild(Element);
            }
            _Document.DocumentElement.AppendChild(GameElement);
            _Document.Save(_FileName);
        }

        public void Save(ButtonOffice.ISaveable Saveable)
        {
            if((Saveable != null) && (_Elements.ContainsKey(Saveable) == false))
            {
                _Elements.Add(Saveable, null);

                System.Xml.XmlElement Element = Saveable.Save(this);

                Element.Attributes.Append(_CreateAttribute("identifier", _GetIdentifier(Saveable).ToString(_CultureInfo)));
                _Elements[Saveable] = Element;
            }
        }
    }
}

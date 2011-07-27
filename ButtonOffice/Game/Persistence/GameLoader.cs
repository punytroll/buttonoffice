namespace ButtonOffice
{
    internal class GameLoader
    {
        System.Globalization.CultureInfo _CultureInfo;
        System.Xml.XmlDocument _Document;
        System.Collections.Generic.Dictionary<System.UInt32, ButtonOffice.PersistentObject> _Objects;

        public GameLoader(System.String FileName)
        {
            _CultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            _Document = new System.Xml.XmlDocument();
            _Document.Load(FileName);
            _Objects = new System.Collections.Generic.Dictionary<System.UInt32, ButtonOffice.PersistentObject>();
        }

        public void Load(ButtonOffice.Game Game)
        {
            System.Xml.XmlElement ButtonOfficeElement = _Document.DocumentElement;

            if(ButtonOfficeElement.Attributes["version"] == null)
            {
                throw new System.FormatException();
            }
            if(ButtonOfficeElement.Attributes["version"].Value != ButtonOffice.Data.SaveGameFileVersion)
            {
                throw new System.FormatException();
            }

            System.Xml.XmlElement GameElement = ButtonOfficeElement.SelectSingleNode("game") as System.Xml.XmlElement;

            _AssertElementAndType(GameElement, "ButtonOffice.Game");

            Game.Load(this, GameElement);
        }

        private ButtonOffice.Accountant _LoadAccountant(System.Xml.XmlElement Element)
        {
            return _LoadPersistentObject(Element) as ButtonOffice.Accountant;
        }

        public System.Collections.Generic.List<ButtonOffice.Accountant> LoadAccountantList(System.Xml.XmlElement ObjectElement, System.String ListName, System.String ListElementName)
        {
            System.Collections.Generic.List<ButtonOffice.Accountant> Result = new System.Collections.Generic.List<ButtonOffice.Accountant>();

            foreach(System.Xml.XmlNode Node in _GetPropertyElements(ObjectElement, ListName, ListElementName))
            {
                Result.Add(_LoadAccountant(Node as System.Xml.XmlElement));
            }

            return Result;
        }

        public ButtonOffice.ActionState LoadActionStateProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return (ButtonOffice.ActionState)System.Enum.Parse(typeof(ButtonOffice.ActionState), _GetPropertyValue(ObjectElement, PropertyName, "ButtonOffice.ActionState"));
        }

        public ButtonOffice.AnimationState LoadAnimationStateProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return (ButtonOffice.AnimationState)System.Enum.Parse(typeof(ButtonOffice.AnimationState), _GetPropertyValue(ObjectElement, PropertyName, "ButtonOffice.AnimationState"));
        }

        public System.Boolean LoadBooleanProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return System.Convert.ToBoolean(_GetPropertyValue(ObjectElement, PropertyName, "System.Boolean"));
        }

        public System.Collections.Generic.List<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>> LoadBrokenThingList(System.Xml.XmlElement ObjectElement, System.String PropertyName, System.String ElementName)
        {
            System.Collections.Generic.List<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>> Result = new System.Collections.Generic.List<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>>();
            System.Xml.XmlNodeList Elements = _GetPropertyElements(ObjectElement, PropertyName, ElementName);

            foreach(System.Xml.XmlNode Node in Elements)
            {
                Result.Add(_LoadBrokenThing(Node as System.Xml.XmlElement));
            }

            return Result;
        }

        public System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> _LoadBrokenThing(System.Xml.XmlElement PropertyElement)
        {
            System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> Result = null;

            if(PropertyElement.ChildNodes.Count > 0)
            {
                Result = new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(LoadOfficeProperty(PropertyElement, "office"), _LoadBrokenThingProperty(PropertyElement, "broken-thing"));
            }

            return Result;
        }

        public System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> LoadBrokenThingProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            System.Xml.XmlElement PropertyElement = _GetPropertyElement(ObjectElement, PropertyName);

            return _LoadBrokenThing(PropertyElement);
        }

        public ButtonOffice.BrokenThing _LoadBrokenThingProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return (ButtonOffice.BrokenThing)System.Enum.Parse(typeof(ButtonOffice.BrokenThing), _GetPropertyValue(ObjectElement, PropertyName, "ButtonOffice.BrokenThing"));
        }

        public System.Byte LoadByteProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return System.Convert.ToByte(_GetPropertyValue(ObjectElement, PropertyName, "System.Byte"), _CultureInfo);
        }

        public ButtonOffice.Cat LoadCatProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(ObjectElement, PropertyName)) as ButtonOffice.Cat;
        }

        public System.Drawing.Color LoadColorProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            System.Xml.XmlElement PropertyElement = _GetPropertyElement(ObjectElement, PropertyName);

            return System.Drawing.Color.FromArgb(LoadByteProperty(PropertyElement, "alpha"), LoadByteProperty(PropertyElement, "red"), LoadByteProperty(PropertyElement, "green"), LoadByteProperty(PropertyElement, "blue"));
        }

        public ButtonOffice.Computer LoadComputerProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(ObjectElement, PropertyName)) as ButtonOffice.Computer;
        }

        public System.Collections.Generic.List<ButtonOffice.Desk> LoadDeskList(System.Xml.XmlElement ObjectElement, System.String ListName, System.String ElementName)
        {
            System.Collections.Generic.List<ButtonOffice.Desk> Result = new System.Collections.Generic.List<ButtonOffice.Desk>();

            foreach(System.Xml.XmlNode Node in _GetPropertyElements(ObjectElement, ListName, ElementName))
            {
                Result.Add(_LoadDesk(Node as System.Xml.XmlElement));
            }

            return Result;
        }

        public ButtonOffice.Desk LoadDeskProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _LoadDesk(_GetPropertyElement(ObjectElement, PropertyName));
        }

        private ButtonOffice.Desk _LoadDesk(System.Xml.XmlElement Element)
        {
            return _LoadPersistentObject(Element) as ButtonOffice.Desk;
        }

        public System.Collections.Generic.List<ButtonOffice.Goal> LoadGoalList(System.Xml.XmlElement ObjectElement, System.String ListName, System.String ListElementName)
        {
            System.Collections.Generic.List<ButtonOffice.Goal> Result = new System.Collections.Generic.List<ButtonOffice.Goal>();

            foreach(System.Xml.XmlNode Node in _GetPropertyElements(ObjectElement, ListName, ListElementName))
            {
                Result.Add(_LoadGoal(Node as System.Xml.XmlElement));
            }

            return Result;
        }

        public ButtonOffice.Goal LoadGoalProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _LoadGoal(_GetPropertyElement(ObjectElement, PropertyName));
        }

        private ButtonOffice.Goal _LoadGoal(System.Xml.XmlElement PropertyElement)
        {
            return _LoadPersistentObject(PropertyElement) as ButtonOffice.Goal;
        }

        public ButtonOffice.GoalState LoadGoalState(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return (ButtonOffice.GoalState)System.Enum.Parse(typeof(ButtonOffice.GoalState), _GetPropertyValue(ObjectElement, PropertyName, "ButtonOffice.GoalState"));
        }

        public System.Int32 LoadInt32Property(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return System.Convert.ToInt32(_GetPropertyValue(ObjectElement, PropertyName, "System.Int32"), _CultureInfo);
        }

        public ButtonOffice.Janitor LoadJanitorProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return LoadPersonProperty(ObjectElement, PropertyName) as ButtonOffice.Janitor;
        }

        public ButtonOffice.Lamp LoadLampProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _LoadPersistentObject(_GetPropertyElement(ObjectElement, PropertyName)) as ButtonOffice.Lamp;
        }

        public ButtonOffice.LivingSide LoadLivingSideProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return (ButtonOffice.LivingSide)System.Enum.Parse(typeof(ButtonOffice.LivingSide), _GetPropertyValue(ObjectElement, PropertyName, "ButtonOffice.LivingSide"));
        }

        public ButtonOffice.Office _LoadOffice(System.Xml.XmlElement Element)
        {
            return _LoadPersistentObject(Element) as ButtonOffice.Office;
        }

        public System.Collections.Generic.List<ButtonOffice.Office> LoadOfficeList(System.Xml.XmlElement ObjectElement, System.String ListName, System.String ElementName)
        {
            System.Collections.Generic.List<ButtonOffice.Office> Result = new System.Collections.Generic.List<ButtonOffice.Office>();

            foreach(System.Xml.XmlNode Node in _GetPropertyElements(ObjectElement, ListName, ElementName))
            {
                Result.Add(_LoadOffice(Node as System.Xml.XmlElement));
            }

            return Result;
        }

        public ButtonOffice.Office LoadOfficeProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _LoadOffice(_GetPropertyElement(ObjectElement, PropertyName));
        }

        private ButtonOffice.Person _LoadPerson(System.Xml.XmlElement Element)
        {
            return _LoadPersistentObject(Element) as ButtonOffice.Person;
        }

        public ButtonOffice.Person LoadPersonProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _LoadPerson(_GetPropertyElement(ObjectElement, PropertyName));
        }

        public System.Collections.Generic.List<ButtonOffice.Person> LoadPersonList(System.Xml.XmlElement ObjectElement, System.String ListName, System.String ElementName)
        {
            System.Collections.Generic.List<ButtonOffice.Person> Result = new System.Collections.Generic.List<ButtonOffice.Person>();

            foreach(System.Xml.XmlNode Node in _GetPropertyElements(ObjectElement, ListName, ElementName))
            {
                Result.Add(_LoadPerson(Node as System.Xml.XmlElement));
            }

            return Result;
        }

        public System.Drawing.PointF LoadPointProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            System.Xml.XmlElement PropertyElement = _GetPropertyElement(ObjectElement, PropertyName);

            return new System.Drawing.PointF(LoadSingleProperty(PropertyElement, "x"), LoadSingleProperty(PropertyElement, "y"));
        }

        public System.Drawing.RectangleF LoadRectangleProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            System.Xml.XmlElement PropertyElement = _GetPropertyElement(ObjectElement, PropertyName);

            return new System.Drawing.RectangleF(LoadSingleProperty(PropertyElement, "x"), LoadSingleProperty(PropertyElement, "y"), LoadSingleProperty(PropertyElement, "width"), LoadSingleProperty(PropertyElement, "height"));
        }

        public System.Single LoadSingleProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return System.Convert.ToSingle(_GetPropertyValue(ObjectElement, PropertyName, "System.Single"), _CultureInfo);
        }

        public System.String LoadStringProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return _GetPropertyValue(ObjectElement, PropertyName, "System.String");
        }

        public ButtonOffice.Type LoadTypeProperty(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return (ButtonOffice.Type)System.Enum.Parse(typeof(ButtonOffice.Type), _GetPropertyValue(ObjectElement, PropertyName, "ButtonOffice.Type"));
        }

        public System.UInt32 LoadUInt32Property(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return System.Convert.ToUInt32(_GetPropertyValue(ObjectElement, PropertyName, "System.UInt32"), _CultureInfo);
        }

        private System.UInt32 _LoadUInt32(System.Xml.XmlElement Element)
        {
            return System.Convert.ToUInt32(_GetTypeSafeValue(Element, "System.UInt32"), _CultureInfo);
        }

        public System.UInt64 LoadUInt64Property(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return System.Convert.ToUInt64(_GetPropertyValue(ObjectElement, PropertyName, "System.UInt64"), _CultureInfo);
        }

        private System.String _AssertElementAndGetType(System.Xml.XmlElement Element)
        {
            if(Element == null)
            {
                throw new System.FormatException();
            }
            if(Element.Attributes["type"] == null)
            {
                throw new System.FormatException();
            }

            return Element.Attributes["type"].Value;
        }

        private void _AssertElementAndType(System.Xml.XmlElement Element, System.String Type)
        {
            if(_AssertElementAndGetType(Element) != Type)
            {
                throw new System.FormatException();
            }
        }

        private System.Xml.XmlElement _GetPropertyElement(System.Xml.XmlElement ObjectElement, System.String PropertyName)
        {
            return ObjectElement.SelectSingleNode(PropertyName) as System.Xml.XmlElement;
        }

        private System.String _GetPropertyValue(System.Xml.XmlElement ObjectElement, System.String PropertyName, System.String PropertyType)
        {
            return _GetTypeSafeValue(_GetPropertyElement(ObjectElement, PropertyName), PropertyType);
        }

        private ButtonOffice.PersistentObject _LoadPersistentObject(System.Xml.XmlElement PropertyElement)
        {
            try
            {
                System.UInt32 Identifier = _LoadUInt32(PropertyElement);

                if(_Objects.ContainsKey(Identifier) == false)
                {
                    System.Xml.XmlElement ReferenceObjectElement = _Document.SelectSingleNode("//button-office/*[@identifier='" + Identifier.ToString() + "']") as System.Xml.XmlElement;
                    ButtonOffice.PersistentObject PersistentObject = System.Activator.CreateInstance(System.Type.GetType(_AssertElementAndGetType(ReferenceObjectElement))) as ButtonOffice.PersistentObject;

                    _Objects[Identifier] = PersistentObject;
                    PersistentObject.Load(this, ReferenceObjectElement);
                }

                return _Objects[Identifier];
            }
            catch(System.FormatException)
            {
                return null;
            }
        }

        private System.Xml.XmlNodeList _GetPropertyElements(System.Xml.XmlElement ObjectElement, System.String PropertyName, System.String ListElementName)
        {
            return ObjectElement.SelectNodes(PropertyName + "/" + ListElementName);
        }

        private System.String _GetTypeSafeValue(System.Xml.XmlElement Element, System.String Type)
        {
            _AssertElementAndType(Element, Type);

            return Element.InnerText;
        }
    }
}

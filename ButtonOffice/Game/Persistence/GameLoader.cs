namespace ButtonOffice
{
    internal class GameLoader
    {
        System.Xml.XmlDocument _Document;
        System.Collections.Generic.Dictionary<System.UInt32, System.Object> _Objects;

        public GameLoader(System.String FileName)
        {
            _Document = new System.Xml.XmlDocument();
            _Document.Load(FileName);
            _Objects = new System.Collections.Generic.Dictionary<System.UInt32, System.Object>();
        }

        public void Load(ButtonOffice.Game Game)
        {
            System.Xml.XmlElement GameElement = (System.Xml.XmlElement)_Document.SelectSingleNode("//button-office/game");

            _AssertElementAndType(GameElement, "ButtonOffice.Game");
        }

        private void _AssertElementAndType(System.Xml.XmlElement Element, System.String Type)
        {
            if(Element == null)
            {
                throw new System.FormatException();
            }
            if(Element.Attributes["type"] == null)
            {
                throw new System.FormatException();
            }
            if(Element.Attributes["type"].Value != Type)
            {
                throw new System.FormatException();
            }
        }
    }
}

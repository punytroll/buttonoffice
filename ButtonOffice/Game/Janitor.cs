namespace ButtonOffice
{
    internal class Janitor : ButtonOffice.Person
    {
        private System.Collections.Generic.Queue<ButtonOffice.Desk> _CleaningTargets;

        public Janitor() :
            base(ButtonOffice.Type.Janitor)
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.JanitorStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.JanitorBackgroundColor;
            _BorderColor = ButtonOffice.Data.JanitorBorderColor;
            _CleaningTargets = new System.Collections.Generic.Queue<ButtonOffice.Desk>();
            _Goal = new ButtonOffice.Goals.JanitorThink();
            _Wage = ButtonOffice.Data.JanitorWage;
            _WorkMinutes = ButtonOffice.Data.JanitorWorkMinutes;
        }

        public void ClearCleaningTargets()
        {
            _CleaningTargets.Clear();
        }

        public void DequeueCleaningTarget()
        {
            _CleaningTargets.Dequeue();
        }

        public void EnqueueCleaningTarget(ButtonOffice.Desk Desk)
        {
            _CleaningTargets.Enqueue(Desk);
        }

        public ButtonOffice.Desk PeekCleaningTarget()
        {
            if(_CleaningTargets.Count > 0)
            {
                return _CleaningTargets.Peek();
            }
            else
            {
                return null;
            }
        }

        public override System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = base.Save(GameSaver);
            System.Xml.XmlElement CleaningTargetsElement = GameSaver.CreateElement("cleaning-targets");

            foreach(ButtonOffice.Desk Desk in _CleaningTargets)
            {
                CleaningTargetsElement.AppendChild(GameSaver.CreateProperty("desk", Desk));
            }
            Result.AppendChild(CleaningTargetsElement);

            return Result;
        }

        public override void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            base.Load(GameLoader, Element);
            foreach(ButtonOffice.Desk Desk in GameLoader.LoadDeskList(Element, "cleaning-targets", "desk"))
            {
                _CleaningTargets.Enqueue(Desk);
            }
        }
    }
}

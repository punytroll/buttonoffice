namespace ButtonOffice
{
    public class Janitor : ButtonOffice.Person
    {
        private readonly System.Collections.Generic.Queue<ButtonOffice.Desk> _CleaningTargets;

        public Janitor()
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.JanitorStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.JanitorBackgroundColor;
            _BorderColor = ButtonOffice.Data.JanitorBorderColor;
            _CleaningTargets = new System.Collections.Generic.Queue<ButtonOffice.Desk>();
            _Wage = ButtonOffice.Data.JanitorWage;
            _WorkMinutes = ButtonOffice.Data.JanitorWorkMinutes;
            _Mind.SetRootGoal(new ButtonOffice.Goals.JanitorThink());
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

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("cleaning-targets", _CleaningTargets);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            foreach(var Desk in ObjectStore.LoadDesks("cleaning-targets"))
            {
                _CleaningTargets.Enqueue(Desk);
            }
        }
    }
}

namespace ButtonOffice
{
    internal class Janitor : ButtonOffice.Person
    {
        private System.Collections.Generic.Queue<ButtonOffice.Desk> _CleaningTargets;

        public Janitor() :
            base(ButtonOffice.Type.Janitor)
        {
            _CleaningTargets = new System.Collections.Generic.Queue<ButtonOffice.Desk>();
        }

        public void DropAllCleaningTargets()
        {
            _CleaningTargets.Clear();
        }

        public void DropFirstCleaningTarget()
        {
            _CleaningTargets.Dequeue();
        }

        public void EnqueueCleaningTarget(ButtonOffice.Desk CleaningTarget)
        {
            _CleaningTargets.Enqueue(CleaningTarget);
        }

        public System.Int32 GetNumberOfCleaningTargets()
        {
            return _CleaningTargets.Count;
        }

        public ButtonOffice.Desk PeekFirstCleaningTarget()
        {
            return _CleaningTargets.Peek();
        }
    }
}

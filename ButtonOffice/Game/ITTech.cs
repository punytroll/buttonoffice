namespace ButtonOffice
{
    internal class ITTech : ButtonOffice.Person
    {
        private System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> _RepairingTarget;

        public ITTech() :
            base(ButtonOffice.Type.ITTech)
        {
            _RepairingTarget = null;
        }

        public void DropRepairingTarget()
        {
            System.Diagnostics.Debug.Assert(_RepairingTarget != null);
            _RepairingTarget = null;
        }

        public ButtonOffice.BrokenThing GetBrokenThing()
        {
            System.Diagnostics.Debug.Assert(_RepairingTarget != null);

            return _RepairingTarget.Second;
        }

        public ButtonOffice.Office GetOffice()
        {
            System.Diagnostics.Debug.Assert(_RepairingTarget != null);

            return _RepairingTarget.First;
        }

        public void SetRepairingTarget(ButtonOffice.Office Office, ButtonOffice.BrokenThing BrokenThing)
        {
            System.Diagnostics.Debug.Assert(_RepairingTarget == null);
            _RepairingTarget = new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Office, BrokenThing);
        }
    }
}

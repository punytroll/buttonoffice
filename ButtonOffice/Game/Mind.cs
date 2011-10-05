namespace ButtonOffice
{
    internal class Mind : ButtonOffice.PersistentObject
    {
        protected ButtonOffice.Goal _RootGoal;

        public Mind()
        {
            _RootGoal = null;
        }

        public void Move(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            System.Collections.Generic.List<ButtonOffice.Goal> ParentGoals = new System.Collections.Generic.List<ButtonOffice.Goal>();
            ButtonOffice.Goal CurrentGoal = _RootGoal;

            while(CurrentGoal != null)
            {
                ButtonOffice.Goal NextGoal = null;

                if(CurrentGoal.HasSubGoals() == true)
                {
                    NextGoal = CurrentGoal.GetFirstSubGoal();
                }
                if(CurrentGoal.GetState() == ButtonOffice.GoalState.Inactive)
                {
                    CurrentGoal.Activate(Game, Person);
                }
                if(CurrentGoal.GetState() == ButtonOffice.GoalState.Active)
                {
                    CurrentGoal.Execute(Game, Person, DeltaMinutes);
                }
                if(CurrentGoal.GetState() == ButtonOffice.GoalState.Done)
                {
                    System.Collections.Generic.Stack<ButtonOffice.Goal> TerminateGoals = new System.Collections.Generic.Stack<ButtonOffice.Goal>();

                    TerminateGoals.Push(CurrentGoal);
                    while(TerminateGoals.Count > 0)
                    {
                        ButtonOffice.Goal TerminateGoal = TerminateGoals.Peek();

                        if(TerminateGoal.HasSubGoals() == true)
                        {
                            while(TerminateGoal.HasSubGoals() == true)
                            {
                                TerminateGoals.Push(TerminateGoal.GetFirstSubGoal());
                                TerminateGoal.RemoveFirstSubGoal();
                            }
                        }
                        else
                        {
                            TerminateGoals.Pop().Terminate(Game, Person);
                        }
                    }
                    if(ParentGoals.Count > 0)
                    {
                        ParentGoals.GetLast().RemoveFirstSubGoal();
                    }
                    else
                    {
                        _RootGoal = null;
                    }
                    CurrentGoal = null;
                    NextGoal = null;
                }
                if(NextGoal != null)
                {
                    ParentGoals.Add(CurrentGoal);
                    CurrentGoal = NextGoal;
                }
                else
                {
                    CurrentGoal = null;
                }
            }
        }

        public void SetRootGoal(ButtonOffice.Goal RootGoal)
        {
            _RootGoal = RootGoal;
        }

        public System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = GameSaver.CreateElement("mind");

            Result.AppendChild(GameSaver.CreateProperty("root-goal", _RootGoal));

            return Result;
        }

        public void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            _RootGoal = GameLoader.LoadGoalProperty(Element, "root-goal");
        }
    }
}

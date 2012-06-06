namespace ButtonOffice.UnitTest
{
    [ButtonOffice.UnitTest.TestCollection()]
    internal class MindTests
    {
        [ButtonOffice.UnitTest.Test()]
        internal static void Test_001_ConstructedMindIsEmpty()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Mind Mind = new ButtonOffice.Mind();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();

            Mind.Move(Game, Person, 0.1f);
        }

        private class TraceGoal : ButtonOffice.Goal
        {
            private readonly System.Collections.Generic.List<System.String> _Trace;

            public System.Collections.Generic.List<System.String> Trace
            {
                get
                {
                    return _Trace;
                }
            }

            public TraceGoal()
            {
                _Trace = new System.Collections.Generic.List<string>();
            }

            protected override void _OnAbort(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add("> _OnAbort()");
                base._OnAbort(Game, Person);
                _Trace.Add("< _OnAbort()");
            }

            protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
            {
                _Trace.Add("> _OnExecute(" + DeltaMinutes.ToString(System.Globalization.CultureInfo.InvariantCulture) + ")");
                base._OnExecute(Game, Person, DeltaMinutes);
                Finish(Game, Person);
                _Trace.Add("< _OnExecute(" + DeltaMinutes.ToString(System.Globalization.CultureInfo.InvariantCulture) + ")");
            }

            protected override void _OnFinish(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add("> _OnFinish()");
                base._OnFinish(Game, Person);
                _Trace.Add("< _OnFinish()");
            }

            protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add("> _OnInitialize()");
                base._OnInitialize(Game, Person);
                _Trace.Add("< _OnInitialize()");
            }

            protected override void _OnResume(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add("> _OnResume()");
                base._OnResume(Game, Person);
                _Trace.Add("< _OnResume()");
            }

            protected override void _OnSuspend(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add("> _OnSuspend()");
                base._OnSuspend(Game, Person);
                _Trace.Add("< _OnSuspend()");
            }

            protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add("> _OnTerminate()");
                base._OnTerminate(Game, Person);
                _Trace.Add("< _OnTerminate()");
            }
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_002_AddGoalToMind()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Mind Mind = new ButtonOffice.Mind();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.UnitTest.MindTests.TraceGoal Goal = new ButtonOffice.UnitTest.MindTests.TraceGoal();

            Mind.SetRootGoal(Goal);
            Mind.Move(Game, Person, 0.1f);
        }
    }
}

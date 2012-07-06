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

        private enum TraceEvents
        {
            EnterOnAbort,
            ExitOnAbort,
            EnterOnExecute,
            ExitOnExecute,
            EnterFinish,
            ExitFinish,
            EnterOnInitialize,
            ExitOnInitialize,
            EnterOnResume,
            ExitOnResume,
            EnterOnSuspend,
            ExitOnSuspend,
            EnterOnTerminate,
            ExitOnTerminate
        }

        private class TraceGoal : ButtonOffice.Goal
        {
            private readonly System.Collections.Generic.List<TraceEvents> _Trace;

            public System.Collections.Generic.List<TraceEvents> Trace
            {
                get
                {
                    return _Trace;
                }
            }

            protected TraceGoal()
            {
                _Trace = new System.Collections.Generic.List<TraceEvents>();
            }

            protected override void _OnAbort(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add(TraceEvents.EnterOnAbort);
                base._OnAbort(Game, Person);
                _OnTraceAbort(Game, Person);
                _Trace.Add(TraceEvents.ExitOnAbort);
            }

            protected virtual void _OnTraceAbort(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
            }

            protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
            {
                _Trace.Add(TraceEvents.EnterOnExecute);
                base._OnExecute(Game, Person, DeltaMinutes);
                _OnTraceExecute(Game, Person, DeltaMinutes);
                _Trace.Add(TraceEvents.ExitOnExecute);
            }

            protected virtual void _OnTraceExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
            {
            }

            protected override void _OnFinish(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add(TraceEvents.EnterFinish);
                base._OnFinish(Game, Person);
                _OnTraceFinish(Game, Person);
                _Trace.Add(TraceEvents.ExitFinish);
            }

            protected virtual void _OnTraceFinish(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
            }

            protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add(TraceEvents.EnterOnInitialize);
                base._OnInitialize(Game, Person);
                _OnTraceInitialize(Game, Person);
                _Trace.Add(TraceEvents.ExitOnInitialize);
            }

            protected virtual void _OnTraceInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
            }

            protected override void _OnResume(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add(TraceEvents.EnterOnResume);
                base._OnResume(Game, Person);
                _OnTraceResume(Game, Person);
                _Trace.Add(TraceEvents.ExitOnResume);
            }

            protected virtual void _OnTraceResume(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
            }

            protected override void _OnSuspend(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add(TraceEvents.EnterOnSuspend);
                base._OnSuspend(Game, Person);
                _OnTraceSuspend(Game, Person);
                _Trace.Add(TraceEvents.ExitOnSuspend);
            }

            protected virtual void _OnTraceSuspend(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
            }

            protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
                _Trace.Add(TraceEvents.EnterOnTerminate);
                base._OnTerminate(Game, Person);
                _OnTraceTerminate(Game, Person);
                _Trace.Add(TraceEvents.ExitOnTerminate);
            }

            protected virtual void _OnTraceTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
            {
            }
        }

        private class FinishOnFirstExecute : ButtonOffice.UnitTest.MindTests.TraceGoal
        {
            protected override void _OnTraceExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
            {
                Finish(Game, Person);
            }
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_002_AddGoalToMind()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Mind Mind = new ButtonOffice.Mind();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.UnitTest.MindTests.TraceGoal Goal = new ButtonOffice.UnitTest.MindTests.FinishOnFirstExecute();

            Mind.SetRootGoal(Goal);
            Mind.Move(Game, Person, 0.1f);
        }
    }
}

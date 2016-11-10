using System;
using System.Collections.Generic;

namespace ButtonOffice.UnitTest
{
    [TestCollection()]
    internal class MindTests
    {
        [Test()]
        internal static void Test_001_ConstructedMindIsEmpty()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Mind = new Mind();
            var Person = new Janitor();

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

        private class TraceGoal : Goal
        {
            private readonly List<TraceEvents> _Trace;

            public List<TraceEvents> Trace
            {
                get
                {
                    return _Trace;
                }
            }

            protected TraceGoal()
            {
                _Trace = new List<TraceEvents>();
            }

            protected override void _OnAbort(Game Game, PersistentObject Actor)
            {
                _Trace.Add(TraceEvents.EnterOnAbort);
                base._OnAbort(Game, Actor);
                _OnTraceAbort(Game, Actor);
                _Trace.Add(TraceEvents.ExitOnAbort);
            }

            protected virtual void _OnTraceAbort(Game Game, PersistentObject Actor)
            {
            }

            protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
            {
                _Trace.Add(TraceEvents.EnterOnExecute);
                base._OnExecute(Game, Actor, DeltaMinutes);
                _OnTraceExecute(Game, Actor, DeltaMinutes);
                _Trace.Add(TraceEvents.ExitOnExecute);
            }

            protected virtual void _OnTraceExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
            {
            }

            protected override void _OnFinish(Game Game, PersistentObject Actor)
            {
                _Trace.Add(TraceEvents.EnterFinish);
                base._OnFinish(Game, Actor);
                _OnTraceFinish(Game, Actor);
                _Trace.Add(TraceEvents.ExitFinish);
            }

            protected virtual void _OnTraceFinish(Game Game, PersistentObject Actor)
            {
            }

            protected override void _OnInitialize(Game Game, PersistentObject Actor)
            {
                _Trace.Add(TraceEvents.EnterOnInitialize);
                base._OnInitialize(Game, Actor);
                _OnTraceInitialize(Game, Actor);
                _Trace.Add(TraceEvents.ExitOnInitialize);
            }

            protected virtual void _OnTraceInitialize(Game Game, PersistentObject Actor)
            {
            }

            protected override void _OnResume(Game Game, PersistentObject Actor)
            {
                _Trace.Add(TraceEvents.EnterOnResume);
                base._OnResume(Game, Actor);
                _OnTraceResume(Game, Actor);
                _Trace.Add(TraceEvents.ExitOnResume);
            }

            protected virtual void _OnTraceResume(Game Game, PersistentObject Actor)
            {
            }

            protected override void _OnSuspend(Game Game, PersistentObject Actor)
            {
                _Trace.Add(TraceEvents.EnterOnSuspend);
                base._OnSuspend(Game, Actor);
                _OnTraceSuspend(Game, Actor);
                _Trace.Add(TraceEvents.ExitOnSuspend);
            }

            protected virtual void _OnTraceSuspend(Game Game, PersistentObject Actor)
            {
            }

            protected override void _OnTerminate(Game Game, PersistentObject Actor)
            {
                _Trace.Add(TraceEvents.EnterOnTerminate);
                base._OnTerminate(Game, Actor);
                _OnTraceTerminate(Game, Actor);
                _Trace.Add(TraceEvents.ExitOnTerminate);
            }

            protected virtual void _OnTraceTerminate(Game Game, PersistentObject Actor)
            {
            }
        }

        private class FinishOnFirstExecute : MindTests.TraceGoal
        {
            protected override void _OnTraceExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
            {
                Finish(Game, Actor);
            }
        }

        [Test()]
        internal static void Test_002_AddGoalToMind()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Mind = new Mind();
            var Person = new Janitor();
            var Goal = new MindTests.FinishOnFirstExecute();

            Mind.SetRootGoal(Goal);
            Mind.Move(Game, Person, 0.1f);
        }
    }
}

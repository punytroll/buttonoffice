namespace ButtonOffice.UnitTest
{
    [ButtonOffice.UnitTest.TestCollection()]
    internal class GoalSimpleStateTransitions
    {
        [ButtonOffice.UnitTest.Test()]
        internal static void Test_001_ConstructedGoalIsPrestine()
        {
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Pristine);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_002_PrestineGoalCanInitialize()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.NoAssertion(() => Goal.Initialize(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_003_PrestineGoalInitializedIsReady()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Ready);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_004_PrestineGoalCannotResume()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_005_PrestineGoalCannotSuspend()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_006_PrestineGoalCannotAbort()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReadyOrExecuting, () => Goal.Abort(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_007_PrestineGoalCannotFinish()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_008_PrestineGoalCannotTerminate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_009_ReadyGoalCanResume()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.NoAssertion(() => Goal.Resume(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_010_ReadyGoalResumedIsExecuting()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Executing);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_011_ReadyGoalCanAbort()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.NoAssertion(() => Goal.Abort(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_012_ReadyGoalAbortedIsDone()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Abort(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Done);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_013_ReadyGoalCannotInitialize()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_014_ReadyGoalCannotSuspend()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_015_ReadyGoalCannotTerminate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_016_ReadyGoalCannotFinish()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_017_ExecutingGoalCanSuspend()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            ButtonOffice.UnitTest.Assert.NoAssertion(() => Goal.Suspend(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_018_ExecutingGoalSuspendedIsReady()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Suspend(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Ready);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_019_ExecutingGoalCanAbort()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            ButtonOffice.UnitTest.Assert.NoAssertion(() => Goal.Abort(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_020_ExecutingGoalAbortedIsDone()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Abort(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Done);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_021_ExecutingGoalCanFinish()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            ButtonOffice.UnitTest.Assert.NoAssertion(() => Goal.Finish(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_022_ExecutingGoalFinishedIsDone()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Done);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_023_ExecutingGoalCannotInitialize()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_024_ExecutingGoalCannotResume()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_025_ExecutingGoalCannotTerminate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_026_DoneGoalCanTerminate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            ButtonOffice.UnitTest.Assert.NoAssertion(() => Goal.Terminate(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_027_DoneGoalTerminatedIsTerminated()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Terminated);
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_028_DoneGoalCannotInitialize()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_029_DoneGoalCannotResume()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_030_DoneGoalCannotSuspend()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_031_DoneGoalCannotAbort()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReadyOrExecuting, () => Goal.Abort(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_032_DoneGoalCannotFinish()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_033_TerminatedGoalCannotInitialize()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_034_TerminatedGoalCannotResume()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_035_TerminatedGoalCannotSuspend()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_036_TerminatedGoalCannotAbort()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReadyOrExecuting, () => Goal.Abort(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_037_TerminatedGoalCannotTerminate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_038_TerminatedGoalCannotFinish()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }
    }
}

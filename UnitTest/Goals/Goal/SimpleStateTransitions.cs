using System.Diagnostics;

namespace ButtonOffice.UnitTest
{
    [TestCollection]
    internal class GoalSimpleStateTransitions
    {
        [Test]
        internal static void Test_001_ConstructedGoalIsPrestine()
        {
            var Goal = new Goal();

            Debug.Assert(Goal.GetState() == GoalState.Pristine);
        }

        [Test]
        internal static void Test_002_PrestineGoalCanInitialize()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Assert.NoAssertion(() => Goal.Initialize(Game, Person));
        }

        [Test]
        internal static void Test_003_PrestineGoalInitializedIsReady()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Debug.Assert(Goal.GetState() == GoalState.Ready);
        }

        [Test]
        internal static void Test_004_PrestineGoalCannotResume()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Assert.Assertion(AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [Test]
        internal static void Test_005_PrestineGoalCannotSuspend()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [Test]
        internal static void Test_006_PrestineGoalCannotAbort()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Assert.Assertion(AssertMessages.CurrentStateIsNotReadyOrExecuting, () => Goal.Abort(Game, Person));
        }

        [Test]
        internal static void Test_007_PrestineGoalCannotFinish()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }

        [Test]
        internal static void Test_008_PrestineGoalCannotTerminate()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Assert.Assertion(AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [Test]
        internal static void Test_009_ReadyGoalCanResume()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Assert.NoAssertion(() => Goal.Resume(Game, Person));
        }

        [Test]
        internal static void Test_010_ReadyGoalResumedIsExecuting()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Debug.Assert(Goal.GetState() == GoalState.Executing);
        }

        [Test]
        internal static void Test_011_ReadyGoalCanAbort()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Assert.NoAssertion(() => Goal.Abort(Game, Person));
        }

        [Test]
        internal static void Test_012_ReadyGoalAbortedIsDone()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Abort(Game, Person);
            Debug.Assert(Goal.GetState() == GoalState.Done);
        }

        [Test]
        internal static void Test_013_ReadyGoalCannotInitialize()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [Test]
        internal static void Test_014_ReadyGoalCannotSuspend()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [Test]
        internal static void Test_015_ReadyGoalCannotTerminate()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [Test]
        internal static void Test_016_ReadyGoalCannotFinish()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }

        [Test]
        internal static void Test_017_ExecutingGoalCanSuspend()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Assert.NoAssertion(() => Goal.Suspend(Game, Person));
        }

        [Test]
        internal static void Test_018_ExecutingGoalSuspendedIsReady()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Suspend(Game, Person);
            Debug.Assert(Goal.GetState() == GoalState.Ready);
        }

        [Test]
        internal static void Test_019_ExecutingGoalCanAbort()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Assert.NoAssertion(() => Goal.Abort(Game, Person));
        }

        [Test]
        internal static void Test_020_ExecutingGoalAbortedIsDone()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Abort(Game, Person);
            Debug.Assert(Goal.GetState() == GoalState.Done);
        }

        [Test()]
        internal static void Test_021_ExecutingGoalCanFinish()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Assert.NoAssertion(() => Goal.Finish(Game, Person));
        }

        [Test]
        internal static void Test_022_ExecutingGoalFinishedIsDone()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Debug.Assert(Goal.GetState() == GoalState.Done);
        }

        [Test]
        internal static void Test_023_ExecutingGoalCannotInitialize()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [Test]
        internal static void Test_024_ExecutingGoalCannotResume()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [Test]
        internal static void Test_025_ExecutingGoalCannotTerminate()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [Test]
        internal static void Test_026_DoneGoalCanTerminate()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Assert.NoAssertion(() => Goal.Terminate(Game, Person));
        }

        [Test]
        internal static void Test_027_DoneGoalTerminatedIsTerminated()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            Debug.Assert(Goal.GetState() == GoalState.Terminated);
        }

        [Test]
        internal static void Test_028_DoneGoalCannotInitialize()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [Test]
        internal static void Test_029_DoneGoalCannotResume()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [Test]
        internal static void Test_030_DoneGoalCannotSuspend()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [Test]
        internal static void Test_031_DoneGoalCannotAbort()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotReadyOrExecuting, () => Goal.Abort(Game, Person));
        }

        [Test]
        internal static void Test_032_DoneGoalCannotFinish()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }

        [Test]
        internal static void Test_033_TerminatedGoalCannotInitialize()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotPrestine, () => Goal.Initialize(Game, Person));
        }

        [Test]
        internal static void Test_034_TerminatedGoalCannotResume()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotReady, () => Goal.Resume(Game, Person));
        }

        [Test]
        internal static void Test_035_TerminatedGoalCannotSuspend()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Suspend(Game, Person));
        }

        [Test]
        internal static void Test_036_TerminatedGoalCannotAbort()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotReadyOrExecuting, () => Goal.Abort(Game, Person));
        }

        [Test]
        internal static void Test_037_TerminatedGoalCannotTerminate()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotDone, () => Goal.Terminate(Game, Person));
        }

        [Test]
        internal static void Test_038_TerminatedGoalCannotFinish()
        {
            var Game = ButtonOffice.Game.CreateNew();
            var Person = new Janitor();
            var Goal = new Goal();

            Goal.Initialize(Game, Person);
            Goal.Resume(Game, Person);
            Goal.Finish(Game, Person);
            Goal.Terminate(Game, Person);
            Assert.Assertion(AssertMessages.CurrentStateIsNotExecuting, () => Goal.Finish(Game, Person));
        }
    }
}

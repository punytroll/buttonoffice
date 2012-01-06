namespace ButtonOffice.UnitTest
{
    [ButtonOffice.UnitTest.TestCollection()]
    internal class Goal
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

            ButtonOffice.UnitTest.Assert.NoAssertion(delegate()
            {
                Goal.Initialize(Game, Person);
            });
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

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReady, delegate()
            {
                Goal.Resume(Game, Person);
            });
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_005_PrestineGoalCannotSuspend()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, delegate()
            {
                Goal.Suspend(Game, Person);
            });
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_006_PrestineGoalCannotAbort()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReadyOrExecuting, delegate()
            {
                Goal.Abort(Game, Person);
            });
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_007_PrestineGoalCannotFinish()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, delegate()
            {
                Goal.Finish(Game, Person);
            });
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_008_PrestineGoalCannotTerminate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotDone, delegate()
            {
                Goal.Terminate(Game, Person);
            });
        }

        [ButtonOffice.UnitTest.Test()]
        internal static void Test_009_InitializeMakesGoalReady()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Ready);
        }
    }
}

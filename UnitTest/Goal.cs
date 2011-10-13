namespace ButtonOffice.UnitTest
{
    internal class Goal
    {
        internal static void RunTests()
        {
            Test_001_ConstructedGoalIsPrestine();
            Test_002_PrestineGoalCanInitialize();
            Test_003_PrestineGoalCannotResume();
            Test_004_PrestineGoalCannotSuspend();
            Test_005_PrestineGoalCannotAbort();
            Test_006_PrestineGoalCannotFinish();
            Test_007_PrestineGoalCannotTerminate();
            Test_008_InitializeMakesGoalReady();
        }

        internal static void Test_001_ConstructedGoalIsPrestine()
        {
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Pristine);
        }

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

        internal static void Test_003_PrestineGoalCannotResume()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReady, delegate()
            {
                Goal.Resume(Game, Person);
            });
        }

        internal static void Test_004_PrestineGoalCannotSuspend()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, delegate()
            {
                Goal.Suspend(Game, Person);
            });
        }

        internal static void Test_005_PrestineGoalCannotAbort()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotReadyOrExecuting, delegate()
            {
                Goal.Abort(Game, Person);
            });
        }

        internal static void Test_006_PrestineGoalCannotFinish()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotExecuting, delegate()
            {
                Goal.Finish(Game, Person);
            });
        }

        internal static void Test_007_PrestineGoalCannotTerminate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.Assertion(ButtonOffice.AssertMessages.CurrentStateIsNotDone, delegate()
            {
                Goal.Terminate(Game, Person);
            });
        }

        internal static void Test_008_InitializeMakesGoalReady()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            Goal.Initialize(Game, Person);
            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Ready);
        }
    }
}

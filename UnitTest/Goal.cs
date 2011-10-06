namespace ButtonOffice.UnitTest
{
    internal class Goal
    {
        internal static void RunTests()
        {
            Test1_ConstructedGoalIsPrestine();
            Test2_PrestineGoalCanActivate();
        }

        internal static void Test1_ConstructedGoalIsPrestine()
        {
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.AreEqual(Goal.GetState(), ButtonOffice.GoalState.Pristine);
        }

        internal static void Test2_PrestineGoalCanActivate()
        {
            ButtonOffice.Game Game = ButtonOffice.Game.CreateNew();
            ButtonOffice.Person Person = new ButtonOffice.Janitor();
            ButtonOffice.Goal Goal = new ButtonOffice.Goal();

            ButtonOffice.UnitTest.Assert.NoAssertion(delegate()
            {
                Goal.Activate(Game, Person);
            });
        }
    }
}

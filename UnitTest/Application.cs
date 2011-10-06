namespace ButtonOffice.UnitTest
{
    internal class Application
    {
        internal static void Main()
        {
            ButtonOffice.UnitTest.Goal.RunTests();
            System.Console.WriteLine("All tests successfull.\n\nPress any key to finish.");
            System.Console.ReadKey();
        }
    }
}

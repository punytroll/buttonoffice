namespace ButtonOffice.UnitTest
{
    internal class Application
    {
        internal static void Main()
        {
            System.Reflection.Assembly Assembly = System.Reflection.Assembly.GetExecutingAssembly();

            foreach(System.Type Type in Assembly.GetTypes())
            {
                if(Type.GetCustomAttributes(typeof(ButtonOffice.UnitTest.TestCollectionAttribute), true).Length > 0)
                {
                    foreach(System.Reflection.MethodInfo MethodInfo in Type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public))
                    {
                        if(MethodInfo.GetCustomAttributes(typeof(ButtonOffice.UnitTest.TestAttribute), true).Length > 0)
                        {
                            System.Console.WriteLine("Calling test " + Type.FullName + "." + MethodInfo.Name + "().");
                            MethodInfo.Invoke(null, new System.Object[] { });
                        }
                    }
                }
            }
            System.Console.WriteLine("\nAll tests successfull.\n\nPress any key to finish.");
            System.Console.ReadKey();
        }
    }
}

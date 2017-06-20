using System;
using System.Reflection;

namespace ButtonOffice.UnitTest
{
    internal class Application
    {
        internal static void Main()
        {
            var Assembly = System.Reflection.Assembly.GetExecutingAssembly();

            foreach(var Type in Assembly.GetTypes())
            {
                if(Type.GetCustomAttributes(typeof(TestCollectionAttribute), true).Length > 0)
                {
                    foreach(var MethodInfo in Type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                    {
                        if(MethodInfo.GetCustomAttributes(typeof(TestAttribute), true).Length > 0)
                        {
                            Console.WriteLine("Calling test " + Type.FullName + "." + MethodInfo.Name + "().");
                            try
                            {
                                MethodInfo.Invoke(null, new Object[] { });
                            }
                            catch(Exception Exception)
                            {
                                Console.WriteLine(Exception.InnerException);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nAll tests successfull.\n\nPress any key to finish.");
            Console.ReadKey();
        }
    }
}

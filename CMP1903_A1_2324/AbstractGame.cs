using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal abstract class AbstractGame : IGame
    {
        public virtual void StartGame()
        {
            Console.WriteLine("Starting the game!");
        }

        public virtual void EndGame()
        {
            Console.WriteLine("Ending the game!");
        }

        public virtual void DisplayMenu()
        {
            Console.WriteLine("Displaying the menu!");
        }

        protected void InvokeMethod(string functionName)
        {
            Type type = GetType();

            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                if (method.Name == functionName)
                {
                    method.Invoke(this, null);
                    return;
                }
            }

            Console.WriteLine($"There was an unknown error when trying to invoke method {functionName}.");
        }
    }
}

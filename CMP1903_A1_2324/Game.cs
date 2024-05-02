using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal abstract class Game : IGame
    {
        // Setup default parameters
        protected bool _gameOver = false;
        public bool IsGameOver() { return _gameOver; }

        // Setup default/expected methods
        public virtual void StartGame()
        {
            Console.WriteLine("Starting the game!");
        }

        public virtual void EndGame()
        {
            Console.WriteLine("Ending the game!");
        }

        // Used to fetch a method from a string of it's name
        protected void InvokeMethod(string functionName)
        {
            // Gets the object type that InvokeMethod is being ran on
            Type type = GetType();

            // Searches the formerly found object type for methods, including those public, or not and any instance which may be a method (BindingFlags specifies how search is conducted)
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // Runs over all methods
            foreach (var method in methods)
            {
                // If a method matches with the given name
                if (method.Name == functionName)
                {
                    // Run the method!
                    method.Invoke(this, null);
                    return;
                }
            }

            // No method of the given name was found, inform of the case.
            Console.WriteLine($"There was an unknown error when trying to invoke method {functionName}.");
        }
    }
}

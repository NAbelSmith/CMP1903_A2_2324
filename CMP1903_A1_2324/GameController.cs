using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class GameController
    {
        public GameController() {
            DisplayMenu();
        }
        internal void PlaySevensOut()
        {
            // Instantiate a new game of SevensOut
            new SevensOut();
        }

        internal void PlayThreeOrMore()
        {
            // Instantiate a new game of ThreeOrMore
            new ThreeOrMore();
        }

        internal void RunTest()
        {
            // Create new Testing object
            Testing test = new Testing();

            // Test game functions as intended
            test.RunTest();
        }

        internal void DisplayMenu()
        {
            // Player Selection Menu
            GameMenu menu = new GameMenu();

            // Add options
            menu.AddOption("Play: Sevens Out", "PlaySevensOut");
            menu.AddOption("Play: Three Or More", "PlayThreeOrMore");
            menu.AddOption("Test: Run a test of both Sevens Out and Three Or More", "RunTest");
            menu.AddOption("Quit: Quit execution", "Quit");

            // Display menu
            menu.DisplayMenu();

            // Take player input and run the option method
            string selectedOption = menu.FetchSelectedOption();
            InvokeMethod(selectedOption);
        }

        // Used to fetch a method from a string of it's name
        internal void InvokeMethod(string functionName)
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

        internal void Quit() {
            Console.WriteLine("Successfully ended selection!");
        }
    }
}

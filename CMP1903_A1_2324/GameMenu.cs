using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class GameMenu
    {
        // List to hold menu options
        private List<(string, string)> _menuOptions;

        public GameMenu()
        {
            // Instantiate the list
            _menuOptions = new List<(string, string)>();
        }

        // Add an option to the total menu options
        public void AddOption(string optionDescription, string functionName)
        {
            // Added in the form of a tuple
            _menuOptions.Add((functionName, optionDescription));
        }

        // Remove an option from the total menu options
        public void RemoveOption(string functionName) 
        {
            // Removed where the function name is the found
            _menuOptions.RemoveAll(tuple => tuple.Item1 == functionName);
        }

        // Reset the menu entirely
        public void ResetMenu()
        {
            // Remove all options
            _menuOptions.Clear();
        }

        // Display the menu
        public void DisplayMenu()
        {
            int menuCounter = 1;

            // Output all of the options to the console
            foreach ((string, string) menuOption in _menuOptions) {
                Console.WriteLine($"[{menuCounter}] {menuOption.Item2}");
                menuCounter++;
            }
        }

        // Check the desired option based on input
        public string FetchSelectedOption()
        {
            Console.WriteLine($"Please input a number between 1 and {_menuOptions.Count} based on the Menu Options.");

            // Gather user input & only allow numeric values 0-9
            string userInput = Regex.Replace(Console.ReadLine(), "[^0-9]", "");
            int convertedInput;

            // Valid input detected
            if (userInput.Length > 0)
            {
                // Convert input to integer
                convertedInput = Convert.ToInt32(userInput);

                // Integer was not within range
                if (convertedInput < 1 || convertedInput > _menuOptions.Count)
                {
                    Console.WriteLine($"The number must be between 1 and {_menuOptions.Count}.");
                    return FetchSelectedOption();
                }

                // Return the function name
                Thread.Sleep(2000);
                return _menuOptions[convertedInput - 1].Item1;
            }

            // Invalid input, retry
            Console.WriteLine("You did not input a valid number, please try again!");
            return FetchSelectedOption();
        }
    }
}

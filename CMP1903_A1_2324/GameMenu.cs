using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class GameMenu
    {
        private List<(string, string)> _menuOptions;

        public GameMenu()
        {
            _menuOptions = new List<(string, string)>();
        }

        public void AddOption(string optionDescription, string functionName)
        {
            _menuOptions.Add((functionName, optionDescription));
        }

        public void RemoveOption(string functionName) 
        {
            _menuOptions.RemoveAll(tuple => tuple.Item1 == functionName);
        }

        public void ResetMenu()
        {
            _menuOptions.Clear();
        }

        public void DisplayMenu()
        {
            int menuCounter = 1;

            foreach ((string, string) menuOption in _menuOptions) {
                Console.WriteLine($"[{menuCounter}] {menuOption.Item2}");
                menuCounter++;
            }
        }

        public string FetchSelectedOption()
        {
            Console.WriteLine($"Please input a number between 1 and {_menuOptions.Count} based on the Menu Options.");

            string userInput = Regex.Replace(Console.ReadLine(), "[^0-9]", "");
            int convertedInput;

            if (userInput.Length > 0)
            {
                convertedInput = Convert.ToInt32(userInput);

                if (convertedInput < 1 || convertedInput > _menuOptions.Count)
                {
                    Console.WriteLine($"The number must be between 1 and {_menuOptions.Count}.");
                    return FetchSelectedOption();
                }

                return _menuOptions[convertedInput - 1].Item1;
            }

            Console.WriteLine("You did not input a valid number, please try again!");
            return FetchSelectedOption();
        }
    }
}

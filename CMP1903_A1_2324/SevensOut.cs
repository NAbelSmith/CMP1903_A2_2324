using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class SevensOut : AbstractDiceGame
    {
        private bool _computerMode;
        private int _numOfDice = 2;

        public SevensOut()
        {
            GameMenu menu = new GameMenu();
            menu.AddOption("Play: Player vs Player Mode", "PlayPlayer");
            menu.AddOption("Play: Player vs Computer Mode", "PlayComputer");
            menu.AddOption("View: Game Statistics", "ViewStatistics");
            menu.AddOption("Quit: Stop Playing SevensOut", "EndGame");
            menu.DisplayMenu();
            string selectedOption = menu.FetchSelectedOption();
            InvokeMethod(selectedOption);

            // Ensure set value for _numOfDie is within allowed range
            if (_numOfDice <= 1)
            {
                Console.WriteLine($"Set number of die ({_numOfDice}) is lower than the minimum number (2). Set number of die to 2.");
                _numOfDice = 2;
            }

            for (int i = 1;  i <= _numOfDice; i++)
            {
                _diceList.Add(new Die());
            }
        }

        private void PlayRound()
        {

        }

        private void PlayPlayer()
        {
            _computerMode = false;
            Console.WriteLine("Playingh palyer");
        }

        private void PlayComputer()
        {
            _computerMode = true;
            Console.WriteLine("Playingh comp");
        }
    }
}

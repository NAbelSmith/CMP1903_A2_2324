using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class Game
    {
        /*
         * The Game class should create three die objects, roll them, sum and report the total of the three dice rolls.
         *
         * EXTRA: For extra requirements (these aren't required though), the dice rolls could be managed so that the
         * rolls could be continous, and the totals and other statistics could be summarised for example.
         */

        // Create DiceList property
        public Die[] DieList;

        // Create properties (sum variables)
        private int _sum;
        private int _rounds;

        // Number of die
        private int _numOfDie = 3;

        // Method to return number of die - for testing
        public int NumOfDie { get { return _numOfDie; } }

        // Constructor
        public Game()
        {
            // Ensure set value for _numOfDie is within allowed range
            if (_numOfDie == 0)
            {
                Console.WriteLine($"Set number of die ({_numOfDie}) is lower than the minimum number (2). Set number of die to 2.");
                _numOfDie = 2;
            }

            // Instantitae DiceList Property
            DieList = new Die[_numOfDie];

            for (int i = 0; i <=_numOfDie-1; i++)
            {
                DieList[i] = new Die();
            }
        }

        //Methods
        private bool Continue()
        {
            Console.WriteLine($"\nDo you wish to roll the dice again? They have been rolled a total of {_rounds} times (ROUND {_rounds}) so far.\nPlease ensure you only input a Y for yes or N for no, other inputs will not be accepted! (Y/N) ");
            ConsoleKeyInfo userInput = Console.ReadKey();
            if (userInput.KeyChar.ToString().ToUpper() == "Y")
            {
                return true;
            }
            else if (userInput.KeyChar.ToString().ToUpper() == "N")
            {
                Console.WriteLine("\n\nSuccessfully exited rolls!");
                return false;
            }
            Console.WriteLine($"\n\nInput failed - your input of {userInput.KeyChar.ToString().ToUpper()} was rejected!\nYou may only input a Y for yes or N for no, other inputs will be rejected!");
            return Continue();
        }

        private int RollDie(Die die, int dieNum)
        { 
            int dieRoll = die.Roll();
            Console.WriteLine($"Die {dieNum} has been rolled: {dieRoll}");
            _sum += dieRoll;
            return dieRoll;
        }

        public int BeginRound()
        {
            // Increment Total Round Count
            _rounds += 1;

            // Notify user of current round
            Console.WriteLine($"\nBeginning Round {_rounds}\n");

            // Round Total
            int roundTotal = 0;

            // Roll All Die
            for (int i = 0; i <=_numOfDie-1; i++)
            {
                roundTotal += RollDie(DieList[i], i + 1);
            }

            // Output Statistics
            Console.WriteLine($"\nROUND STATISTICS\nRound Total: {roundTotal}\nRound Average: {Convert.ToSingle(roundTotal) / _numOfDie}\n\nGAME STATISTICS\nGame Total: {_sum}\nGame Average: {Convert.ToSingle(_sum) / (3 * _rounds)}");

            return roundTotal;
        }

        public int StartGame()
        {
            BeginRound();

            while (Continue())
            {
                BeginRound();
            }

            return _sum;
        }
    }
}

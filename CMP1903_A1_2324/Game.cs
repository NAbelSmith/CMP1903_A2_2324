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

        // Instantiate dice
        public Die die1 = new Die();
        public Die die2 = new Die();
        public Die die3 = new Die();

        // Create properties (sum variables)
        private int _sum;
        private int _rounds;

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
                return false;
            }

            return Continue();
        }

        public int BeginRound()
        {
            // Increment Total Round Count
            _rounds += 1;

            // Notify user of current round
            Console.WriteLine($"\nBeginning Round {_rounds}\n");

            // Round Total
            int roundTotal = 0;

            // Roll Die 1
            int dieRoll = die1.Roll();
            Console.WriteLine($"Die 1 has been rolled: {dieRoll}");
            _sum += dieRoll;
            roundTotal += dieRoll;

            // Roll Die 2
            dieRoll = die2.Roll();
            Console.WriteLine($"Die 2 has been rolled: {dieRoll}");
            _sum += dieRoll;
            roundTotal += dieRoll;

            // Roll Die 3
            dieRoll = die3.Roll();
            Console.WriteLine($"Die 3 has been rolled: {dieRoll}");
            _sum += dieRoll;
            roundTotal += dieRoll;

            // Output Statistics
            Console.WriteLine($"\nROUND STATISTICS\nRound Total: {roundTotal}\nRound Average: {Convert.ToSingle(roundTotal)/3}\n\nGAME STATISTICS\nGame Total: {_sum}\nGame Average: {Convert.ToSingle(_sum) / (3*_rounds)}");

            if (Continue())
            {
                return BeginRound();
            }

            return _sum;
        }
    }
}

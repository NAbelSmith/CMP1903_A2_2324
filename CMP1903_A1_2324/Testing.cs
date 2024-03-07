using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CMP1903_A1_2324
{
    internal class Testing
    {
        /*
         * This class should test the Game and the Die class.
         * Create a Game object, call the methods and compare their output to expected output.
         * Create a Die object and call its method.
         * Use debug.assert() to make the comparisons and tests.
         */

        //Method
        public void RunTest()
        {
            // Console seperator
            Console.WriteLine("\nRunning a test!");

            // Instantiates a new game and die
            Game game = new Game();
            Die die = new Die();

            // Checks if each die rolled within boundaries
            int rollResult = die.Roll();
            Debug.Assert(rollResult > 1 || rollResult < 7, $"Die Roll error! Die rolled a {rollResult} which is not between 1 and 6." );

            // Checking game for errors
            int sumOfRound = game.BeginRound();

            int combinedValuesOfDice = 0;

            // Checks if each respetive die rolled within boundaries & fetches 
            for (int i = 0; i <= game.NumOfDie-1; i++)
            {
                combinedValuesOfDice += game.DieList[i].Value;
                Debug.Assert(game.DieList[i].Value > 1 || game.DieList[i].Value < 7, $"Die Roll error! Die {i+1} rolled a {game.DieList[i].Value} which is not between 1 and 6.");
            }

            // Checks if sum of round is equal to the actual sum of the rolled values and within expected boundaries
            Debug.Assert(sumOfRound > 2 && sumOfRound < 19, $"Game BeginRound error! Dice accumulated to {sumOfRound} which is outside the maximum and minimum value.");
            Debug.Assert(sumOfRound == (combinedValuesOfDice), $"Game BeginRound error! Dice accumulated to {combinedValuesOfDice} but dice did not add to outputted sum ({sumOfRound}).");

            Console.WriteLine("\nTest complete!");
        }
    }
}

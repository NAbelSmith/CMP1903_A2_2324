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
            Debug.Assert(rollResult < 1 || rollResult > 6, $"Die Roll error! Die rolled a {rollResult} which is not between 1 and 6." );

            // Checking game for errors
            int sumOfRound = game.BeginRound();
            // Checks if sum of round is equal to the actual sum of the rolled values
            Debug.Assert(sumOfRound == (game.die1.value + game.die2.value + game.die3.value), $"Game BeginRound error! Dice accumulated to {sumOfRound} but dice did not add to outputted sum.");
            // Checks if each respetive die rolled within boundaries
            Debug.Assert(game.die1.value < 1 || game.die1.value > 6, $"Die Roll error! Die 1 rolled a {game.die1.value} which is not between 1 and 6.");
            Debug.Assert(game.die2.value < 1 || game.die2.value > 6, $"Die Roll error! Die 2 rolled a {game.die2.value} which is not between 1 and 6.");
            Debug.Assert(game.die3.value < 1 || game.die3.value > 6, $"Die Roll error! Die 3 rolled a {game.die3.value} which is not between 1 and 6.");

            Console.WriteLine("\nTest complete!");
        }
    }
}

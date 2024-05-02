using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Dice Games
                Sevens Out
                2 x dice
                Rules:
	                Roll the two dice, noting the total rolled each time.
	                If it is a 7 - stop.
	                If it is any other number - add it to your total.
		                If it is a double - add double the total to your score (3,3 would add 12 to your total)

                Three or More
                5 x dice
                Rules:
	                Roll all 5 dice hoping for a 3-of-a-kind or better.
	                If 2-of-a-kind is rolled, player may choose to rethrow all, or the remaining dice.
	                3-of-a-kind: 3 points
	                4-of-a-kind: 6 points
	                5-of-a-kind: 12 points
	                First to a total of 20.


                Player can choose either game to play through a menu.
                Play with partner (on the same computer), or against the computer.
                Should be a console implementation - but scope for extending it to a GUI application should be possible.
             * 
             */
            /*
             * Create a Game object and call its methods.
             * Create a Testing object to verify the output and operation of the other classes.
             */

            /*/ Create new Game object
            Game game = new Game();

            // Create new Testing object
            Testing test = new Testing();

            // Test game functions as intended
            test.RunTest();

            // Endless loop
            game.StartGame();*/

            //SevensOut sevensOut = new SevensOut();
            ThreeOrMore threeOrMore = new ThreeOrMore();

            // Stop automatic termination of program
            Console.WriteLine("\nPress any key to terminate the program.");
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}

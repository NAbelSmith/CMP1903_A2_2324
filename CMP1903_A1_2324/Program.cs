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
            /*
             * Create a Game object and call its methods.
             * Create a Testing object to verify the output and operation of the other classes.
             */

            // Create new Game object
            Game game = new Game();
            Die dieTest = new Die();

            // Create new Testing object
            Testing test = new Testing();

            // Endless loop
            game.StartGame();

            // Stop automatic termination of program
            Console.WriteLine("Press any key to terminate the program.");
            Console.ReadKey();
        }
    }
}

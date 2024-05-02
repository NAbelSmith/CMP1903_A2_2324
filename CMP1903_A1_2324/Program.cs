using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class Program
    {
        static void Main()
        {
            /*
             * Start the game options, giving the player the option to choose their game
             */

            new GameController();

            // Stop automatic termination of program
            Console.WriteLine("\nPress any key to terminate the program.");
            Console.ReadKey();
        }
    }
}

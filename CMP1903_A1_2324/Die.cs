using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class Die
    {
        /*
         * The Die class should contain one property to hold the current die value,
         * and one method that rolls the die, returns and integer and takes no parameters.
         */

        // Set properties (value in this case)
        private int _value;
        private static Random _random = new Random();

        public int value { get { return _value; } }

        // Roll method, rolls the die
        public int Roll()
        {

            // Get new random integer between 1 and 6 and store in value property
            _value = _random.Next(1, 7);

            // Return the new value
            return _value;
        }

    }
}

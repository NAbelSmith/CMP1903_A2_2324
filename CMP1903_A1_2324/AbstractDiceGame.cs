using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal abstract class AbstractDiceGame : Game
    {
        // List of all dice
        protected static List<Die> _diceList;

        public AbstractDiceGame()
        {
            // Instantiate the dice list
            _diceList = new List<Die>();
        }

        protected void RollDie()
        {
            // Roll all the dice in the list
            foreach (var die in _diceList)
            {
                die.Roll();
            }
        }

        public int[] GetDiceValues()
        {
            // Dice List doesn't have any dice in it!
            if (_diceList.Count == 0) return null;

            // Create new array for the values of the dice
            int[] diceVals = new int[_diceList.Count];
            // Copy values over to the array
            for (int i = 0; i < _diceList.Count; i++)
            {
                diceVals[i] = _diceList[i].Value;
            }

            // Return the array of dice values
            return diceVals;
        }
    }
}

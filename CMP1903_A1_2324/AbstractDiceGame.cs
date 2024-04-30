using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal abstract class AbstractDiceGame : AbstractGame
    {
        protected List<Die> _diceList;

        public AbstractDiceGame()
        {
            _diceList = new List<Die>();
        }

        protected void RollDie()
        {
            foreach (var die in _diceList)
            {
                die.Roll();
            }
        }

        public int[] GetDiceValues()
        {
            if (_diceList.Count == 0) return null;
            int[] diceVals = new int[_diceList.Count - 1];
            foreach (Die die in _diceList)
            {
                diceVals.Append(die.Value);
            }

            return diceVals;
        }
    }
}

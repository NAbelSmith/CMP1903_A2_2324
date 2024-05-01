using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class SevensOut : AbstractDiceGame
    {
        private bool _computerMode;
        private int _numOfDice = 2;
        private int[] _gameTotals = {0, 0};
        private int _playerTurn = 0;
        private bool _gameOver = false;

        public SevensOut()
        {
            // Ensure set value for _numOfDie is within allowed range
            if (_numOfDice <= 1)
            {
                Console.WriteLine($"Set number of die ({_numOfDice}) is lower than the minimum number (2). Set number of die to 2.");
                _numOfDice = 2;
            }

            for (int i = 0; i < _numOfDice; i++)
            { 
                _diceList.Add(new Die());
                Console.WriteLine(_diceList[i].Value);
            }

            Console.WriteLine("You are now playing Sevens Out!\nYou will have a variety of options to choose from.\n");
            GameMenu menu = new GameMenu();
            menu.AddOption("Play: Player vs Player Mode", "PlayPlayer");
            menu.AddOption("Play: Player vs Computer Mode", "PlayComputer");
            //menu.AddOption("View: Game Statistics", "ViewStatistics");
            menu.AddOption("Quit: Stop Playing SevensOut", "EndGame");
            menu.DisplayMenu();
            string selectedOption = menu.FetchSelectedOption();
            InvokeMethod(selectedOption);
        }

        private bool RunRound()
        {
            RollDie();
            int[] diceValues = GetDiceValues();
            bool isDouble = CheckEqual(diceValues);
            int diceTotals = 0;
            foreach (int diceValue in diceValues)
            {
                diceTotals += diceValue;
            }
            
            if (diceTotals == 7)
            {
                DisplayRoundResults(diceValues, isDouble, diceTotals);
                EndGame();
                return false;
            }

            if (isDouble)
            {
                diceTotals += diceTotals;
            }

            _gameTotals[_playerTurn] += diceTotals;

            DisplayRoundResults(diceValues, isDouble, diceTotals);

            CyclePlayer();

            return true;
        }

        private void DisplayRoundResults(int[] diceValues, bool isDouble, int diceTotal)
        {
            string diceOutput = "";
            string doubleOutput = "was";
            for (int i = 1; i <= diceValues.Length; i++)
            {
                diceOutput = $"{diceOutput} Dice {i}: {diceValues[i-1]}";
            }
            if (!isDouble)
            {
                doubleOutput = $"{doubleOutput} not";
            }
            if (diceTotal == 7)
            {
                Console.WriteLine($"\nPlayer {_playerTurn + 1} rolled:{diceOutput}. There {doubleOutput} a double! Due to rolling a {diceTotal} the game was ended!");
                return;
            }
            Console.WriteLine($"\nPlayer {_playerTurn + 1} rolled:{diceOutput}. There {doubleOutput} a double! As such gained {diceTotal} points.");
        }

        public override void StartGame()
        {
            bool canContinue = true;
            _gameOver = false;
            while (canContinue)
            {
                if (_gameOver) { break; }
                if (_computerMode && _playerTurn == 1)
                {
                    Console.WriteLine($"\nPlayer {_playerTurn + 1}'s (Computer) Turn.");
                    canContinue = RunRound();
                } else
                {
                    Console.WriteLine($"\nPlayer {_playerTurn + 1}'s Turn. Please select from the following:");
                    PlayerInput();
                }
            }
            
        }

        public override void EndGame()
        {
            _gameOver = true;
            Console.WriteLine($"\nSuccessfully ended the Sevens Out game! The game ended on Player {_playerTurn+1}'s Turn.");

            if (_gameTotals[0] == _gameTotals[1])
            {
                if (_gameTotals[0] == 0)
                {
                    Console.WriteLine($"Game ended before it started... Feel free to start a new game!");
                    return;
                }
                Console.WriteLine($"There was a draw! No individual player won. With a shared score of {_gameTotals[0]}.");
            } else if (_gameTotals[0] > _gameTotals[1])
            {
                Console.WriteLine($"Player 1 won! With a total of {_gameTotals[0]}. Whereas Player 2 had {_gameTotals[1]}.");
            } else
            {
                Console.WriteLine($"Player 2 won! With a total of {_gameTotals[1]}. Whereas Player 1 had {_gameTotals[0]}.");
            }
            //ViewStatistics();
        }

        private void CyclePlayer()
        {
            if (_playerTurn == 0)
            {
                _playerTurn = 1;
            } else
            {
                _playerTurn = 0;
            }
        }

        private void PlayerInput()
        {
            GameMenu playerMenu = new GameMenu();
            playerMenu.AddOption("Roll Dice", "RunRound");
            //playerMenu.AddOption("View: Game Statistics", "ViewStatistics");
            playerMenu.AddOption("Quit: Stop Playing SevensOut", "EndGame");
            playerMenu.DisplayMenu();
            string selectedOption = playerMenu.FetchSelectedOption();
            InvokeMethod(selectedOption);
        }

        private bool CheckEqual(int[] passedArray)
        {
            if (passedArray.Length == 0) return true;

            for (int i = 1; i < passedArray.Length; i++)
            {
                if (passedArray[i] != passedArray[0])
                {
                    return false;
                }
            }

            return true;
        }

        private void PlayPlayer()
        {
            _computerMode = false;
            StartGame();
        }

        private void PlayComputer()
        {
            _computerMode = true;
            StartGame();
        }
    }
}

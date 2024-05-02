using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class ThreeOrMore : AbstractDiceGame
    {
        private int _numOfDice = 5;
        private int _winScore = 20;

        private bool _computerMode;
        private bool _reRoll = false;
        private bool _running = false;
        private bool _reRunLockout = false;
        private bool _testing = false;

        private int _playerTurn = 0;
        private int turns = 0;
        private int[] _gameTotals = { 0, 0 };

        private static readonly Random _random = new Random();
        private Statistics _statistics;

        public int[] GetScores() { return _gameTotals; }
        public int GetWinScore() { return _winScore; }
        public int LastPlayerTurn() { if (_playerTurn == 0) return 1; else return 0; }

        public ThreeOrMore()
        {
            // Ensure set value for _numOfDie is within allowed range
            if (_numOfDice <= 4)
            {
                Console.WriteLine($"Set number of die ({_numOfDice}) is lower than the minimum number (5). Set number of die to 5.");
                _numOfDice = 5;
            }

            for (int i = 0; i < _numOfDice; i++)
            {
                _diceList.Add(new Die());
            }

            _statistics = new Statistics(this);

            Console.WriteLine("You are now playing Three Or More!\nYou will have a variety of options to choose from.\n");
            GameMenu menu = new GameMenu();
            menu.AddOption("Play: Player vs Player Mode", "PlayPlayer");
            menu.AddOption("Play: Player vs Computer Mode", "PlayComputer");
            menu.AddOption("View: Game Statistics", "ViewStatistics");
            menu.AddOption("Quit: Stop Playing Three Or More", "EndGame");
            menu.DisplayMenu();
            string selectedOption = menu.FetchSelectedOption();
            InvokeMethod(selectedOption);
        }

        public ThreeOrMore(bool testing)
        {
            if (!testing) return;
            _testing = true;

            // Ensure set value for _numOfDie is within allowed range
            if (_numOfDice <= 4)
            {
                Console.WriteLine($"Set number of die ({_numOfDice}) is lower than the minimum number (5). Set number of die to 5.");
                _numOfDice = 5;
            }

            for (int i = 0; i < _numOfDice; i++)
            {
                _diceList.Add(new Die());
            }
        }

        public void RunTest()
        {
            RunRound();
        }

        private bool RunRound()
        {
            _running = true;

            RollDie();
            int[] diceValues = GetDiceValues();
            (int, int[]) manyOfAKind = CheckKinds(diceValues);
            int points = 0;

            if (manyOfAKind.Item1 == 2 && !_reRoll && !_reRunLockout)
            {
                if (_computerMode && _playerTurn == 1 || _testing)
                {
                    int computerChoice = _random.Next(1, 3);
                    if (computerChoice == 1)
                    {
                        Console.WriteLine($"Player {_playerTurn + 1} (Computer) has chosen to roll all dice again!");
                        _reRunLockout = true;
                        RunRound();
                    } else
                    {
                        Console.WriteLine($"Player {_playerTurn + 1} (Computer) has chosen to roll all dice other than the 2-of-a-kind!");
                        _reRunLockout = true;
                        RollRemaining();
                    }
                    return true;
                }
                Console.WriteLine($"You rolled a 2-of-a-kind, as such you may choose from the following:");
                GameMenu menu = new GameMenu();
                menu.AddOption("Roll: Roll all dice again", "RunRound");
                menu.AddOption("Roll: Roll all dice other than the 2-of-a-kind", "RollRemaining");
                menu.DisplayMenu();
                string selectedOption = menu.FetchSelectedOption();
                _reRoll = true;
                InvokeMethod(selectedOption);
                return true;
            } 
            else if (manyOfAKind.Item1 == 3) points = 3;
            else if (manyOfAKind.Item1 == 4) points = 6;
            else if (manyOfAKind.Item1 == 5) points = 12;

            _gameTotals[_playerTurn] += points;

            DisplayRoundResults(diceValues, manyOfAKind.Item1, points);

            CyclePlayer();

            return true;
        }

        private void RollRemaining()
        {
            int[] diceValues = GetDiceValues();
            (int, int[]) manyOfAKind = CheckKinds(diceValues);

            RollSpecificDie(manyOfAKind.Item2);

            diceValues = GetDiceValues();
            manyOfAKind = CheckKinds(diceValues);

            int points = 0;

            if (manyOfAKind.Item1 == 3) points = 3;
            else if (manyOfAKind.Item1 == 4) points = 6;
            else if (manyOfAKind.Item1 == 5) points = 12;

            _gameTotals[_playerTurn] += points;

            DisplayRoundResults(diceValues, manyOfAKind.Item1, points);

            CyclePlayer();
        }

        private void RollSpecificDie(int[] diceToRoll)
        {
            for (int i = 0; i < diceToRoll.Length; i++)
            {
                _diceList[diceToRoll[i]].Roll();
            }
        }

        private void DisplayRoundResults(int[] diceValues, int manyOfAKind, int points)
        {
            string diceOutput = "";
            for (int i = 1; i <= diceValues.Length; i++)
            {
                diceOutput = $"{diceOutput} Dice {i}: {diceValues[i - 1]}";
            }
            if (manyOfAKind == 0)
            {
                Console.WriteLine($"\nPlayer {_playerTurn + 1} rolled:{diceOutput}. No duplicates were found! As such gained {points} points.");
                CheckEndGame();
                return;
            }
            Console.WriteLine($"\nPlayer {_playerTurn + 1} rolled:{diceOutput}. A {manyOfAKind}-of-a-kind was found! As such gained {points} points.");
            CheckEndGame();
        }

        public override void StartGame()
        {
            _statistics.statistics["Number of Plays"] = $"{int.Parse(_statistics.statistics["Number of Plays"]) + 1}";
            bool canContinue = true;
            _gameOver = false;
            while (canContinue)
            {
                if (_gameOver) break;
                if (_running) {
                    Thread.Sleep(2000);
                    continue; 
                }
                if (_computerMode && _playerTurn == 1)
                {
                    turns++;
                    Console.WriteLine($"\nPlayer {_playerTurn + 1}'s (Computer) Turn.");
                    canContinue = RunRound();
                }
                else
                {
                    turns++;
                    Console.WriteLine($"\nPlayer {_playerTurn + 1}'s Turn. Please select from the following:");
                    PlayerInput();
                }
            }

        }

        public override void EndGame()
        {
            _gameOver = true;
            Console.WriteLine($"\nSuccessfully ended the Three Or More game! The game ended on Player {_playerTurn + 1}'s Turn.");

            if (_gameTotals[0] == _gameTotals[1])
            {
                if (_gameTotals[0] == 0)
                {
                    Console.WriteLine($"Game ended before it started... Feel free to start a new game!");
                    return;
                }
                Console.WriteLine($"There was a draw! No individual player won. With a shared score of {_gameTotals[0]}.");
            }
            else if (_gameTotals[0] > _gameTotals[1])
            {
                if (_gameTotals[0] > int.Parse(_statistics.statistics["Top Score"])) _statistics.statistics["Top Score"] = $"{_gameTotals[0]}";
                _statistics.statistics["Player 1 Wins"] = $"{int.Parse(_statistics.statistics["Player 1 Wins"]) + 1}";
                Console.WriteLine($"Player 1 won! With a total of {_gameTotals[0]}. Whereas Player 2 had {_gameTotals[1]}.");
            }
            else
            {
                if (_gameTotals[1] > int.Parse(_statistics.statistics["Top Score"])) _statistics.statistics["Top Score"] = $"{_gameTotals[1]}";
                _statistics.statistics["Player 2 Wins"] = $"{int.Parse(_statistics.statistics["Player 2 Wins"]) + 1}";
                Console.WriteLine($"Player 2 won! With a total of {_gameTotals[1]}. Whereas Player 1 had {_gameTotals[0]}.");
            }
            if (turns > int.Parse(_statistics.statistics["Fastest Win"])) _statistics.statistics["Fastest Win"] = $"{turns}";
            ViewStatistics();
            if (!_testing) _statistics.SaveStatistics(this);
        }

        private void CheckEndGame()
        {
            if (_gameTotals[0] >= _winScore || _gameTotals[1] >= _winScore)
            {
                EndGame();
            } else
            {
                Console.WriteLine($"Scores are, Player 1: {_gameTotals[0]} | Player 2: {_gameTotals[1]}");
            }
        }

        private void CyclePlayer()
        {
            if (_playerTurn == 0)
            {
                _playerTurn = 1;
            }
            else
            {
                _playerTurn = 0;
            }
            _running = false;
            _reRunLockout = false;
            _reRoll = false;
        }

        private void PlayerInput()
        {
            GameMenu playerMenu = new GameMenu();
            playerMenu.AddOption("Roll Dice", "RunRound");
            playerMenu.AddOption("View: Game Statistics", "ViewStatistics");
            playerMenu.AddOption("Quit: Stop Playing Three Or More", "EndGame");
            playerMenu.DisplayMenu();
            string selectedOption = playerMenu.FetchSelectedOption();
            InvokeMethod(selectedOption);
        }

        private void ViewStatistics()
        {
            _statistics.ShowStatistics();
        }

        private (int, int[]) CheckKinds(int[] passedArray)
        {
            if (passedArray.Length == 0) return (0, new int[1]);

            int[] lengths = new int[passedArray.Length];
            int[] kindsFound = new int[passedArray.Length];

            for (int i = 0; i < passedArray.Length; i++)
            {
                var comparedValues = from number in passedArray
                                     where number == passedArray[i]
                                     select i;


                if (comparedValues.Count() < 2) lengths[i] = 0;
                else
                {
                    lengths[i] = comparedValues.Count();
                    kindsFound = comparedValues.ToArray();
                }

            }

            int maxValue = lengths[0];

            for (int i = 1; i < lengths.Length; i++)
            {
                if (lengths[i] > maxValue) maxValue = lengths[i];
            }

            return (maxValue, kindsFound);
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

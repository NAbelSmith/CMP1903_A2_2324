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
        // Game Settings
        private int _numOfDice = 5;
        private int _winScore = 20;

        // Access parameters
        private bool _computerMode;
        private bool _reRoll = false;
        private bool _running = false;
        private bool _reRunLockout = false;
        private bool _testing = false;

        // Game local stats
        private int _playerTurn = 0;
        private int turns = 0;
        private int[] _gameTotals = { 0, 0 };

        private static readonly Random _random = new Random();
        private Statistics _statistics;

        // Public methods of accessing certain private params
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

            // Create an access point for statistics
            _statistics = new Statistics(this);

            // Display the menu and take user option
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
            // Check if testing is being ran!
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
            // Run a round seperately to test
            RunRound();
        }

        private bool RunRound()
        {
            _running = true;
            // Roll all dice and retreive values
            RollDie();
            int[] diceValues = GetDiceValues();
            // Check if a #-of-a-kind is found
            (int, int[]) manyOfAKind = CheckKinds(diceValues);
            int points = 0;

            // If a 2 of a kind is found
            if (manyOfAKind.Item1 == 2 && !_reRoll && !_reRunLockout)
            {
                // If a computer is the player, or testing is occuring
                if (_computerMode && _playerTurn == 1 || _testing)
                {
                    // Generate a random number to determine the choice
                    int computerChoice = _random.Next(1, 3);
                    if (computerChoice == 1)
                    {
                        // Re roll all dice
                        Console.WriteLine($"Player {_playerTurn + 1} (Computer) has chosen to roll all dice again!");
                        _reRunLockout = true;
                        RunRound();
                    } else
                    {
                        // Roll remaining dice
                        Console.WriteLine($"Player {_playerTurn + 1} (Computer) has chosen to roll all dice other than the 2-of-a-kind!");
                        _reRunLockout = true;
                        RollRemaining();
                    }
                    return true;
                }
                // Give the player the option to choose to re roll all or the remaining dice
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
            // Assign points as required
            else if (manyOfAKind.Item1 == 3) points = 3;
            else if (manyOfAKind.Item1 == 4) points = 6;
            else if (manyOfAKind.Item1 == 5) points = 12;

            // Add points
            _gameTotals[_playerTurn] += points;

            // Display results to the player
            DisplayRoundResults(diceValues, manyOfAKind.Item1, points);

            // Cycle player turn
            CyclePlayer();

            return true;
        }

        private void RollRemaining()
        {
            // Fetch all the dice values
            int[] diceValues = GetDiceValues();
            (int, int[]) manyOfAKind = CheckKinds(diceValues);

            // Roll the dice that are not 2-of-a-kind
            RollSpecificDie(manyOfAKind.Item2);

            // Fetch new values
            diceValues = GetDiceValues();
            manyOfAKind = CheckKinds(diceValues);

            // Update points as necessary
            int points = 0;

            if (manyOfAKind.Item1 == 3) points = 3;
            else if (manyOfAKind.Item1 == 4) points = 6;
            else if (manyOfAKind.Item1 == 5) points = 12;

            // Add points
            _gameTotals[_playerTurn] += points;

            // Display results to the player
            DisplayRoundResults(diceValues, manyOfAKind.Item1, points);

            // Cycle player turn
            CyclePlayer();
        }

        private void RollSpecificDie(int[] diceToRoll)
        {
            // Roll only the required dice as specified
            for (int i = 0; i < diceToRoll.Length; i++)
            {
                _diceList[diceToRoll[i]].Roll();
            }
        }

        private void DisplayRoundResults(int[] diceValues, int manyOfAKind, int points)
        {
            // Display all dice and their values
            string diceOutput = "";
            for (int i = 1; i <= diceValues.Length; i++)
            {
                diceOutput = $"{diceOutput} Dice {i}: {diceValues[i - 1]}";
            }
            // Output results
            if (manyOfAKind == 0)
            {
                Console.WriteLine($"\nPlayer {_playerTurn + 1} rolled:{diceOutput}. No duplicates were found! As such gained {points} points.");
                CheckEndGame();
                return;
            }
            Console.WriteLine($"\nPlayer {_playerTurn + 1} rolled:{diceOutput}. A {manyOfAKind}-of-a-kind was found! As such gained {points} points.");

            // Check if game is over
            CheckEndGame();
        }

        public override void StartGame()
        {
            // Increment the number of plays for statistic purposes
            _statistics.statistics["Number of Plays"] = $"{int.Parse(_statistics.statistics["Number of Plays"]) + 1}";
            bool canContinue = true;
            _gameOver = false;
            while (canContinue)
            {
                if (_gameOver) break; // Game is over
                if (_running) { // Game is already running
                    Thread.Sleep(2000);
                    continue; 
                }
                if (_computerMode && _playerTurn == 1) // Computers turn, no options
                {
                    turns++;
                    Console.WriteLine($"\nPlayer {_playerTurn + 1}'s (Computer) Turn.");
                    canContinue = RunRound();
                }
                else
                { // Players turn, provide options
                    turns++;
                    Console.WriteLine($"\nPlayer {_playerTurn + 1}'s Turn. Please select from the following:");
                    PlayerInput();
                }
            }

        }

        public override void EndGame()
        {
            // End the game
            _gameOver = true;
            Console.WriteLine($"\nSuccessfully ended the Three Or More game! The game ended on Player {_playerTurn + 1}'s Turn.");

            if (_gameTotals[0] == _gameTotals[1])
            {
                // Game exited during first menu
                if (_gameTotals[0] == 0)
                {
                    Console.WriteLine($"Game ended before it started... Feel free to start a new game!");
                    return;
                }
                // No points awarded during game
                Console.WriteLine($"There was a draw! No individual player won. With a shared score of {_gameTotals[0]}.");
            }
            else if (_gameTotals[0] > _gameTotals[1]) // Player 1 Wins
            {
                // Perform stats calculations and update as necessary
                if (_gameTotals[0] > int.Parse(_statistics.statistics["Top Score"])) _statistics.statistics["Top Score"] = $"{_gameTotals[0]}";
                _statistics.statistics["Player 1 Wins"] = $"{int.Parse(_statistics.statistics["Player 1 Wins"]) + 1}";

                // Update visually
                Console.WriteLine($"Player 1 won! With a total of {_gameTotals[0]}. Whereas Player 2 had {_gameTotals[1]}.");
            }
            else // Player 2 Wins
            {
                // Perform stats calculations and update as necessary
                if (_gameTotals[1] > int.Parse(_statistics.statistics["Top Score"])) _statistics.statistics["Top Score"] = $"{_gameTotals[1]}";
                _statistics.statistics["Player 2 Wins"] = $"{int.Parse(_statistics.statistics["Player 2 Wins"]) + 1}";

                // Update visually
                Console.WriteLine($"Player 2 won! With a total of {_gameTotals[1]}. Whereas Player 1 had {_gameTotals[0]}.");
            }
            // Update fastest win condition of stats
            if (turns > int.Parse(_statistics.statistics["Fastest Win"])) _statistics.statistics["Fastest Win"] = $"{turns}";

            // Display statistics
            ViewStatistics();

            // Save statistics if not testing
            if (!_testing) _statistics.SaveStatistics(this);
        }

        private void CheckEndGame()
        {
            // Check if the game meets the end conditions
            if (_gameTotals[0] >= _winScore || _gameTotals[1] >= _winScore)
            {
                // End the game as required
                EndGame();
            } else
            {
                // Output scores as game is still going
                Console.WriteLine($"Scores are, Player 1: {_gameTotals[0]} | Player 2: {_gameTotals[1]}");
            }
        }

        private void CyclePlayer()
        {
            // Cycle player turn
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
            // Get the players input for selection
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
            // Display statistics
            _statistics.ShowStatistics();
        }

        private (int, int[]) CheckKinds(int[] passedArray)
        {
            // Check if values were passed
            if (passedArray.Length == 0) return (0, new int[1]);

            // Create new int arrays to work with
            int[] lengths = new int[passedArray.Length];
            int[] kindsFound = new int[passedArray.Length];

            for (int i = 0; i < passedArray.Length; i++)
            {
                // LINQ query to fetch all numbers that match that of the current index dice value
                var comparedValues = from number in passedArray
                                     where number == passedArray[i]
                                     select i;

                // All unique so length is 0
                if (comparedValues.Count() < 2) lengths[i] = 0;
                else
                {
                    // Duplicates found, log such
                    lengths[i] = comparedValues.Count();
                    kindsFound = comparedValues.ToArray();
                }

            }

            // Determine the highest #-of-a-kind
            int maxValue = lengths[0];

            for (int i = 1; i < lengths.Length; i++)
            {
                // Update if value is greater
                if (lengths[i] > maxValue) maxValue = lengths[i];
            }

            // Return max value and the #-of-a-kinds found
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

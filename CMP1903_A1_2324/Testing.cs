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
         * This class should test the ThreeOrMore and SevensOut classes.
         * Create a ThreeOrMore object, call the methods and compare their output to expected output.
         * Create a SevensOut object, call the methods and compare their output to expected output.
         * Use debug.assert() to make the comparisons and tests.
         */

        //Method
        public void RunTest()
        {
            // Log Creator
            Statistics logger = new Statistics(this);

            // Console seperator
            logger.LogTest("[Testing] Running a test!");
            Console.WriteLine("\n[Testing] Running a test!");

            // Instantiates a new SevensOut game
            SevensOut sevensOut = new SevensOut(true);

            // Do a testing game of SevensOut
            int[] oldScore = { 0, 0 };
            while (!sevensOut.IsGameOver())
            {
                // Run the test game
                sevensOut.RunTest();
                // Get values to compare
                int[] diceValues = sevensOut.GetDiceValues();
                int diceTotal = 0;

                // Add all the dice
                foreach (int diceValue in diceValues)
                {
                    diceTotal += diceValue;
                }
                
                // Check if a double was rolled
                var doubleChecking = from diceValue in diceValues
                                 where diceValue == diceValues[0]
                                 select diceValue;
                oldScore[sevensOut.LastPlayerTurn()] += diceTotal;

                // Double was rolled, hence double results
                if (doubleChecking.Count() > 1)
                {
                    oldScore[sevensOut.LastPlayerTurn()] += diceTotal;
                }
                logger.LogTest($"[Testing] A score of {diceTotal} was found.");
                Console.WriteLine($"[Testing] A score of {diceTotal} was found.");

                // Testing dice added to 7, check if real game caught it
                if (diceTotal == 7)
                {
                    // Output error if game did not end when expected
                    oldScore[sevensOut.LastPlayerTurn()] -= 7;
                    logger.LogTest($"[Testing] 7 rolled. Game status concluded: {sevensOut.IsGameOver()}");
                    Debug.Assert(sevensOut.IsGameOver(), $"Game conclusion error! A 7 was rolled, but the game did not conclude!");
                }
                // Output error if scores are misaligned
                logger.LogTest($"[Testing] Player {sevensOut.LastPlayerTurn() + 1} calculated score is: {oldScore[sevensOut.LastPlayerTurn()]}. Actual score: {sevensOut.GetScores()[sevensOut.LastPlayerTurn()]}.");
                Debug.Assert(oldScore[sevensOut.LastPlayerTurn()] == sevensOut.GetScores()[sevensOut.LastPlayerTurn()], $"Score error! Score calculated was {oldScore[sevensOut.LastPlayerTurn()]} whearas the score found was {sevensOut.GetScores()[sevensOut.LastPlayerTurn()]}");
            }
            // Output error if scores are misaligned
            logger.LogTest($"[Testing] Player 1 calculated score: {oldScore[0]}. Actual score: {sevensOut.GetScores()[0]}.");
            logger.LogTest($"[Testing] Player 2 calculated score: {oldScore[1]}. Actual score: {sevensOut.GetScores()[1]}.");
            Debug.Assert(oldScore[0] == sevensOut.GetScores()[0], $"Score error! Final Player 1 score calculated was {oldScore[0]} whearas the score found was {sevensOut.GetScores()[0]}");
            Debug.Assert(oldScore[1] == sevensOut.GetScores()[1], $"Score error! Final Player 2 score calculated was {oldScore[1]} whearas the score found was {sevensOut.GetScores()[1]}");

            logger.LogTest($"[Testing] Testing for SevensOut was completed successfully! Moving onto ThreeOrMore testing!");
            Console.WriteLine("[Testing] Testing for SevensOut was completed successfully!\n[Testing] Moving onto ThreeOrMore testing!\n\n");

            // Instatiates a new ThreeOrMore game
            ThreeOrMore threeOrMore = new ThreeOrMore(true);

            // Do a testing game of ThreeOrMore
            oldScore[0] = 0;
            oldScore[1] = 0;
            while (!threeOrMore.IsGameOver())
            {
                // Run the test game
                threeOrMore.RunTest();
                // Get values to compare
                int[] diceValues = threeOrMore.GetDiceValues();
                int desiredScore = 0;

                // Calculate scores
                for (int i = 0; i < diceValues.Length; i++)
                {
                    // Log Dice Values
                    logger.LogTest($"[Testing] Dice {i+1} value: {diceValues[i]}");

                    // Check if a #-of-a-kind exists
                    var manyOfAKindCheck = from value in diceValues
                                         where value == diceValues[i]
                                         select value;

                    // If a #-of-a-kind exists, give appropriate score
                    if (manyOfAKindCheck.Count() == 3) desiredScore = 3;
                    if (manyOfAKindCheck.Count() == 4) desiredScore = 6;
                    if (manyOfAKindCheck.Count() == 5) desiredScore = 12;
                }
                oldScore[threeOrMore.LastPlayerTurn()] += desiredScore;

                logger.LogTest($"[Testing] A score of {desiredScore} was found.");
                Console.WriteLine($"[Testing] A score of {desiredScore} was found.");

                // A total testing score of >= 20 was found, check if real game caught it
                if (desiredScore >= 20)
                {
                    // Output error if game did not end when expected
                    logger.LogTest($"[Testing] Score of {desiredScore} found, detected >= 20. Game status concluded: {threeOrMore.IsGameOver()}");
                    Debug.Assert(threeOrMore.IsGameOver(), $"[Testing] Game conclusion error! A score of 20 or more was achieved, but the game did not conclude!");
                }
                // Output error if scores are misaligned
                logger.LogTest($"[Testing] Player {threeOrMore.LastPlayerTurn() + 1} calculated score is: {oldScore[threeOrMore.LastPlayerTurn()]}. Actual score: {threeOrMore.GetScores()[threeOrMore.LastPlayerTurn()]}.");
                Debug.Assert(oldScore[threeOrMore.LastPlayerTurn()] == threeOrMore.GetScores()[threeOrMore.LastPlayerTurn()], $"Score error! Score calculated was {oldScore[threeOrMore.LastPlayerTurn()]} whearas the score found was {threeOrMore.GetScores()[threeOrMore.LastPlayerTurn()]}");
            }
            // Output error if scores are misaligned
            logger.LogTest($"[Testing] Player 1 calculated score: {oldScore[0]}. Actual score: {threeOrMore.GetScores()[0]}.");
            logger.LogTest($"[Testing] Player 2 calculated score: {oldScore[1]}. Actual score: {threeOrMore.GetScores()[1]}.");
            Debug.Assert(oldScore[0] == threeOrMore.GetScores()[0], $"Score error! Final Player 1 score calculated was {oldScore[0]} whearas the score found was {threeOrMore.GetScores()[0]}");
            Debug.Assert(oldScore[1] == threeOrMore.GetScores()[1], $"Score error! Final Player 2 score calculated was {oldScore[1]} whearas the score found was {threeOrMore.GetScores()[1]}");

            // Testing finished!
            logger.LogTest($"[Testing] Test complete!");
            Console.WriteLine("\n[Testing] Test complete!");
        }
    }
}

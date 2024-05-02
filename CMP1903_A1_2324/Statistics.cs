using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMP1903_A1_2324
{
    internal class Statistics
    {
        private string _targetFile = @"";
        public Dictionary<string, string> statistics = new Dictionary<string, string>();
        // @"..\..\SavedFiles\TestingLogs.txt"
        // @"..\..\SavedFiles\SevensOutStatistics.txt"
        // @"..\..\SavedFiles\ThreeOrMoreStatistics.txt"
        public Statistics(SevensOut sevensOutGame) 
        {
            _targetFile = @"..\..\SavedFiles\SevensOutStatistics.txt";
            // Update statistics variable and convert recovered statistics to a dictionary for ease of access
            string[] recoveredContents = ToArray(_targetFile);
            foreach (string value in recoveredContents)
            {
                string[] splitValue = value.Split(':');
                statistics.Add(splitValue[0], splitValue[1]);
            }
        }

        public Statistics(ThreeOrMore threeOrMore)
        {
            _targetFile = @"..\..\SavedFiles\ThreeOrMoreStatistics.txt";
            // Update statistics variable and convert recovered statistics to a dictionary for ease of access
            string[] recoveredContents = ToArray(_targetFile);
            foreach(string value in recoveredContents)
            {
                string[] splitValue = value.Split(':');
                statistics.Add(splitValue[0], splitValue[1]);
            }
        }

        public Statistics(Testing testing)
        {
            _targetFile = @"..\..\SavedFiles\TestingLogs.txt";
        }

        public string[] ToArray(string filePath)
        {
            // Convert a file to an array
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Get file contents
                    string fileContents = sr.ReadToEnd();
                    // Split file by new lines (as is formated as such)
                    string[] fileContentsArrayString = fileContents.Split('\n');
                    // Return the converted file as an array
                    return fileContentsArrayString;
                }
            }
            catch (Exception ex)
            {
                // An unknown error occurred
                Console.WriteLine($"An error occurred: {ex}");
            }
            // Return a blank array due to unforseen error
            return new string[0];
        }

        public void SaveStatistics(SevensOut sevensOut)
        {
            try
            {
                // Write the desired message to the file
                File.WriteAllText(_targetFile, $"Top Score:{statistics["Top Score"]}\nNumber of Plays:{statistics["Number of Plays"]}\nScores:{statistics["Scores"]}\nPlayer 1 Wins:{statistics["Player 1 Wins"]}\nPlayer 2 Wins:{statistics["Player 2 Wins"]}");
            }
            catch (Exception ex)
            {
                // An unknown error occurred
                Console.WriteLine($"An error occurred: {ex}");
            }
        }

        public void SaveStatistics(ThreeOrMore threeOrMore)
        {
            try
            {
                // Write the desired message to the file
                File.WriteAllText(_targetFile, $"Top Score:{statistics["Top Score"]}\nNumber of Plays:{statistics["Number of Plays"]}\nPlayer 1 Wins:{statistics["Player 1 Wins"]}\nPlayer 2 Wins:{statistics["Player 2 Wins"]}\nFastest Win:{statistics["Fastest Win"]}");
            }
            catch (Exception ex)
            {
                // An unknown error occurred
                Console.WriteLine($"An error occurred: {ex}");
            }
        }

        public void ShowStatistics()
        {
            // Output all statistics selected
            Console.WriteLine("Current Statistics:");
            foreach (var stat in statistics)
            {
                Console.WriteLine($"{stat.Key}: {stat.Value}");
            }
        }

        public void LogTest(string message)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(_targetFile, true))
                {
                    // Get the current timestamp
                    DateTime currentTime = DateTime.Now;

                    // Format the timestamp as text
                    string timestampText = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

                    // Write the desired message to the file
                    sr.WriteLine($"[{timestampText}] {message}");

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                // An unknown error occurred
                Console.WriteLine($"An error occurred: {ex}");
            }
        }
    }
}

using System.IO;
using System;
using System.Collections.Generic;

namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// Provides utility methods for reading and writing data.
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Reads data from text files in the specified directory and populates the provided list of rounds.
        /// </summary>
        /// <param name="directory">The directory containing the text files.</param>
        /// <param name="Rounds">The list of rounds to populate with data.</param>
        public static void ReadData(string directory, List<Round> Rounds)
        {
            string[] filePaths = Directory.GetFiles(directory, "*.txt");
            foreach (string path in filePaths)
            {
                ReadPlayerData(path, Rounds);
            }
        }

        /// <summary>
        /// Reads player data from the specified file and populates the provided list of rounds.
        /// </summary>
        /// <param name="fileName">The name of the file containing player data.</param>
        /// <param name="Rounds">The list of rounds to populate with data.</param>
        public static void ReadPlayerData(string fileName, List<Round> Rounds)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string number = reader.ReadLine();
                DateTime date = DateTime.Parse(reader.ReadLine());
                Round round = new Round(number, date);
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    switch (values.Length)
                    {
                        case 6:
                            round.AddPlayer(new CSPlayer(line));
                            break;
                        case 8:
                            round.AddPlayer(new LoLPlayer(line));
                            break;
                    }
                }
                Rounds.Add(round);
            }
        }

        /// <summary>
        /// Prints data for all rounds to a file with the specified name.
        /// </summary>
        /// <param name="fileName">The name of the file to print data to.</param>
        /// <param name="Rounds">The list of rounds to print data for.</param>
        public static void PrintDataToFile(string fileName, List<Round> Rounds)
        {
            if (Rounds.Count.Equals(0))
                throw new EmptyException();

            for (int i = 0; i < Rounds.Count; i++)
            {
                PrintRoundToFile(fileName, Rounds[i], "Ratas " + Rounds[i].Number.ToString());
            }
        }

        /// <summary>
        /// Prints data for the specified round to a file with the specified name and title.
        /// </summary>
        /// <param name="fileName">The name of the file to print data to.</param>
        /// <param name="round">The round to print data for.</param>
        /// <param name="title">The title to include in the output.</param>
        public static void PrintRoundToFile(string fileName, Round round, string title)
        {
            if (round.Count().Equals(0))
                throw new EmptyException();

            using (var writer = new StreamWriter(fileName, true))
            {
                string line = new string('-', 115);
                writer.WriteLine(title);
                writer.WriteLine(line);
                for (int i = 0; i < round.Count(); i++)
                {
                    writer.WriteLine(round.GetPlayer(i).ToStringTXT());
                }
                writer.WriteLine(line);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Prints data for all players to a file with the specified name and title.
        /// </summary>
        /// <param name="fileName">The name of the file to print data to.</param>
        /// <param name="Players">The list of players to print data for.</param>
        /// <param name="title">The title to include in the output.</param>
        public static void PrintPlayersToFile(string fileName, List<Player> Players, string title)
        {
            if (Players.Count.Equals(0))
                throw new EmptyException();

            using (var writer = new StreamWriter(fileName, true))
            {
                string line = new string('-', 115);
                writer.WriteLine(title);
                writer.WriteLine(line);
                for (int i = 0; i < Players.Count; i++)
                {
                    writer.WriteLine(Players[i].ToStringTXT());
                }
                writer.WriteLine(line);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Prints data for all teams to a file with the specified name and title.
        /// </summary>
        /// <param name="fileName">The name of the file to print data to.</param>
        /// <param name="Teams">The list of teams to print data for.</param>
        /// <param name="title">The title to include in the output.</param>
        public static void PrintTeamsToFile(string fileName, List<string> Teams, string title)
        {
            if (Teams.Count.Equals(0))
                throw new EmptyException();

            using (var writer = new StreamWriter(fileName, true))
            {
                string line = new string('-', 32);
                writer.WriteLine(title);
                writer.WriteLine(line);
                for (int i = 0; i < Teams.Count; i++)
                {
                    writer.WriteLine(string.Format($"| {i+1, 5} | {Teams[i], -20} |"));
                }
                writer.WriteLine(line);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Prints data for a new team to a file with the specified name and title.
        /// </summary>
        /// <typeparam name="T">The type of player in the new team.</typeparam>
        /// <param name="fileName">The name of the file to print data to.</param>
        /// <param name="NewTeam">The list of players in the new team to print data for.</param>
        /// <param name="title">The title to include in the output.</param>
        public static void PrintNewTeamToFile<T>(string fileName, List<T> NewTeam, string title) where T : Player
        {
            if (NewTeam.Count.Equals(0))
                throw new EmptyException();

            using (var writer = new StreamWriter(fileName, true))
            {
                string line = new string('-', 115);
                writer.WriteLine(title);
                writer.WriteLine(line);
                for (int i = 0; i < NewTeam.Count; i++)
                {
                    writer.WriteLine(NewTeam[i].ToStringTXT());
                }
                writer.WriteLine(line);
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Prints the list of players to a CSV file with the given filename.
        /// </summary>
        /// <param name="fileName">The filename of the CSV file to be created.</param>
        /// <param name="Players">The list of players to be printed to the CSV file.</param>
        /// <exception cref="EmptyException">Thrown if the list of players is empty.</exception>
        public static void PrintPlayersToFileCSV(string fileName, List<Player> Players)
        {
            if (Players.Count.Equals(0))
                throw new EmptyException();

            using (var writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    writer.WriteLine(Players[i].ToStringCSV());
                }
            }
        }

        /// <summary>
        /// Prints the list of teams to a CSV file with the given filename.
        /// </summary>
        /// <param name="fileName">The filename of the CSV file to be created.</param>
        /// <param name="Teams">The list of teams to be printed to the CSV file.</param>
        /// <exception cref="EmptyException">Thrown if the list of teams is empty.</exception>
        public static void PrintTeamsToFileCSV(string fileName, List<string> Teams)
        {
            if (Teams.Count.Equals(0))
                throw new EmptyException();

            using (var writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < Teams.Count; i++)
                {
                    writer.WriteLine(Teams[i]);
                }
            }
        }

        /// <summary>
        /// Prints the list of new teams with the given title to a CSV file with the given filename.
        /// </summary>
        /// <typeparam name="T">The type of the new team, which must be a subclass of Player.</typeparam>
        /// <param name="fileName">The filename of the CSV file to be created or appended to.</param>
        /// <param name="NewTeam">The list of new teams to be printed to the CSV file.</param>
        /// <param name="title">The title of the new team to be printed to the CSV file.</param>
        /// <exception cref="EmptyException">Thrown if the list of new teams is empty.</exception>
        public static void PrintNewTeamToFileCSV<T>(string fileName, List<T> NewTeam, string title) where T : Player
        {
            if (NewTeam.Count.Equals(0))
                throw new EmptyException();

            using (var writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(title);
                for (int i = 0; i < NewTeam.Count; i++)
                {
                    writer.WriteLine(NewTeam[i].ToStringCSV());
                }
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Prints an "empty" message to a file with the given filename.
        /// </summary>
        /// <param name="fileName">The filename of the file to be created or appended to.</param>
        public static void PrintEmpty(string fileName)
        {
            using (var writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine("DUOMENŲ NĖRA");
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Prints an "empty" message to a CSV file with the given filename.
        /// </summary>
        /// <param name="fileName">The filename of the CSV file to be created.</param>
        public static void PrintEmptyCSV(string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("DUOMENŲ NĖRA");
                writer.WriteLine();
            }
        }
    }
}
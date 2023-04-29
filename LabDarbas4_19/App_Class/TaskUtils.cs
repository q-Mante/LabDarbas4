using System;
using System.Collections.Generic;

namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// A collection of utility methods for tasks related to games and players.
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Returns the best player of type T from a list of rounds.
        /// </summary>
        /// <typeparam name="T">The type of player to return.</typeparam>
        /// <param name="Rounds">The list of rounds to consider.</param>
        /// <returns>The best player of type T.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when an index is out of range.</exception>
        public static T BestPlayer<T>(List<Round> Rounds) where T : Player, new()
        {
            T best = new T();
            if (Rounds.Count != 0)
            {
                Round Merged = Merge(Rounds);
                for (int i = 0; i < Merged.Count(); i++)
                {
                    Player player = Merged.GetPlayer(i);
                    if (player.Same(best))
                    {
                        if (player.CompareTo(best) < 0)
                            best = (T)player;
                    }
                }
            }
            return best;
        }

        /// <summary>
        /// Merges a list of rounds into a single round.
        /// </summary>
        /// <param name="Rounds">The list of rounds to merge.</param>
        /// <returns>The merged round.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when an index is out of range.</exception>
        private static Round Merge(List<Round> Rounds)
        {
            return Recursion(Rounds, 0);
        }

        /// <summary>
        /// Recursively merges a list of rounds into a single round.
        /// </summary>
        /// <param name="Rounds">The list of rounds to merge.</param>
        /// <param name="index">The current index in the list of rounds.</param>
        /// <returns>The merged round.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when an index is out of range.</exception>
        private static Round Recursion(List<Round> Rounds, int index)
        {
            if (index < 0 || index >= Rounds.Count)
                throw new IndexOutOfRangeException();

            if (index < Rounds.Count - 1)
                return Rounds[index] + Recursion(Rounds, ++index);
            return Rounds[index];
        }

        /// <summary>
        /// Returns a list of all the teams represented in a list of rounds.
        /// </summary>
        /// <param name="Rounds">The list of rounds to consider.</param>
        /// <returns>A list of all the teams represented in the rounds.</returns>
        public static List<string> GetTeams(List<Round> Rounds)
        {
            List<string> teams = new List<string>();
            foreach (Round round in Rounds)
            {
                for (int i = 0; i < round.Count(); i++)
                {
                    Player player = round.GetPlayer(i);
                    if (!teams.Contains(player.Team))
                        teams.Add(player.Team);
                }
            }
            return teams;
        }

        /// <summary>
        /// Returns a list of players of type T that pass a given criteria from a list of rounds.
        /// </summary>
        /// <typeparam name="T">The type of player to return.</typeparam>
        /// <typeparam name="ICriteria">The criteria to use when selecting players.</typeparam>
        /// <param name="Rounds">The list of rounds to consider.</param>
        /// <param name="criteria">The criteria to use when selecting players.</param>
        /// <returns>A list of players of type T that pass the criteria.</returns>
        public static List<T> NewTeam<T, ICriteria>(List<Round> Rounds, ICriteria criteria) where T : Player, IFilter<ICriteria>, new()
        {
            List<T> newTeam = new List<T>();
            foreach (Round round in Rounds)
            {
                for (int i = 0; i < round.Count(); i++)
                {
                    Player player = round.GetPlayer(i);
                    if (player is T selected)
                        if (!newTeam.Contains(selected))
                            if (selected.Pass(criteria))
                                newTeam.Add(selected);
                }
            }
            return newTeam;
        }

        /// <summary>
        /// Returns a list of all the players represented in a list of rounds.
        /// </summary>
        /// <param name="Rounds">The list of rounds to consider.</param>
        /// <returns>A list of all the players represented in the rounds.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when an index is out of range.</exception>
        public static List<Player> GetPlayers(List<Round> Rounds)
        {
            List<Player> players = new List<Player>();
            if (Rounds.Count != 0)
            {
                Round Merged = Merge(Rounds);
                for (int i = 0; i < Merged.Count(); i++)
                {
                    Player player = Merged.GetPlayer(i);
                    if (!players.Contains(player))
                        players.Add(player);
                }
            }
            return players;
        }
    }
}
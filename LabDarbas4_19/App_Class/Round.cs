using System;
using System.Collections.Generic;

namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// Represents a round of a game.
    /// </summary>
    public class Round
    {
        /// <summary>
        /// Gets the number of the round.
        /// </summary>
        public string Number { get; private set; }

        /// <summary>
        /// Gets the date of the round.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets or sets the list of players in the round.
        /// </summary>

        private List<Player> Players;

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class with a number and date.
        /// </summary>
        /// <param name="number">The number of the round.</param>
        /// <param name="date">The date of the round.</param>
        public Round(string number, DateTime date)
        {
            Number = number;
            Date = date;
            Players = new List<Player>();
        }

        /// <summary>
        /// Gets the number of players in the round.
        /// </summary>
        /// <returns>The number of players in the round.</returns>
        public int Count() => Players.Count;

        /// <summary>
        /// Adds a player to the round.
        /// </summary>
        /// <param name="player">The player to add.</param>
        public void AddPlayer(Player player) => Players.Add(player);

        /// <summary>
        /// Gets the player at the specified index.
        /// </summary>
        /// <param name="index">The index of the player to get.</param>
        /// <returns>The player at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is less than zero or greater than or equal to the number of players in the round.</exception>
        public Player GetPlayer(int index)
        {
            if (index < 0 || index >= Players.Count)
                throw new IndexOutOfRangeException();

            return Players[index];
        }

        /// <summary>
        /// Removes a player from the round.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>true if the player was removed successfully; otherwise, false.</returns>
        public bool RemovePlayer(Player player) => Players.Remove(player);

        /// <summary>
        /// Sorts the list of players in the round.
        /// </summary>
        public void SortPlayers() => Players.Sort();

        /// <summary>
        /// Combines two rounds by adding the kills, deaths, and assists of players with the same name.
        /// </summary>
        /// <param name="lhs">The first round to combine.</param>
        /// <param name="rhs">The second round to combine.</param>
        /// <returns>A new <see cref="Round"/> object that is the result of combining the two rounds.</returns>
        public static Round operator +(Round lhs, Round rhs)
        {
            Round result = new Round(string.Format($"{lhs.Number} + {rhs.Number}"), rhs.Date);
            for (int i = 0; i < lhs.Count(); i++)
            {
                Player first = lhs.GetPlayer(i).Copy();
                for (int j = 0; j < rhs.Count(); j++)
                {
                    if (first.Same(rhs.GetPlayer(j)))
                    {
                        Player second = rhs.GetPlayer(j);
                        first.Merge(second);
                        result.AddPlayer(first);
                        break;
                    }
                }
            }
            return result;
        }
    }
}
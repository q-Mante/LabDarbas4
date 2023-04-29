using System.Text.RegularExpressions;

namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// Represents a player in the game League of Legends.
    /// </summary>
    public sealed class LoLPlayer : Player
    {
        /// <summary>
        /// Gets the position of the player.
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        /// Gets the champion played by the player.
        /// </summary>
        public string Champion { get; private set; }

        /// <summary>
        /// Gets the number of kills made by the player.
        /// </summary>
        public int Kills { get; private set; }

        /// <summary>
        /// Gets the number of deaths made by the player.
        /// </summary>
        public int Deaths { get; private set; }

        /// <summary>
        /// Gets the number of assists made by the player.
        /// </summary>
        public int Assists { get; private set; }

        /// <summary>
        /// Gets the KDA (Kill-Death-Assist) ratio of the player.
        /// </summary>
        private double KDA
        {
            get
            {
                if (Deaths.Equals(0))
                    return Kills + Assists;
                return (double)(Kills + Assists) / Deaths;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoLPlayer"/> class.
        /// </summary>
        public LoLPlayer()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoLPlayer"/> class with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="surname">The surname of the player.</param>
        /// <param name="team">The team of the player.</param>
        /// <param name="position">The position of the player.</param>
        /// <param name="champion">The champion played by the player.</param>
        /// <param name="kills">The number of kills made by the player.</param>
        /// <param name="deaths">The number of deaths made by the player.</param>
        /// <param name="assists">The number of assists made by the player.</param>
        public LoLPlayer(string name, string surname, string team, string position, string champion, int kills, int deaths, int assists)
            : base(name, surname, team)
        {
            Position = position;
            Champion = champion;
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoLPlayer"/> class with the specified data string.
        /// </summary>
        /// <param name="data">The data string containing the player information.</param>
        public LoLPlayer(string data)
            : base(data)
        {
            SetData(data);
        }

        /// <summary>
        /// Sets the player data from the specified data string.
        /// </summary>
        /// <param name="line">The data string containing the player information.</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = Regex.Split(line, ";");
            Position = values[3];
            Champion = values[4];
            Kills = int.Parse(values[5]);
            Deaths = int.Parse(values[6]);
            Assists = int.Parse(values[7]);
        }

        /// <summary>
        /// Creates a copy of the current <see cref="LoLPlayer"/> object.
        /// </summary>
        /// <returns>A new <see cref="LoLPlayer"/> object that is a copy of the current instance.</returns>
        public override Player Copy()
        {
            return new LoLPlayer(Name, Surname, Team, Position, Champion, Kills, Deaths, Assists);
        }

        /// <summary>
        /// Compares the current <see cref="LoLPlayer"/> object with another <see cref="Player"/> object.
        /// </summary>
        /// <param name="other">The <see cref="Player"/> object to compare with.</param>
        /// <returns>A value indicating the relative order of the objects being compared.</returns>
        public override int CompareTo(Player other)
        {
            if (other is LoLPlayer selected)
            {
                return -1 * KDA.CompareTo(selected.KDA);
            }

            return -1;
        }

        /// <summary>
        /// Determines if this LoLPlayer is the same as another Player instance
        /// </summary>
        /// <param name="other">The Player to compare against</param>
        /// <returns>True if both players are LoLPlayers, false otherwise</returns>
        public override bool Same(Player other)
        {
            return GetType().Equals(other.GetType());
        }

        /// <summary>
        /// Merges the data of another Player instance into this LoLPlayer instance
        /// </summary>
        /// <param name="other">The Player instance to merge data from</param>
        public override void Merge(Player other)
        {
            if (other is LoLPlayer player)
            {
                Kills += player.Kills;
                Deaths += player.Deaths;
                Assists += player.Assists;
            }
        }

        /// <summary>
        /// Determines if this LoLPlayer passes a certain criteria based on their Kills statistic
        /// </summary>
        /// <param name="criteria">The criteria to compare Kills against</param>
        /// <returns>True if the LoLPlayer's Kills is greater than the criteria, false otherwise</returns>
        public override bool Pass(int criteria)
        {
            return Kills > criteria;
        }

        /// <summary>
        /// Generates the header row for a text representation of a LoLPlayer
        /// </summary>
        /// <returns>A formatted string representing the header row</returns>
        public override string Header()
        {
            return string.Format("{0}\tPozicija\tČempionas\tNužudymai\tMirtys\tPadėjimai", base.Header());
        }

        /// <summary>
        /// Generates a string representation of a LoLPlayer for a text file
        /// </summary>
        /// <returns>A formatted string representing the LoLPlayer's data</returns>
        public override string ToStringTXT()
        {
            return string.Format($"{base.ToStringTXT()} {Position,-10} | {Champion,-15} | {Kills,5} | {Deaths,5} | {Assists,5} |");
        }

        /// <summary>
        /// Generates a string representation of a LoLPlayer for a CSV file
        /// </summary>
        /// <returns>A formatted string representing the LoLPlayer's data</returns>
        public override string ToStringCSV()
        {
            return string.Format($"{base.ToStringCSV()};{Position};{Champion};{Kills};{Deaths};{Assists}");
        }

        /// <summary>
        /// Generates a string representation of a LoLPlayer for debugging purposes
        /// </summary>
        /// <returns>A formatted string representing the LoLPlayer's data</returns>
        public override string ToString()
        {
            return string.Format($"{base.ToString()}\t{Position}\t{Champion}\t{Kills}\t{Deaths}\t{Assists}");
        }
    }
}
using System.Text.RegularExpressions;

namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// A class representing a Counter-Strike player that inherits from the <see cref="Player"/> class.
    /// </summary>
    public sealed class CSPlayer : Player
    {
        /// <summary>
        /// Gets or sets the number of kills.
        /// </summary>
        public int Kills { get; private set; }

        /// <summary>
        /// Gets or sets the number of deaths.
        /// </summary>
        public int Deaths { get; private set; }

        /// <summary>
        /// Gets or sets the favorite weapon.
        /// </summary>
        public string FavoriteWeapon { get; private set; }

        /// <summary>
        /// Gets the kill-to-death ratio.
        /// </summary>
        private double KD
        {
            get
            {
                if (Deaths.Equals(0))
                    return Kills;
                return (double)Kills / Deaths;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSPlayer"/> class with default values.
        /// </summary>
        public CSPlayer()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSPlayer"/> class with the specified values.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="surname">The surname of the player.</param>
        /// <param name="team">The name of the team.</param>
        /// <param name="kills">The number of kills.</param>
        /// <param name="deaths">The number of deaths.</param>
        /// <param name="favoriteWeapon">The favorite weapon.</param>
        public CSPlayer(string name, string surname, string team, int kills, int deaths, string favoriteWeapon)
            : base(name, surname, team)
        {
            Kills = kills;
            Deaths = deaths;
            FavoriteWeapon = favoriteWeapon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSPlayer"/> class with values parsed from the specified string.
        /// </summary>
        /// <param name="data">The string to parse.</param>
        public CSPlayer(string data)
            : base(data)
        {
            SetData(data);
        }

        /// <summary>
        /// Sets the data of the player from the specified string.
        /// </summary>
        /// <param name="line">The string to parse.</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = Regex.Split(line, ";");
            Kills = int.Parse(values[3]);
            Deaths = int.Parse(values[4]);
            FavoriteWeapon = values[5];
        }

        /// <summary>
        /// Returns a copy of the player.
        /// </summary>
        /// <returns>A copy of the player.</returns>
        public override Player Copy()
        {
            return new CSPlayer(Name, Surname, Team, Kills, Deaths, FavoriteWeapon);
        }

        /// <summary>
        /// Compares the player to another player.
        /// </summary>
        /// <param name="other">The player to compare to.</param>
        /// <returns>A value indicating the relative order of the players.</returns>
        public override int CompareTo(Player other)
        {
            if (other is CSPlayer selected)
            {
                return -1 * KD.CompareTo(selected.KD);
            }

            return 1;
        }

        /// <summary>
        /// Checks if the given player has the same type as this instance of Player.
        /// </summary>
        /// <param name="other">The Player object to compare with this instance.</param>
        /// <returns>True if the type of the given object is the same as this instance, false otherwise.</returns>
        public override bool Same(Player other)
        {
            return GetType().Equals(other.GetType());
        }

        /// <summary>
        /// Merges the data of the given Player object with the data of this instance of Player.
        /// </summary>
        /// <param name="other">The Player object to merge with this instance.</param>
        public override void Merge(Player other)
        {
            if (other is CSPlayer player)
            {
                Kills += player.Kills;
                Deaths += player.Deaths;
            }
        }

        /// <summary>
        /// Checks if this instance of Player passes the given criteria based on the number of deaths.
        /// </summary>
        /// <param name="criteria">The maximum number of deaths allowed.</param>
        /// <returns>True if this instance's number of deaths is less than the given criteria, false otherwise.</returns>
        public override bool Pass(int criteria)
        {
            return Deaths < criteria;
        }

        /// <summary>
        /// Gets the header string for displaying the properties of a CSPlayer object.
        /// </summary>
        /// <returns>A formatted string representing the headers for displaying a CSPlayer object's properties.</returns>
        public override string Header()
        {
            return string.Format("{0}\tNužudymai\tMirtys\tGinklas", base.Header());
        }

        /// <summary>
        /// Gets a string representation of the CSPlayer object in TXT format.
        /// </summary>
        /// <returns>A formatted string representing the properties of a CSPlayer object in TXT format.</returns>
        public override string ToStringTXT()
        {
            return string.Format($"{base.ToStringTXT()} {"",-10} | {FavoriteWeapon,-15} | {Kills,5} | {Deaths,5} | {"",5} | ");
        }

        /// <summary>
        /// Gets a string representation of the CSPlayer object in CSV format.
        /// </summary>
        /// <returns>A formatted string representing the properties of a CSPlayer object in CSV format.</returns>
        public override string ToStringCSV()
        {
            return string.Format($"{base.ToStringCSV()};{Kills};{Deaths};{FavoriteWeapon}");
        }

        /// <summary>
        /// Gets a string representation of the CSPlayer object.
        /// </summary>
        /// <returns>A formatted string representing the properties of a CSPlayer object.</returns>
        public override string ToString()
        {
            return string.Format($"{base.ToString()}\t{""}\t{FavoriteWeapon}\t{Kills}\t{Deaths}\t{""}");
        }
    }
}
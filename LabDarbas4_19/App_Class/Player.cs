using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// Abstract base class for player objects.
    /// </summary>
    public abstract class Player : IComparable<Player>, IEquatable<Player>, IMergable<Player>, IFilter<int>, ITableHeader
    {
        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the surname of the player.
        /// </summary>
        public string Surname { get; private set; }

        /// <summary>
        /// Gets the team name of the player.
        /// </summary>
        public string Team { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        public Player() { }

        /// <summary>
        /// Initializes a new instance of the Player class with the given name, surname, and team name.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="surname">The surname of the player.</param>
        /// <param name="team">The team name of the player.</param>
        public Player(string name, string surname, string team)
        {
            Name = name;
            Surname = surname;
            Team = team;
        }

        /// <summary>
        /// Initializes a new instance of the Player class with the data from the given string.
        /// </summary>
        /// <param name="data">The data string containing name, surname, and team name.</param>
        public Player(string data)
        {
            SetData(data);
        }

        /// <summary>
        /// Sets the data of the player from the given string.
        /// </summary>
        /// <param name="line">The data string containing name, surname, and team name.</param>
        public virtual void SetData(string line)
        {
            string[] values = Regex.Split(line, ";");
            Name = values[0];
            Surname = values[1];
            Team = values[2];
        }

        /// <summary>
        /// Creates a copy of the player object.
        /// </summary>
        public abstract Player Copy();

        /// <summary>
        /// Compares the player object to another player object.
        /// </summary>
        /// <param name="other">The other player object to compare to.</param>
        /// <returns>An integer indicating the relative order of the player objects.</returns>
        public abstract int CompareTo(Player other);

        /// <summary>
        /// Checks if the player object is the same as another player object.
        /// </summary>
        /// <param name="other">The other player object to compare to.</param>
        /// <returns>True if the player objects are the same, false otherwise.</returns>
        public abstract bool Same(Player other);

        /// <summary>
        /// Merges the player object with another player object.
        /// </summary>
        /// <param name="other">The other player object to merge with.</param>
        public abstract void Merge(Player other);

        /// <summary>
        /// Checks if the player object passes a filter with the given criteria.
        /// </summary>
        /// <param name="criteria">The criteria for the filter.</param>
        /// <returns>True if the player object passes the filter, false otherwise.</returns>
        public abstract bool Pass(int criteria);

        /// <summary>
        /// Checks if the player object is equal to another player object.
        /// </summary>
        /// <param name="other">The other player object to compare to.</param>
        /// <returns>True if the player objects are equal, false otherwise.</returns>
        public virtual bool Equals(Player other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name) && Surname.Equals(other.Surname) && Team.Equals(other.Team);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            return this.Equals(obj as Player);
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int hashCode = -1130704600;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Team);
            return hashCode;
        }

        /// <summary>
        /// Gets the table header for this player.
        /// </summary>
        /// <returns>The table header for this player.</returns>
        public virtual string Header()
        {
            return string.Format("Vardas\tPavardė\tKomandos pavadinimas");
        }

        /// <summary>
        /// Returns a string representation of the player in TXT format.
        /// </summary>
        /// <returns>A string representation of the player in TXT format.</returns>
        public virtual string ToStringTXT()
        {
            return string.Format($"| {Name,-15} | {Surname,-15} | {Team,-20} |");
        }

        /// <summary>
        /// Returns a string representation of the player in CSV format.
        /// </summary>
        /// <returns>A string representation of the player in CSV format.</returns>
        public virtual string ToStringCSV()
        {
            return string.Format($"{Name};{Surname};{Team}");
        }

        /// <summary>
        /// Returns a string representation of the player in base format.
        /// </summary>
        /// <returns>A string representation of the player in base format.</returns>
        public virtual string ToStringBase()
        {
            return string.Format($"{Name}\t{Surname}\t{Team}");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.ToStringBase();
        }
    }
}
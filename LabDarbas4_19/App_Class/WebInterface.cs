using LabDarbas4_19.App_Class;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabDarbas4_19
{
    /// <summary>
    /// Represents a web interface.
    /// </summary>
    public partial class WebInterface : System.Web.UI.Page
    {
        /// <summary>
        /// Adds an empty table to the specified location.
        /// </summary>
        /// <param name="location">The location where the table will be added.</param>
        protected void AddEmptyTable(string location)
        {
            HtmlGenericControl dataTableContainer = new HtmlGenericControl("div");
            dataTableContainer.Attributes.Add("class", "tournament__data-table-container");
            Table newTable = new Table();
            newTable.CssClass = "tournament__table";
            AddTableHeaderRow(newTable, "NĖRA DUOMENŲ");
            dataTableContainer.Controls.Add(newTable);

            HtmlGenericControl dataContainer = new HtmlGenericControl("div");
            dataContainer.Attributes.Add("class", "tournament__data-container");
            dataContainer.Controls.Add(dataTableContainer);

            FindControl(location).Controls.Add(dataContainer);
        }

        /// <summary>
        /// Adds initial data tables to the specified location.
        /// </summary>
        /// <param name="Rounds">The rounds to display in the tables.</param>
        /// <param name="location">The location where the tables will be added.</param>
        protected void AddInitialDataTables(List<Round> Rounds, string location)
        {
            if (Rounds.Count.Equals(0))
                throw new EmptyException();

            if (FindControl(location) is null)
                throw new ArgumentNullException("location");

            for (int i = 0; i < Rounds.Count; i++)
            {
                HtmlGenericControl dataText = new HtmlGenericControl("div");
                dataText.Attributes.Add("class", "tournament__data-text-container");
                Label text = new Label();
                text.ID = "Label" + i;
                text.CssClass = "tournament__label";
                text.Text = string.Format($"Pradiniai duomenys: Ratas {Rounds[i].Number}, Data {Rounds[i].Date:d}");
                dataText.Controls.Add(text);

                HtmlGenericControl dataTableContainer = new HtmlGenericControl("div");
                dataTableContainer.Attributes.Add("class", "tournament__data-table-container");
                Table newTable = new Table();
                newTable.ID = "Table" + i;
                newTable.CssClass = "tournament__table";
                AddTableHeaderRow(newTable, "Vardas", "Pavardė", "Komandos pavadinimas", "Pozicija", "Čempionas/Ginklas", "Nužudymai", "Mirtys", "Padėjimai");
                for (int j = 0; j < Rounds[i].Count(); j++)
                {
                    string[] values = Rounds[i].GetPlayer(j).ToString().Split('\t');
                    AddTableRow(newTable, values);
                }
                dataTableContainer.Controls.Add(newTable);

                HtmlGenericControl dataContainer = new HtmlGenericControl("div");
                dataContainer.Attributes.Add("class", "tournament__data-container");
                dataContainer.Controls.Add(dataText);
                dataContainer.Controls.Add(dataTableContainer);

                FindControl(location).Controls.Add(dataContainer);
            }
        }

        /// <summary>
        /// Adds the best players to the specified location.
        /// </summary>
        /// <param name="player">The player to display.</param>
        /// <param name="location">The location where the player will be displayed.</param>
        protected void AddBestPlayers(Player player, string location)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player)); 

            HtmlGenericControl dataText = new HtmlGenericControl("div");
            dataText.Attributes.Add("class", "tournament__data-text-container");
            Label text = new Label();
            text.CssClass = "tournament__label";
            text.Text = player.ToStringBase();
            dataText.Controls.Add(text);

            FindControl(location).Controls.Add(dataText);
        }

        /// <summary>
        /// Adds a table of players to the specified location.
        /// </summary>
        /// <param name="Players">List of players to be added to the table.</param>
        /// <param name="location">Location of the control where the table will be added.</param>
        /// <param name="id">ID of the control where the table will be added.</param>
        protected void AddPlayersTable(List<Player> Players, string location, string id)
        {
            if (Players.Count.Equals(0))
                throw new EmptyException();

            if (FindControl(location) is null)
                throw new ArgumentNullException("location");

            HtmlGenericControl dataText = new HtmlGenericControl("div");
            dataText.Attributes.Add("class", "tournament__data-text-container");
            Label text = new Label();
            text.ID = "Label" + id;
            text.CssClass = "tournament__label";
            text.Text = "Žaidėjų sąrašas";
            dataText.Controls.Add(text);

            HtmlGenericControl dataTableContainer = new HtmlGenericControl("div");
            dataTableContainer.Attributes.Add("class", "tournament__data-table-container");
            Table newTable = new Table();
            newTable.ID = "Table" + id;
            newTable.CssClass = "tournament__table";
            AddTableHeaderRow(newTable, "Vardas", "Pavardė", "Komandos pavadinimas", "Pozicija", "Čempionas/Ginklas", "Nužudymai", "Mirtys", "Padėjimai");
            for (int i = 0; i < Players.Count; i++)
            {
                string[] values = Players[i].ToString().Split('\t');
                AddTableRow(newTable, values);
            }
            dataTableContainer.Controls.Add(newTable);

            HtmlGenericControl dataContainer = new HtmlGenericControl("div");
            dataContainer.Attributes.Add("class", "tournament__data-container");
            dataContainer.Controls.Add(dataText);
            dataContainer.Controls.Add(dataTableContainer);

            FindControl(location).Controls.Add(dataContainer);
        }

        /// <summary>
        /// Adds a table of teams to the specified location.
        /// </summary>
        /// <param name="Teams">List of teams to be added to the table.</param>
        /// <param name="location">Location of the control where the table will be added.</param>
        /// <param name="id">ID of the control where the table will be added.</param>
        protected void AddTeamsTable(List<string> Teams, string location, string id)
        {
            if (Teams.Count.Equals(0))
                throw new EmptyException();

            if (FindControl(location) is null)
                throw new ArgumentNullException("location");

            HtmlGenericControl dataText = new HtmlGenericControl("div");
            dataText.Attributes.Add("class", "tournament__data-text-container");
            Label text = new Label();
            text.ID = "Label" + id;
            text.CssClass = "tournament__label";
            text.Text = "Komandų sąrašas";
            dataText.Controls.Add(text);

            HtmlGenericControl dataTableContainer = new HtmlGenericControl("div");
            dataTableContainer.Attributes.Add("class", "tournament__data-table-container");
            Table newTable = new Table();
            newTable.ID = "Table" + id;
            newTable.CssClass = "tournament__table";
            AddTableHeaderRow(newTable, "Eil. Nr.", "Komandos pavadinimas");
            for (int i = 0; i < Teams.Count; i++)
            {
                AddTableRow(newTable, (i + 1).ToString(), Teams[i]);
            }
            dataTableContainer.Controls.Add(newTable);

            HtmlGenericControl dataContainer = new HtmlGenericControl("div");
            dataContainer.Attributes.Add("class", "tournament__data-container");
            dataContainer.Controls.Add(dataText);
            dataContainer.Controls.Add(dataTableContainer);

            FindControl(location).Controls.Add(dataContainer);
        }

        /// <summary>
        /// Adds a table of new teams to the specified location.
        /// </summary>
        /// <typeparam name="T">Type of the new team to be added to the table.</typeparam>
        /// <param name="NewTeam">List of new teams to be added to the table.</param>
        /// <param name="location">Location of the control where the table will be added.</param>
        /// <param name="id">ID of the control where the table will be added.</param>
        /// <exception cref="EmptyException">Thrown when the list of new teams is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the specified location is null.</exception>
        protected void AddNewTeamTables<T>(List<T> NewTeam, string location, string id) where T : Player
        {
            if (NewTeam.Count.Equals(0))
                throw new EmptyException();

            if (FindControl(location) is null)
                throw new ArgumentNullException("location");

            HtmlGenericControl dataText = new HtmlGenericControl("div");
            dataText.Attributes.Add("class", "tournament__data-text-container");
            Label text = new Label();
            text.ID = "Label" + id;
            text.CssClass = "tournament__label";
            text.Text = string.Format($"Komanda {id}");
            dataText.Controls.Add(text);

            HtmlGenericControl dataTableContainer = new HtmlGenericControl("div");
            dataTableContainer.Attributes.Add("class", "tournament__data-table-container");
            Table newTable = new Table();
            newTable.ID = "Table" + id;
            newTable.CssClass = "tournament__table";
            for (int i = 0; i < NewTeam.Count; i++)
            {
                if (i.Equals(0))
                {
                    string[] header = NewTeam[i].Header().Split('\t');
                    AddTableHeaderRow(newTable, header);
                }
                string[] values = NewTeam[i].ToStringCSV().Split(';');
                AddTableRow(newTable, values);
            }
            dataTableContainer.Controls.Add(newTable);

            HtmlGenericControl dataContainer = new HtmlGenericControl("div");
            dataContainer.Attributes.Add("class", "tournament__data-container");
            dataContainer.Controls.Add(dataText);
            dataContainer.Controls.Add(dataTableContainer);

            FindControl(location).Controls.Add(dataContainer);
        }
        /// <summary>
        /// Adds a header row to a table with the specified cell headers.
        /// </summary>
        /// <param name="table">The table to add the row to.</param>
        /// <param name="cellsHeaders">The header text for each cell in the row.</param>
        protected void AddTableHeaderRow(Table table, params string[] cellsHeaders)
        {
            TableHeaderRow hRow = new TableHeaderRow();
            for (int i = 0; i < cellsHeaders.Length; i++)
            {
                TableHeaderCell hCell = new TableHeaderCell { Text = cellsHeaders[i] };
                hRow.Cells.Add(hCell);
            }
            table.Rows.Add(hRow);
        }

        /// <summary>
        /// Adds a row to a table with the specified cell text.
        /// </summary>
        /// <param name="table">The table to add the row to.</param>
        /// <param name="cells">The text for each cell in the row.</param>
        protected void AddTableRow(Table table, params string[] cells)
        {
            TableHeaderRow row = new TableHeaderRow();
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].Equals(string.Empty))
                    cells[i] = "-";
                TableCell cell = new TableCell { Text = cells[i] };
                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
        }
    }
}
using LabDarbas4_19.App_Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabDarbas4_19
{
    public partial class WebInterface : System.Web.UI.Page
    {
        const string CDataStorageName = "App_Data";
        const string CResultStorageName = "App_Results";
        const string CFr = "Results.txt";
        const string CFr1 = "Visi.csv";
        const string CFr2 = "Rinktine.csv";
        const string CFr3 = "Komandos.csv";
        const string InitialDataPanel = "InitialData";
        const string NewTeamsPanel = "NewTeams";
        const string BestPlayersPanel = "BestPlayers";
        const string PlayersPanel = "Players";
        const string TeamsPanel = "Teams";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Server.HtmlEncode(FileUpload1.FileName);
                string fileExtention = Path.GetExtension(fileName);

                if (fileExtention.Equals(".txt"))
                {
                    string filePath = Server.MapPath(CDataStorageName + '/' + fileName);

                    FileUpload1.SaveAs(filePath);
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath(CDataStorageName);
            DirectoryInfo PathToFiles = new DirectoryInfo(path);

            if (PathToFiles.Exists)
            {
                string fileResultPath = Server.MapPath(CResultStorageName + '/' + CFr);
                string fileVisiPath = Server.MapPath(CResultStorageName + '/' + CFr1);
                string fileKomandosPath = Server.MapPath(CResultStorageName + '/' + CFr3);
                string fileRinktinesPath = Server.MapPath(CResultStorageName + '/' + CFr2);

                if (File.Exists(fileResultPath))
                    File.Delete(fileResultPath);
                if (File.Exists(fileRinktinesPath))
                    File.Delete(fileRinktinesPath);

                List<Round> Rounds = new List<Round>();

                InOutUtils.ReadData(PathToFiles.FullName, Rounds);

                try
                {
                    int CSPlayerCriteria = int.Parse(TextBox1.Text);
                    int LoLPlayerCriteria = int.Parse(TextBox2.Text);

                    List<CSPlayer> CSTeam = TaskUtils.NewTeam<CSPlayer, int>(Rounds, CSPlayerCriteria);

                    try
                    {
                        AddNewTeamTables(CSTeam, NewTeamsPanel, "CSPlayer");
                    }
                    catch (EmptyException ex)
                    {
                        AddEmptyTable(NewTeamsPanel);
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    catch (ArgumentNullException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    try
                    {
                        InOutUtils.PrintNewTeamToFile(fileResultPath, CSTeam, "New CS TEAM");
                    }
                    catch (EmptyException ex)
                    {
                        InOutUtils.PrintEmpty(fileResultPath);
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    try
                    {
                        InOutUtils.PrintNewTeamToFileCSV(fileRinktinesPath, CSTeam, "New CS TEAM");
                    }
                    catch (EmptyException ex)
                    {
                        InOutUtils.PrintEmptyCSV(fileRinktinesPath);
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    List<LoLPlayer> LoLTeam = TaskUtils.NewTeam<LoLPlayer, int>(Rounds, LoLPlayerCriteria);

                    try
                    {
                        AddNewTeamTables(LoLTeam, NewTeamsPanel, "LoLPlayer");
                    }
                    catch (EmptyException ex)
                    {
                        AddEmptyTable(NewTeamsPanel);
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    catch (ArgumentNullException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    try
                    {
                        InOutUtils.PrintNewTeamToFile(fileResultPath, LoLTeam, "New LOL TEAM");
                    }
                    catch (EmptyException ex)
                    {
                        InOutUtils.PrintEmpty(fileResultPath);
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    try
                    {
                        InOutUtils.PrintNewTeamToFileCSV(fileRinktinesPath, LoLTeam, "New LOL TEAM");
                    }
                    catch (EmptyException ex)
                    {
                        InOutUtils.PrintEmptyCSV(fileRinktinesPath);
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                }
                catch (FormatException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                
                try
                {
                    AddInitialDataTables(Rounds, InitialDataPanel);
                }
                catch (EmptyException ex)
                {
                    AddEmptyTable(InitialDataPanel);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                try
                {
                    InOutUtils.PrintDataToFile(fileResultPath, Rounds);
                }
                catch (EmptyException ex)
                {
                    InOutUtils.PrintEmpty(fileResultPath);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                HtmlGenericControl dataText = new HtmlGenericControl("div");
                dataText.Attributes.Add("class", "tournament__data-text-container");
                Label text = new Label();
                text.CssClass = "tournament__label";
                text.Text = "Geriausi žaidėjai";
                dataText.Controls.Add(text);

                FindControl(BestPlayersPanel).Controls.Add(dataText);

                List<Player> Players = TaskUtils.GetPlayers(Rounds);
                Players.Sort();
                try
                {
                    AddPlayersTable(Players, PlayersPanel, "Players");
                }
                catch (EmptyException ex)
                {
                    AddEmptyTable(PlayersPanel);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                try
                {
                    InOutUtils.PrintPlayersToFile(fileResultPath, Players, "All players");
                }
                catch (EmptyException ex)
                {
                    InOutUtils.PrintEmpty(fileResultPath);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                try
                {
                    InOutUtils.PrintPlayersToFileCSV(fileVisiPath, Players);
                }
                catch (EmptyException ex)
                {
                    InOutUtils.PrintEmptyCSV(fileVisiPath);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                List<string> Teams = TaskUtils.GetTeams(Rounds);

                try
                {
                    AddTeamsTable(Teams, TeamsPanel, "Teams");
                }
                catch (EmptyException ex)
                {
                    AddEmptyTable(TeamsPanel);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                
                try
                {
                    InOutUtils.PrintTeamsToFile(fileResultPath, Teams, "All teams");
                }
                catch (EmptyException ex)
                {
                    InOutUtils.PrintEmpty(fileResultPath);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                try
                {
                    InOutUtils.PrintTeamsToFileCSV(fileKomandosPath, Teams);
                }
                catch (EmptyException ex)
                {
                    InOutUtils.PrintEmptyCSV(fileKomandosPath);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
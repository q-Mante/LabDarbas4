<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebInterface.aspx.cs" Inherits="LabDarbas4_19.WebInterface" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <link href="index.css" type="text/css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>WCG Turnyras</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="home-page">
            <div class="tournament">
                <div class="tournament__first-half-container">
                    <div class="tournament__input-container padding-bottom">
                        <div class="tournament__fileupload-container">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="tournament__fileupload" />
                        </div>
                        <div class="tournament__button-container">
                            <asp:Button ID="Button1" runat="server" Text="IŠSAUGOTI" CssClass="tournament__button" OnClick="Button1_Click" />
                        </div>
                    </div>
                    <asp:Panel ID="InitialData" runat="server"></asp:Panel>
                </div>
                <div class="tournament__second-half-container">
                    <div class="tournament__input-container">
                        <div class="tournament__user-input-container">
                            <div class="tournament__textbox-container">
                                <asp:Label ID="InputLabel1" runat="server" Text="CSPlayer" CssClass="tournament__label"></asp:Label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="tournament__textbox"></asp:TextBox>
                            </div>
                            <div class="tournament__textbox-container">
                                <asp:Label ID="InputLabel2" runat="server" Text="LoLPlayer" CssClass="tournament__label"></asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="tournament__textbox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="tournament__button-container">
                            <asp:Button ID="Button2" runat="server" Text="ANALIZUOTI" CssClass="tournament__button" OnClick="Button2_Click" />
                        </div>
                    </div>
                    <asp:Panel ID="BestPlayers" runat="server"></asp:Panel>
                    <asp:Panel ID="Players" runat="server"></asp:Panel>
                    <asp:Panel ID="Teams" runat="server"></asp:Panel>
                    <asp:Panel ID="NewTeams" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

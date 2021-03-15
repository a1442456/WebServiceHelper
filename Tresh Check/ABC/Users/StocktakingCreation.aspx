<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="StocktakingCreation.aspx.cs" Inherits="WebTrashCheck.Users.StocktakingCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">    
    <asp:Label ID="lblGreetings" runat="server">Stoktaking Creation</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Создать инвентаризацию</asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Table runat="server" ID="tblStocktakingDetails">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label runat="server" ID="lblCalendarDisc">Выберите дату</asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                 <asp:Calendar runat="server" ID="clndrStocktakingDate" OnSelectionChanged="clndrStocktakingDate_SelectionChanged" CssClass="calendar"></asp:Calendar>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblTime">Выберите время начала<br />инвентаризации</asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList runat="server" ID="ddlHours" Width="50" OnSelectedIndexChanged="ddlHours_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <asp:Label runat="server" ID="lblDevider"> : </asp:Label>
                <asp:DropDownList runat="server" ID="ddlMinuts" Width="50" OnSelectedIndexChanged="ddlMinuts_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblChooseDepartment">Выберите департамент</asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList runat="server" ID="ddlDepartment" Width="200" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblChoosePersone">Выберите МОЛ</asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList runat="server" ID="ddlMOL" Width="200"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label runat="server" ID="lblSelectedDate"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Button runat="server" ID="btnCreate" Text="Создать инвентаризацию" OnClick="btnCreate_Click" Width="250"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:Table runat="server" ID="tblDetails" CssClass="stocktakingDetailstbl">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label runat="server" ID="lblDiscription"> Информация по созданным инвентаризациям: </asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell VerticalAlign="Top">
                            <asp:Panel ID="pnlOpened" runat="server"></asp:Panel>
                        </asp:TableCell>
                        <asp:TableCell VerticalAlign="Top">
                            <asp:Panel ID="pnlClosed" runat="server"></asp:Panel>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
</asp:Content>

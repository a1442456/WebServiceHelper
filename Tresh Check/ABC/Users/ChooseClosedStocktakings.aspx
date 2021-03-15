<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="ChooseClosedStocktakings.aspx.cs" Inherits="WebTrashCheck.Users.ChooseClosedStocktakings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">
    <asp:Label ID="lblGreetings" runat="server">Choose Closed Stocktaking</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Выберите закрытую инвентаризацию</asp:Label>
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
            <asp:TableCell ColumnSpan="2">
                <asp:Label runat="server" ID="lblSelectedDate"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblStockTaking">Доступные<br />инвентаризации</asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList runat="server" ID="ddlStocktakingDepartment" Width="200"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Button runat="server" ID="btnNext" Text="Подробнее..." OnClick="btnNext_Click"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Panel runat="server" ID="pnlClosedStocktakings"></asp:Panel>
</asp:Content>

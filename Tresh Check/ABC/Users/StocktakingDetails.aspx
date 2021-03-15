<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="StocktakingDetails.aspx.cs" Inherits="WebTrashCheck.Users.StocktakingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">
    <asp:Label ID="lblGreetings" runat="server">Stoktaking Details</asp:Label>
    <br />
    <asp:Label runat="server" ID="lblStockTakingDetails" CssClass="lblGreetingsRus"></asp:Label>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Table runat="server" if="tblFoundedEquip">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label runat="server" ID="lblDescription"><b>Детали инвентаризации:</b></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                <asp:Panel runat="server" ID="pnlFounded"></asp:Panel>
                <asp:GridView ID="grdvw" runat="server" CssClass="tblEquipments" AllowSorting="True" PagerSettings-Position="Bottom" SelectedRowStyle-BackColor="#60BB64" Caption="Список найденного оборудования" ></asp:GridView>
            </asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                <asp:Panel runat="server" ID="pnlUnFounded"></asp:Panel>
                <asp:GridView ID="grdvw2" runat="server" CssClass="tblEquipments" AllowSorting="True" PagerSettings-Position="Bottom" SelectedRowStyle-BackColor="#60BB64" Caption="Потеряшки" ></asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label runat="server" ID="lblConfirmationWord"><br />Введите "Завершить"<br />перед завершением</asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtbxConfirmationWord"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="btnColmplete" Text="Завершить инвентаризацию" OnClick="btnColmplete_Click" Width="280" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

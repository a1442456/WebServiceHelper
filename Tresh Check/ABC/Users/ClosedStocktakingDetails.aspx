<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="ClosedStocktakingDetails.aspx.cs" Inherits="WebTrashCheck.Users.ClosedStocktakingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">
    <asp:Label ID="lblGreetings" runat="server">Stoktaking Details</asp:Label>
    <br />
    <asp:Label runat="server" ID="lblStockTakingDetails"></asp:Label>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
        <asp:Table ID="Table1" runat="server" if="tblFoundedEquip">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Button runat="server" ID="btnReportFounded" Text="Отчет присутствующих ОС" Width="250" OnClick="btnReportFounded_Click" />
            </asp:TableCell>
            <asp:TableCell>                
                <asp:Button runat="server" ID="btnReportUnFounded" Text="Отчет утерь ОС" Width="250" OnClick="btnReportUnFounded_Click" />
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
    </asp:Table>
</asp:Content>

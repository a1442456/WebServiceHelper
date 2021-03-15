<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="ViewEquipments.aspx.cs" Inherits="WebTrashCheck.Users.ViewEquipments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">
    
    <asp:Label ID="lblGreetings" runat="server">View Equipments</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Просмотр оборудования</asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Table ID="tblViewEquipments" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:GridView ID="grdvw" runat="server" CssClass="tblEquipments" AllowSorting="True" OnSorting="grdvw_Sorting"  OnRowCommand="grdvw_RowCommand" PagerSettings-Position="Bottom" SelectedRowStyle-BackColor="#cde5c5" Caption="Список имеющегося оборудования">
                    <%--Modify select button of gridview--%>
                    <columns>
                         <asp:commandfield selecttext="Скачать Этикетку" showselectbutton="True" />
                    </columns>
                </asp:GridView>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top" HorizontalAlign="Center">
                <asp:Table ID="tblFilters" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblFilterName" runat="server" Text="Фильтр по полю"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell >
                            <asp:DropDownList ID="ddlFilterName" runat="server" Width="222px" AutoPostBack="true"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxFilter" runat="server" TabIndex="0"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnSerch" runat="server" Text="Поиск" TabIndex="1" OnClick="btnSerch_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table runat="server" ID="tblPrint">
                    <asp:TableRow>
                        <asp:TableCell>
                            <label>Скачать все отобранные этикетки</label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnSaveAll" runat="server" Text="Скачать" OnClick="btnSaveAll_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
</asp:Content>

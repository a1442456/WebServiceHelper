<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BootstrapMasterPage.Master" AutoEventWireup="true" CodeBehind="SSH.aspx.cs" Inherits="WebTrashCheck.Users.SSH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphNotation" runat="server">
    <p>SSH(кассы)</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <asp:Table runat="server" Visible="false">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top" BackColor="White">
                <asp:Table ID="Table1" runat="server" BorderStyle="Solid" BorderWidth="2">
        <asp:TableRow>
            <asp:TableCell Width="600">
                 <h2>Консоль</h2>
               <asp:Label runat="server" ID="lblConsole" >
               </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
            </asp:TableCell>

            <asp:TableCell BackColor="White" VerticalAlign="Top">
                <asp:Table ID="Table2" runat="server" BorderStyle="Solid" BorderWidth="2">
        <asp:TableRow>
            
            <asp:TableCell ColumnSpan="2" >
                <asp:Label runat="server" ID="lblCashIP" >IP кассы: </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
                    <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label runat="server" ID="lblCommand">Комманда: </asp:Label> <asp:TextBox runat="server" ID="txtbxCommand" Enabled="false"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell >
                <asp:Label runat="server" ID="lblShopID">Магазин </asp:Label><asp:DropDownList runat="server" ID="ddlShopID" OnSelectedIndexChanged="ddlShopID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblCashID">Касса </asp:Label><asp:DropDownList runat="server" ID="ddlCash" AutoPostBack="true" OnSelectedIndexChanged="ddlCash_SelectedIndexChanged"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>            
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                <asp:RadioButton runat="server" ID="rbtnShowEquipment" Text="Показать оборудование" GroupName="Command" OnCheckedChanged="rbtnShowEquipment_CheckedChanged" AutoPostBack="true" />
                <br />
                <asp:RadioButton runat="server" ID="rbtnShowDate" Text="Показать дату" GroupName="Command" OnCheckedChanged="rbtnShowDate_CheckedChanged" AutoPostBack="true"/>
                <br />
                <asp:RadioButton runat="server" ID="rbtnChangeDateToCurrent" Text="Изменить дату на текущую" GroupName="Command" OnCheckedChanged="rbtnChangeDateToCurrent_CheckedChanged" AutoPostBack="true" />
                <br />
                <asp:RadioButton runat="server" ID="rbtnRebootCash" Text="Перезагрузка" GroupName="Command" OnCheckedChanged="rbtnRebootCash_CheckedChanged" AutoPostBack="true"/>
                <br />
                <asp:RadioButton runat="server" ID="rbtnRebootUKM" Text="Перезагрузка УКМ" GroupName="Command" OnCheckedChanged="rbtnRebootUKM_CheckedChanged" AutoPostBack="true"/>
                <br />
                <asp:RadioButton runat="server" ID="rbtnShutDown" Text="Выключение кассы" GroupName="Command" OnCheckedChanged="rbtnShutDown_CheckedChanged" AutoPostBack="true"/>
            </asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Button runat="server" ID="btnRun" Text="Запустить" OnClick="btnRun_Click"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <%--<h1>Start of a new blocks</h1>--%>
    <div class="row">
        <div class="col-6 tablelike" >
            <h1>Console</h1>
        </div>
        <div class="col-6 tablelike" >
            <div class="row bottomborder">
                <div class="col">
                    IP кассы<asp:TextBox runat="server" ID="txtbxCashIP" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row bottomborder">
                <div class="col">
                    <asp:Label runat="server" ID="Label1">Комманда: </asp:Label> <asp:TextBox runat="server" ID="TextBox1" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <h1>Control panel</h1>
            <h1>Control panel</h1>
            <h1>Control panel</h1>
            <h1>Control panel</h1>
            <h1>Control panel</h1>
            <h1>Control panel</h1>
            <h1>Control panel</h1>
        </div>
    </div>
</asp:Content>

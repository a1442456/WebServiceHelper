<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BootstrapMasterPage.Master" AutoEventWireup="true" CodeBehind="SSH.aspx.cs" Inherits="WebTrashCheck.Users.SSH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphNotation" runat="server">
    <p>SSH(кассы)</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="row">
        <div class="col-6 tablelike" >
            <h1>Консоль</h1>
            <asp:Label runat="server" ID="lblConsole" >
               </asp:Label>
        </div>
        <div class="col-6 tablelike" >
            <div class="row bottomborder">
                <div class="col">
                    IP кассы: <asp:TextBox runat="server" ID="txtbxCashIP" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row bottomborder">
                <div class="col">
                    <asp:Label runat="server" ID="Label1">Комманда: </asp:Label><asp:TextBox runat="server" ID="txtbxCommand" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row bottomborder" >
                    <div class="col-6 rightborder">
                        <asp:Label runat="server" ID="lblShopID">Магазин </asp:Label></br><asp:DropDownList runat="server" ID="ddlShopID" OnSelectedIndexChanged="ddlShopID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-6"> 
                        <asp:Label runat="server" ID="lblCashID">Касса </asp:Label></br><asp:DropDownList runat="server" ID="ddlCash" AutoPostBack="true" OnSelectedIndexChanged="ddlCash_SelectedIndexChanged"></asp:DropDownList>
                    </div>
            </div>
            <div class="row bottomborder">
                <div class="col text-left">
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
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Button runat="server" ID="btnRun" CssClass="btn text-light" Text="Запустить" OnClick="btnRun_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

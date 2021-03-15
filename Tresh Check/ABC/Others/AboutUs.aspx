<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="WebTrashCheck.Else.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    here should be info about us
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphGreetings" runat="server">

     <asp:Label ID="lblGreetings" runat="server">You Are Welcome To Stocktaking's Page</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Добро пожаловать на страницу инвентаризаций</asp:Label>
    <asp:Label ID="l1blGreetings" runat="server" CssClass="lblGreetings">Welcome to ABC-Tests<br/>About us</asp:Label>

</asp:Content>
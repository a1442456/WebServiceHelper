<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="WebTrashCheck.test" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <%--<h1>Герои касс</h1>
    (Будет дополненно)<br />
    <asp:Image runat="server" ID="imgLogoSlider" ImageUrl="../Images/Logo%20slider1.gif" CssClass="ImageLogoSlider" BorderStyle="Solid"/>--%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">
    <asp:Label ID="lblGreetings" runat="server">You Are Welcome To Service-Helper's Page</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Добро пожаловать на web-страницу Сервис Хелпера</asp:Label>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="WebTrashCheck.Others.Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">
    <asp:Label ID="lblGreetings" runat="server">What To Do List</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Список работ</asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <s>Image Slider на главной странице</s> ✓<br />
    <s>Создание Инвентаризаций по МОЛ (Мат. ответственное лицо)</s> ✓<br />
    <s>Работа с инвентаризациями по МОЛ</s> ✓<br />
    <s>Создание класса GridView как на главное странице с входным параметром DataSet.Table</s> ✓<br />
    <s>Просмотр закрытых инвентаризаций</s> ✓<br />
    Печать отчетов по проведенным инвентаризациям<br />
    Создать транзитную БД и конвектор к ней<br />
    Прикрутить фильртры по полям к таблицам данных
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BootstrapMasterPage.Master" AutoEventWireup="true" CodeBehind="TestPageWithMaster.aspx.cs" Inherits="WebTrashCheck.Admins.TestPageWithMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <h5><asp:Label ID="lblContacts" runat="server">Не для коммерческого использования!<br />Контакты:<br />e-mail: a1442456@gmail.com</asp:Label></h5>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphGreetings" runat="server">
     <asp:Label ID="lblGreetings" runat="server">Contact's Page</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Страница контактов</asp:Label>
    

</asp:Content>

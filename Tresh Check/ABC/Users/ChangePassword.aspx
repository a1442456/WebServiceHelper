<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WebTrashCheck.Users.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphGreetings" runat="server">
    <asp:Label ID="lblGreetings" runat="server">Change Password</asp:Label>
    <br />
    <asp:Label ID="lblGreetingsRus" runat="server" CssClass="lblGreetingsRus">Смена пароля пользователя</asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label runat="server" ID="lblInfo"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblOldPassword">Старый пароль</asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="txtbxOldPassword" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblNewPassword">Новый пароль</asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="txtbxNewPassword" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" ID="lblNewPasswordRepeat">Повторите новый пароль</asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="textbxNewPasswordRepeat" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Button runat="server" ID="btnChangePassword" Text="Сменить" OnClick="btnChangePassword_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BootstrapMasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WebTrashCheck.Users.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphNotation" runat="server">
    <p>Смена пароля</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="row">
        <div class="col-3"></div>
        <div class="col-5">
            <asp:Label runat="server" ID="lblInfo"></asp:Label>
            <div class="row">
                <div class="col">
                    <div class="row">
                        <div class="col">
                            <asp:Label runat="server" ID="lblOldPassword">Старый пароль</asp:Label>
                        </div>
                        <div class="col">
                            <asp:TextBox runat="server" ID="txtbxOldPassword" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="row">
                        <div class="col">
                            <asp:Label runat="server" ID="lblNewPassword">Новый пароль</asp:Label>
                        </div>
                        <div class="col">
                            <asp:TextBox runat="server" ID="txtbxNewPassword" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="row">
                        <div class="col ">
                            <asp:Label runat="server" ID="lblNewPasswordRepeat">Повторите новый пароль</asp:Label>
                        </div>
                        <div class="align-content-center col">
                             <asp:TextBox runat="server" ID="textbxNewPasswordRepeat" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Button runat="server" class="btn text-light" ID="btnChangePassword" Text="Сменить" OnClick="btnChangePassword_Click" />
                </div>
            </div>
            </div>
           <div class="col-4"></div>
    </div>
</asp:Content>

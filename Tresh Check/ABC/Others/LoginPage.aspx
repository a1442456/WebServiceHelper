<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BootstrapMasterPage.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WebTrashCheck.Else.Login" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphNotation" runat="server">
    <p>Авторизация</p>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <%--login table--%>
        <div class="row text-center ">
            <div class="col-4"></div>
            <div class="col-4 text-light text-center " >
                    <div class="row " >
                        <div class="col-4 text-center">
                            <asp:Label runat="server" ID="lblLogin">Логин</asp:Label>
                        </div>
                        <div class="col-8 text-center">
                            <asp:TextBox ID="txtbxLogin" runat="server"></asp:TextBox>
                        </div>
                    
                    </div>
                    <div class="row" >
                        <div class="col-4 text-center">
                            <asp:Label runat="server" ID="lblPassword">Пароль</asp:Label>
                        </div>
                        <div class="col-8 text-center">  
                           <asp:TextBox  ID="txtbxPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    
                    </div>
                    <div class="row" >
                       <div class="col-12 text-center">
                            <asp:Button ID="btnLogin" class="btn text-light" runat="server" OnClick="btnLogin_Click" Text="Вход"/>
                        </div>
                    </div>
                </div>
                <div class="col-3"></div>
             </div>
    <%--login table ends--%>
</asp:Content>

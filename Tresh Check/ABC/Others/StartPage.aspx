<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BootstrapMasterPage.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="WebTrashCheck.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphNotation" runat="server">
    <p>Service Helper BMK</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel" style="height:220px">
  <ol class="carousel-indicators">
    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="3"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="4"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="5"></li>
  </ol>
  <div class="carousel-inner">
    <div class="carousel-item active">
      <h1>Что такое Service Helper?</h1>
      <h2>Для чего нужен Service Helper?</h2>
        <br />
        <br />
        <br />
    </div>
    <div class="carousel-item">
      <h1>Работа с кассами</h1>
        <div class="row">
            <div class="col-12 text-center">
                <h5>Работа по SSH</h5>
                <h5>Проверка связи</h5>
                <h5>Скачивание логов</h5>
                <br />
                <br />
            </div>
        </div>
    </div>
    <div class="carousel-item">
        <h1>Бейджи</h1>
        <div class="row">
            <div class="col-12 text-center">
                <h5>Генерация</h5>
                <h5>Отправка</h5>
                <br />
                <br />
                <br />
            </div>
        </div>
    </div>
      <div class="carousel-item">
        <h1>Подключение к ПК пользователей</h1>
        <div class="row">
            <div class="col-12 text-center">
                <h5>VNC</h5>
                <h5>DameWare</h5>
                <br />
                <br />
                <br />
            </div>
        </div>
    </div>
    <div class="carousel-item">
        <h1>Настольная версия</h1>
        <h5>.NET Framework Win Forms</h5>
        <h1>Web версия</h1>
        <h5>ASP.NET Framework Web Forms</h5>
    </div>
    <div class="carousel-item">
        <h1>Использованные технологии</h1>
        <h5>.NET Framework C#</h5>
        <h5>MS SQL server T-Sql</h5>
        <h5>HTML/Div-верстка/Bootstrap 5.0/CSS</h5>
        <br />
    </div>
  </div>
  </div>
  <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
   </asp:Content>




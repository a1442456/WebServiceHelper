<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BootstrapMasterPage.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="WebTrashCheck.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphNotation" runat="server">
    <p>Service Helper BMK</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
  <ol class="carousel-indicators">
    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
  </ol>
  <div class="carousel-inner">
    <div class="carousel-item active">
      <h1>Что такое Service Helper?</h1>
      <h2><br/>Для чего нужен Service Helper?<br/><br/></h2>
    </div>
    <div class="carousel-item">
      <h1>Я ХЗ</h1>
      <h2><br/>Для чего нужен Service Helper<br/><br/></h2>
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
</div>
   </asp:Content>




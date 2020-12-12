<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppReservasSW._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
body {font-family: Arial, Helvetica, sans-serif;background-image:url("./Content/businessLogin.jpg");background-size:auto;background-repeat: no-repeat;}

.defaultbox{
    width:70%;
    margin: auto;
    border: 2px solid green;
    padding: 10px;
    background-color:white;
}
</style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div class="defaultbox">
    <div class="jumbotron">
        <div class="h1">Proyecto Diseño web - ULACIT 2020 - III Cuatrimestre</div>
        <p class="lead">Este proyecto esta disponible en Github</p>
        <p><a target="_blank" href="https://github.com/prograulacit/DisenoWeb-ProyectoFinal-Frontend" class="btn btn-primary btn-lg">Repositorio de Github</a></p>
    </div>
    <br/>
<div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img class="d-block w-100 h-50" src="./Content/car1.jpg" alt="First slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100 h-50" src="./Content/car2.jpg" alt="Second slide">
    </div>
    <div class="carousel-item">
      <img class="d-block w-100 h-50" src="./Content/car3.jpg" alt="Third slide">
    </div>
  </div>
  <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
    <br/>
    <br/>
</div>

</asp:Content>

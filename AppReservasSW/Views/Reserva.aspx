<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="AppReservasSW.Views.Reserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Public/general-style.css">
<style>
body {font-family: Arial, Helvetica, sans-serif;background-color:black;background-size:auto;background-repeat: no-repeat;}

.defaultbox{
    width:90%;
    margin: auto;
    border: 2px solid green;
    padding: 10px;
    background-color:white;
}
</style>s
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="defaultbox">

    <div class="h1">Reserva</div>

    <asp:GridView CssClass="content_table row justify-content-center text-mute" ID="grdReservas" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>

    <table class="form_table row justify-content-center">
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label2" runat="server" Text="Código reserva"></asp:Label>
            </td>
            <td>
                <asp:TextBox class="form-control" ID="Textbox_CodigoReserva" runat="server" type="number"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label1" runat="server" Text="Código de habitación"></asp:Label>
            </td>
            <td>
                <asp:DropDownList class="form-control" ID="DropDownList_CodigoHabitacion" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label CssClass="h4" ID="Label3" runat="server" Text="Fecha de entrada"></asp:Label>
            </td>
            <td class="modal-sm" style="width: 190px">
                <asp:Label CssClass="h4" ID="Label4" runat="server" Text="Fecha de salida"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Calendar ID="Calendar_FechaEntrada" runat="server"></asp:Calendar>
            </td>
            <td>
                <asp:Calendar ID="Calendar_FechaSalida" runat="server"></asp:Calendar>
            </td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="modal-sm" style="width: 190px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Button CssClass="btn btn-success" ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnIngresar_Click" />
                <asp:Button CssClass="btn btn-dark" ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
            </td>
            <td>
                <asp:Button CssClass="btn btn-danger" ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px; height: 21px;">
                <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
            </td>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
</asp:Content>

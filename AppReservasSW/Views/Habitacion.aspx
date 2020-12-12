<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Habitacion.aspx.cs" Inherits="AppReservasSW.Views.Habitacion" %>
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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="defaultbox">

    <div class="h1">Habitacion</div>

    <asp:GridView CssClass="content_table row justify-content-center text-mute" ID="grdHabitacion" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
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
                <asp:Label ID="Label1" runat="server" Text="Código habitación"></asp:Label>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" ID="txtCodigo" runat="server" type="number"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label7" runat="server" Text="Habitación de hotel"></asp:Label>
            </td>
            <td>
                <asp:DropDownList CssClass="form-control" ID="DropDownList_HotelRelacion" runat="server">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label2" runat="server" Text="Número habitación"></asp:Label>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" ID="TextBox_NumeroHabitacion" runat="server" type="number"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label8" runat="server" Text="Capacidad de habitación"></asp:Label>
            </td>
            <td>
                <asp:DropDownList CssClass="form-control" ID="DropDownList_capacidad" runat="server">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label9" runat="server" Text="Tipo habitación"></asp:Label>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" ID="TextBox_TipoHabitacion" runat="server" maxlength="10"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label10" runat="server" Text="Descripción"></asp:Label>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" ID="TextBox_descripcion" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label11" runat="server" Text="Estado"></asp:Label>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" ID="TextBox_Estado" runat="server" maxlength="1"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="modal-sm" style="width: 190px">
                <asp:Label ID="Label12" runat="server" Text="Precio de habitación"></asp:Label>
            </td>
            <td>
                <asp:TextBox CssClass="form-control" ID="TextBox_precio" runat="server" type="number"></asp:TextBox>
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
                <asp:Button CssClass="btn btn-success" ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnIngresar_Click" OnClientClick="LimpiarCajasDeTexto()" />
                <asp:Button CssClass="btn btn-dark" ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" OnClientClick="LimpiarCajasDeTexto()"/>
            </td>
            <td>
                <asp:Button CssClass="btn btn-danger" ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" OnClientClick="LimpiarCajasDeTexto()"/>
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
    <script src="../Scripts/habitacion.js"></script>

</asp:Content>

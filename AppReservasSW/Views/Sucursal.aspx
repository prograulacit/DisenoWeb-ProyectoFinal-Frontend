<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Sucursal.aspx.cs" Inherits="AppReservasSW.Views.Sucursal" %>

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
    <div class="container">

        <div class="h1">Sucursales</div>

        <asp:GridView CssClass="content_table row justify-content-center text-mute" ID="gridView" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
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
                <td class="modal-sm" style="width: 190px">Código</td>
                <td>
                    <asp:TextBox class="form-control" ID="TextBox_codigo" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 190px">Proveedor</td>
                <td>
                    <asp:DropDownList class="form-control" ID="DropDownList_proveedor" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 190px">Gerente</td>
                <td>
                    <asp:DropDownList class="form-control" ID="DropDownList_gerente" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 190px">Nombre</td>
                <td>
                    <asp:TextBox class="form-control" ID="TextBox_nombre" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 190px">Dirección</td>
                <td>
                    <asp:TextBox class="form-control" ID="TextBox_direccion" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 190px">Teléfono</td>
                <td>
                    <asp:TextBox class="form-control" ID="TextBox_telefono" runat="server"></asp:TextBox>
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
                    <asp:Button CssClass="btn btn-success" ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button CssClass="btn btn-dark" ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
                </td>
                <td>
                    <asp:Button CssClass="btn btn-danger" ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 190px; height: 21px;">
                    <asp:Label ID="Label_status" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
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
</div>
</asp:Content>

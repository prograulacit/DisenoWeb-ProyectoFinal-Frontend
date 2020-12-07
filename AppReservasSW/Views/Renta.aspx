<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Renta.aspx.cs" Inherits="AppReservasSW.Views.Renta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Public/general-style.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <asp:GridView CssClass="content_table" ID="gridView" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
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

        <div class="row">

            <div class="col-12 col-sm-12 col-md-12 col-lg-6">

                <table class="form_table">
                    <tr>
                        <td class="modal-sm" style="width: 190px">Código</td>
                        <td>
                            <asp:TextBox class="form-control" ID="TextBox_codigo" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="modal-sm" style="width: 190px">Auto</td>
                        <td>
                            <asp:DropDownList class="form-control" ID="DropDownList_auto" runat="server"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="modal-sm" style="width: 190px">Empleado</td>
                        <td>
                            <asp:DropDownList class="form-control" ID="DropDownList_empleado" runat="server"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="modal-sm" style="width: 190px">Descripción</td>
                        <td>
                            <asp:TextBox class="form-control" ID="TextBox_descripcion" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="modal-sm" style="width: 190px">Cantidad a rentar</td>
                        <td>
                            <asp:TextBox class="form-control" ID="TextBox_cantidadRenta" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="modal-sm" style="width: 190px">Precio de renta</td>
                        <td>
                            <asp:TextBox class="form-control" ID="TextBox_precio" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="modal-sm" style="width: 190px">&nbsp;</td>
                        <td>&nbsp;</td>
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

            <div class="col-12 col-sm-12 col-md-12 col-lg-6">

                <table class="form_table">
                    <tr>
                        <td class="modal-sm" style="width: 190px">
                            <asp:Label CssClass="h4" ID="Label3" runat="server" Text="Fecha de retiro"></asp:Label>
                        </td>
                        <td class="modal-sm" style="width: 190px">
                            <asp:Label CssClass="h4" ID="Label4" runat="server" Text="Fecha de retorno"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Calendar ID="Calendar_FechaRetiro" runat="server"></asp:Calendar>
                        </td>
                        <td>
                            <asp:Calendar ID="Calendar_FechaRetorno" runat="server"></asp:Calendar>
                        </td>
                    </tr>
                </table>

            </div>

        </div>

        <table class="form_table">
            <tr>
                <td class="modal-sm" style="width: 190px">
                    <asp:Button CssClass="btn btn-light" ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button CssClass="btn btn-light" ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
                </td>
                <td>
                    <asp:Button CssClass="btn btn-danger" ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
</asp:Content>

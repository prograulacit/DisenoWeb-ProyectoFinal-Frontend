using AppReservasSW.Controllers;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace AppReservasSW.Views
{
    public partial class Sucursal : System.Web.UI.Page
    {
        IEnumerable<Models.Sucursal> sucursales = new ObservableCollection<Models.Sucursal>();
        SucursalManager sucursalesManager = new SucursalManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarDropdownProveedor();
                RellenarDropdownGerente();
            }
            InicializarControles();
        }

        private async void RellenarDropdownProveedor()
        {
            DropDownList_proveedor.Items.Clear();

            // Trae la lista de proveedores disponibles de la base de datos.
            IEnumerable<Models.Proveedores> iEnumerable_proveedores = new ObservableCollection<Models.Proveedores>();
            ProveedoresManager proveedoresManager = new ProveedoresManager();
            iEnumerable_proveedores = await proveedoresManager.ObtenerProveedores(VG.usuarioActual.CadenaToken);
            List<Models.Proveedores> lista_proveedores = iEnumerable_proveedores.ToList();

            if (lista_proveedores.Count > 0)
            { // Rellena el dropdown con los proveedores disponibles.
                lista_proveedores.Reverse();
                for (int i = 0; i < lista_proveedores.Count(); i++)
                {
                    DropDownList_proveedor.Items
                        .Insert(0, new ListItem(lista_proveedores[i].PROV_NOMBRE
                        , "" + lista_proveedores[i].PROV_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un Proveedor.
            DropDownList_proveedor.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void RellenarDropdownGerente()
        {
            DropDownList_proveedor.Items.Clear();

            // Trae la lista de Gerentes disponibles de la base de datos.
            IEnumerable<Models.Gerente> iEnumerable_gerentes = new ObservableCollection<Models.Gerente>();
            GerenteManager generenteManager = new GerenteManager();
            iEnumerable_gerentes = await generenteManager.ObtenerGerentes(VG.usuarioActual.CadenaToken);
            List<Models.Gerente> lista_gerentes = iEnumerable_gerentes.ToList();

            if (lista_gerentes.Count > 0)
            { // Rellena el dropdown con los gerentes disponibles.
                lista_gerentes.Reverse();
                for (int i = 0; i < lista_gerentes.Count(); i++)
                {
                    DropDownList_gerente.Items
                        .Insert(0,
                        new ListItem(
                            lista_gerentes[i].GER_NOMBRE + " " +
                        "" + lista_gerentes[i].GER_APELLIDO1 + " " +
                        "" + lista_gerentes[i].GER_APELLIDO2
                        , "" + lista_gerentes[i].GER_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un Gerente.
            DropDownList_gerente.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void InicializarControles()
        {
            try
            {
                sucursales = await sucursalesManager.ObtenerSucursales(VG.usuarioActual.CadenaToken);
                gridView.DataSource = sucursales.ToList();
                gridView.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Sucursal sucursarAgregada = new Models.Sucursal();
                Models.Sucursal sucursal = new Models.Sucursal()
                {
                    PROV_CODIGO = Int32.Parse(DropDownList_proveedor.SelectedValue.ToString()),
                    GER_CODIGO = Int32.Parse(DropDownList_gerente.SelectedValue.ToString()),
                    SUC_NOMBRE = TextBox_nombre.Text,
                    SUC_DIRECCION = TextBox_direccion.Text,
                    SUC_TELEFONO = TextBox_telefono.Text
                };

                sucursarAgregada =
                    await sucursalesManager.Ingresar(sucursal, VG.usuarioActual.CadenaToken);

                if (sucursarAgregada != null)
                {
                    MensajeEstado("Registro guardado con exito", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("Ha habido un error al guardar el registro", true, true);
                }
            }
        }

        protected async void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Sucursal sucursalModificada = new Models.Sucursal();
                Models.Sucursal sucursal = new Models.Sucursal()
                {
                    SUC_CODIGO = Int32.Parse(TextBox_codigo.Text),
                    PROV_CODIGO = Int32.Parse(DropDownList_proveedor.SelectedValue.ToString()),
                    GER_CODIGO = Int32.Parse(DropDownList_gerente.SelectedValue.ToString()),
                    SUC_NOMBRE = TextBox_nombre.Text,
                    SUC_DIRECCION = TextBox_direccion.Text,
                    SUC_TELEFONO = TextBox_telefono.Text
                };

                sucursalModificada =
                    await sucursalesManager.Actualizar(sucursal, VG.usuarioActual.CadenaToken);

                if (sucursalModificada != null)
                {
                    MensajeEstado("Registro modificado con exito", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("Hubo un error al intentar modificar el registro", true, true);
                }
            }
        }

        protected async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox_codigo.Text) &&
                VG.CadenaSoloNumeros(TextBox_codigo.Text))
            {
                string codigoEliminado = string.Empty;
                string codigoElemento = string.Empty;

                codigoElemento = TextBox_codigo.Text;

                codigoEliminado =
                    await sucursalesManager.Eliminar(codigoElemento, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    MensajeEstado("Registro ha sido eliminado correctamente", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("El registro no puede ser eliminado. (Registry does not exist or foreign key constraint)", true, true);
                }
            }
            else
            {
                MensajeEstado("Debe ingresar el código para eliminar un registro", true, true);
            }
        }

        private bool ValidarInsertar()
        {
            if (TextBox_nombre.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el nombre de la sucursal a guardar", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la dirección de la sucursal a guardar", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el teléfono de la sucursal a guardar", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_telefono.Text))
            {
                MensajeEstado("Debe ingresar el teléfono valido", true, true);
                return false;
            }

            if (DropDownList_proveedor.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un proveedor", true, true);
                return false;
            }

            if (DropDownList_gerente.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un gerente", true, true);
                return false;
            }

            return true;
        }

        private bool ValidarModificar()
        {
            if (TextBox_nombre.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el nombre de la sucurcal a modificar", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la dirección de la sucursal a modificar", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el teléfono de la sucursal a guardar", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_telefono.Text))
            {
                MensajeEstado("Debe ingresar el teléfono valido", true, true);
                return false;
            }

            if (DropDownList_proveedor.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un proveedor", true, true);
                return false;
            }

            if (DropDownList_gerente.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un gerente", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_codigo.Text))
            {
                MensajeEstado("Debe ingresar un código valido", true, true);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Muestra un mensaje en pantalla del lado del cliente.
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        /// <param name="error">True->Letras color rojo. False->Letras color verde.</param>
        /// <param name="visible">True=Mensaje visible. False=Mensaje no visible.</param>
        private void MensajeEstado(string mensaje, bool error, bool visible)
        {
            Label_status.Text = mensaje;
            if (error)
                Label_status.ForeColor = Color.Maroon;
            else
                Label_status.ForeColor = Color.DarkGreen;
            Label_status.Visible = visible;
        }
    }
}
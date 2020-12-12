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
    public partial class Proveedores : System.Web.UI.Page
    {
        IEnumerable<Models.Proveedores> proveedores = new ObservableCollection<Models.Proveedores>();
        ProveedoresManager proveedoresManager = new ProveedoresManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                proveedores = await proveedoresManager.ObtenerProveedores(VG.usuarioActual.CadenaToken);
                gridView.DataSource = proveedores.ToList();
                gridView.DataBind();
            }
            catch (Exception)
            { // Token caduco, redirecciona al Login.
                Response.Redirect("~/Login.aspx");
            }
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Proveedores proveedorAgregado = new Models.Proveedores();
                Models.Proveedores proveedor = new Models.Proveedores()
                {
                    PROV_NOMBRE = TextBox_nombre.Text,
                    PROV_DIRECCION = TextBox_direccion.Text,
                    PROV_TELEFONO = TextBox_telefono.Text
                };

                proveedorAgregado =
                    await proveedoresManager.Ingresar(proveedor, VG.usuarioActual.CadenaToken);

                if (proveedorAgregado != null)
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
                Models.Proveedores proveedorModificado = new Models.Proveedores();
                Models.Proveedores proveedor = new Models.Proveedores()
                {
                    PROV_CODIGO = Int32.Parse(TextBox_codigo.Text),
                    PROV_NOMBRE = TextBox_nombre.Text,
                    PROV_DIRECCION = TextBox_direccion.Text,
                    PROV_TELEFONO = TextBox_telefono.Text
                };

                proveedorModificado =
                    await proveedoresManager.Actualizar(proveedor, VG.usuarioActual.CadenaToken);

                if (proveedorModificado != null)
                {
                    MensajeEstado("Registro modificado con exito", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("Hubo un error al intentar modificar el registro", false, true);
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
                    await proveedoresManager.Eliminar(codigoElemento, VG.usuarioActual.CadenaToken);

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
                MensajeEstado("Debe ingresar el nombre del proveedor a guardar", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la dirección del proveedor a guardar", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el teléfono del proveedor a guardar", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_telefono.Text))
            {
                MensajeEstado("Debe ingresar el teléfono valido", true, true);
                return false;
            }

            return true;
        }

        private bool ValidarModificar()
        {
            if (TextBox_nombre.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el nombre del proveedor a modificar", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la dirección del proveedor a modificar", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el teléfono del proveedor a modificar", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_telefono.Text))
            {
                MensajeEstado("Debe ingresar el teléfono valido", true, true);
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
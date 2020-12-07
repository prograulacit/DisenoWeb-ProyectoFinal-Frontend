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
    public partial class Modelo : System.Web.UI.Page
    {
        IEnumerable<Models.Modelo> modelos = new ObservableCollection<Models.Modelo>();
        ModeloManager modelosManager = new ModeloManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                modelos = await modelosManager.ObtenerModelos(VG.usuarioActual.CadenaToken);
                gridView.DataSource = modelos.ToList();
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
                Models.Modelo modeloAgregado = new Models.Modelo();
                Models.Modelo modelo = new Models.Modelo()
                {
                    MOD_NOMBRE = TextBox_nombre.Text,
                    MOD_COLOR = TextBox_color.Text
                };

                modeloAgregado =
                    await modelosManager.Ingresar(modelo, VG.usuarioActual.CadenaToken);

                if (modeloAgregado != null)
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
                Models.Modelo modeloModificado = new Models.Modelo();
                Models.Modelo modelo = new Models.Modelo()
                {
                    MOD_CODIGO = Int32.Parse(TextBox_codigo.Text),
                    MOD_NOMBRE = TextBox_nombre.Text,
                    MOD_COLOR = TextBox_color.Text
                };

                modeloModificado =
                    await modelosManager.Actualizar(modelo, VG.usuarioActual.CadenaToken);

                if (modeloModificado != null)
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
                Int32.TryParse(TextBox_codigo.Text, out int num))
            {
                string codigoEliminado = string.Empty;
                string codigoElemento = string.Empty;

                codigoElemento = TextBox_codigo.Text;

                codigoEliminado =
                    await modelosManager.Eliminar(codigoElemento, VG.usuarioActual.CadenaToken);

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
                MensajeEstado("Debe ingresar el nombre del modelo a guardar", true, true);
                return false;
            }

            if (TextBox_color.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el color del modelo a guardar", true, true);
                return false;
            }

            return true;
        }

        private bool ValidarModificar()
        {
            if (TextBox_nombre.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el nombre del modelo a guardar", true, true);
                return false;
            }

            if (TextBox_color.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el color del modelo a guardar", true, true);
                return false;
            }

            if (!Int32.TryParse(TextBox_codigo.Text, out int num))
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
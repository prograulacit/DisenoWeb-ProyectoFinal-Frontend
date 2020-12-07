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
    public partial class Gerente : System.Web.UI.Page
    {
        IEnumerable<Models.Gerente> gerentes = new ObservableCollection<Models.Gerente>();
        GerenteManager gerentesManager = new GerenteManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                gerentes = await gerentesManager.ObtenerGerentes(VG.usuarioActual.CadenaToken);
                gridView.DataSource = gerentes.ToList();
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
                Models.Gerente gerenteAgregado = new Models.Gerente();
                Models.Gerente gerente = new Models.Gerente()
                {
                    GER_NOMBRE = TextBox_nombre.Text,
                    GER_APELLIDO1 = TextBox_primerApellido.Text,
                    GER_APELLIDO2 = TextBox_segundoApellido.Text,
                    GER_DIRECCION = TextBox_direccion.Text,
                    GER_TELEFONO = TextBox_telefono.Text
                };

                gerenteAgregado =
                    await gerentesManager.Ingresar(gerente, VG.usuarioActual.CadenaToken);

                if (gerenteAgregado != null)
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
                Models.Gerente gerenteModificado = new Models.Gerente();
                Models.Gerente gerente = new Models.Gerente()
                {
                    GER_CODIGO = Int32.Parse(TextBox_codigo.Text),
                    GER_NOMBRE = TextBox_nombre.Text,
                    GER_APELLIDO1 = TextBox_primerApellido.Text,
                    GER_APELLIDO2 = TextBox_segundoApellido.Text,
                    GER_DIRECCION = TextBox_direccion.Text,
                    GER_TELEFONO = TextBox_telefono.Text
                };

                gerenteModificado =
                    await gerentesManager.Actualizar(gerente, VG.usuarioActual.CadenaToken);

                if (gerenteModificado != null)
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
                Int32.TryParse(TextBox_codigo.Text, out int num))
            {
                string codigoEliminado = string.Empty;
                string codigoElemento = string.Empty;

                codigoElemento = TextBox_codigo.Text;

                codigoEliminado =
                    await gerentesManager.Eliminar(codigoElemento, VG.usuarioActual.CadenaToken);

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

            if (TextBox_primerApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el apellido paterno del gerente a guardar", true, true);
                return false;
            }

            if (TextBox_segundoApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el apellido materno del gerente a guardar", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la dirección del gerente a guardar", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el teléfono del gerente a guardar", true, true);
                return false;
            }

            if (Int32.TryParse(TextBox_telefono.Text, out int num))
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
                MensajeEstado("Debe ingresar el nombre del modelo a modificar", true, true);
                return false;
            }

            if (TextBox_primerApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el apellido paterno del gerente a modificar", true, true);
                return false;
            }

            if (TextBox_segundoApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el apellido materno del gerente a modificar", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la dirección del gerente a modificar", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el teléfono del gerente a modificar", true, true);
                return false;
            }

            if (Int32.TryParse(TextBox_telefono.Text, out int num))
            {
                MensajeEstado("Debe ingresar el teléfono valido", true, true);
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
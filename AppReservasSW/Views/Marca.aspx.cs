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
    public partial class Marca : System.Web.UI.Page
    {
        IEnumerable<Models.Marca> combustibles = new ObservableCollection<Models.Marca>();
        MarcaManager marcaManager = new MarcaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (VG.usuarioActual == null)
                Response.Redirect("Login.aspx");

            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                combustibles = await marcaManager.ObtenerMarcas(VG.usuarioActual.CadenaToken);
                gridView.DataSource = combustibles.ToList();
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
                Models.Marca marcaAgregada = new Models.Marca();
                Models.Marca marca = new Models.Marca()
                {
                    MAR_NOMBRE = TextBox_marca.Text,
                };

                marcaAgregada =
                    await marcaManager.Ingresar(marca, VG.usuarioActual.CadenaToken);

                if (marcaAgregada != null)
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
                Models.Marca marcaModificada = new Models.Marca();
                Models.Marca marca = new Models.Marca()
                {
                    MAR_CODIGO = Int32.Parse(TextBox_codigo.Text),
                    MAR_NOMBRE = TextBox_marca.Text
                };

                marcaModificada =
                    await marcaManager.Actualizar(marca, VG.usuarioActual.CadenaToken);

                if (marcaModificada != null)
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
            if (!string.IsNullOrEmpty(TextBox_codigo.Text))
            {
                string codigoEliminado = string.Empty;
                string codigoElemento = string.Empty;

                codigoElemento = TextBox_codigo.Text;

                codigoEliminado =
                    await marcaManager.Eliminar(codigoElemento, VG.usuarioActual.CadenaToken);

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
            if (TextBox_marca.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el nombre de la marca a guardar", true, true);
                return false;
            }

            return true;
        }

        private bool ValidarModificar()
        {
            if (TextBox_marca.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el nombre de la marca a modificar", true, true);
                return false;
            }

            if (TextBox_codigo.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el código del combustible a modificar", true, true);
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
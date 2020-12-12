using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using AppReservasSW.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.Ajax.Utilities;

namespace AppReservasSW.Views
{
    public partial class Cliente : System.Web.UI.Page
    {
        IEnumerable<Models.Cliente> clientes = new ObservableCollection<Models.Cliente>();
        ClienteManager clienteManager = new ClienteManager();

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
                clientes = await clienteManager.ObtenerClientes(VG.usuarioActual.CadenaToken);
                grdClientes.DataSource = clientes.ToList();
                grdClientes.DataBind();
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
                Models.Cliente clienteIngresado = new Models.Cliente();
                Models.Cliente cliente = new Models.Cliente()
                {
                    USU_IDENTIFICACION = TextBox_identificacion.Text,
                    CLI_NOMBRE = TextBox_Nombre.Text,
                    CLI_APELLIDO1 = TextBox_primerApellido.Text,
                    CLI_APELLIDO2 = TextBox_segundoApellido.Text,
                    CLI_DIRECCION = TextBox_direccion.Text,
                    CLI_TELEFONO = TextBox_telefono.Text
                };

                clienteIngresado =
                    await clienteManager.Ingresar(cliente, VG.usuarioActual.CadenaToken);

                if (clienteIngresado != null)
                {
                    MensajeEstado("Registro de cliente ingresado con exito", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("Ha ocurrido un error (No puede registrar otro cliente con el mismo número de intentificación.)", true, true);
                }
            }
        }

        protected async void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Cliente clienteModificado = new Models.Cliente();
                Models.Cliente cliente = new Models.Cliente()
                {
                    USU_IDENTIFICACION = TextBox_identificacion.Text,
                    CLI_NOMBRE = TextBox_Nombre.Text,
                    CLI_APELLIDO1 = TextBox_primerApellido.Text,
                    CLI_APELLIDO2 = TextBox_segundoApellido.Text,
                    CLI_DIRECCION = TextBox_direccion.Text,
                    CLI_TELEFONO = TextBox_telefono.Text
                };

                clienteModificado =
                    await clienteManager.Actualizar(cliente, VG.usuarioActual.CadenaToken);

                if (clienteModificado != null)
                {
                    MensajeEstado("Registro de cliente modificado con exito", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("Hubo un error al intentar modificar el cliente", false, true);
                }
            }
        }

        protected async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox_identificacion.Text))
            {
                string codigoClienteEliminado = string.Empty;
                string codigoCliente = string.Empty;

                codigoCliente = TextBox_identificacion.Text;

                codigoClienteEliminado =
                    await clienteManager.Eliminar(codigoCliente, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoClienteEliminado))
                {
                    MensajeEstado("Registro de cliente eliminado correctamente", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("El registro no puede ser eliminado. (Registry does not exist or foreign key constraint)", true, true);
                }
            }
            else
            {
                MensajeEstado("Debe ingresar el código para eliminar el cliente", true, true);
            }
        }

        private bool ValidarInsertar()
        {
            if (TextBox_identificacion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su número de identifiación", true, true);
                return false;
            }

            if (TextBox_Nombre.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su nombre", true, true);
                return false;
            }

            if (TextBox_primerApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su primer apellido", true, true);
                return false;
            }

            if (TextBox_segundoApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su segundo", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su dirección", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su número de teléfono", true, true);
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
            if (TextBox_identificacion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su número de identifiación", true, true);
                return false;
            }

            if (TextBox_Nombre.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el nombre del usuario", true, true);
                return false;
            }

            if (TextBox_primerApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su primer apellido", true, true);
                return false;
            }

            if (TextBox_segundoApellido.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su segundo apellido", true, true);
                return false;
            }

            if (TextBox_direccion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar su dirección", true, true);
                return false;
            }

            if (TextBox_telefono.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar un número de teléfono", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_telefono.Text))
            {
                MensajeEstado("Debe ingresar el teléfono valido", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_identificacion.Text))
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
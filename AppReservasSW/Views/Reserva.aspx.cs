using AppReservasSW.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace AppReservasSW.Views
{
    public partial class Reserva : System.Web.UI.Page
    {
        IEnumerable<Models.Reserva> reservas = new ObservableCollection<Models.Reserva>();
        ReservaManager reservaManager = new ReservaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarDropdown_CodigoHabitacion();
            }
            InicializarControles();
        }

        private async void RellenarDropdown_CodigoHabitacion()
        {
            DropDownList_CodigoHabitacion.Items.Clear();


            // Trae la lista de habitaciones disponibles de la base de datos.
            IEnumerable<Models.Habitacion> iEnumerable_habitaciones = new ObservableCollection<Models.Habitacion>();
            HabitacionManager habitacionManager = new HabitacionManager();
            iEnumerable_habitaciones = await habitacionManager.ObtenerHabitaciones(VG.usuarioActual.CadenaToken);
            List<Models.Habitacion> lista_habitaciones = iEnumerable_habitaciones.ToList();

            if (lista_habitaciones.Count > 0)
            { // Rellena el dropdown con las habitaciones disponibles.
                lista_habitaciones.Reverse();
                for (int i = 0; i < lista_habitaciones.Count(); i++)
                {
                    DropDownList_CodigoHabitacion.Items
                        .Insert(0, new ListItem("" + lista_habitaciones[i].HAB_CODIGO
                        , "" + lista_habitaciones[i].HAB_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un hotel.
            DropDownList_CodigoHabitacion.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void InicializarControles()
        {
            try
            {
                reservas = await reservaManager.ObtenerReservas(VG.usuarioActual.CadenaToken);
                grdReservas.DataSource = reservas.ToList();
                grdReservas.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarIngresar() &&
                ValidarFecha() &&
                ValidarFecha_SalidaAntesDeEntrada())
            {
                IngresarReserva();
            }
        }

        private async void IngresarReserva()
        {
            Models.Reserva reservaIngresada = new Models.Reserva();
            Models.Reserva reserva = new Models.Reserva();

            reserva.USU_CODIGO = VG.usuarioActual.USU_CODIGO;
            reserva.HAB_CODIGO = Int32.Parse(DropDownList_CodigoHabitacion.SelectedValue.ToString());
            reserva.RES_FECHA_INGRESO = Calendar_FechaEntrada.SelectedDate;
            reserva.RES_FECHA_SALIDA = Calendar_FechaSalida.SelectedDate;

            reservaIngresada =
                await reservaManager.Ingresar(reserva, VG.usuarioActual.CadenaToken);

            if (reservaIngresada != null)
            {
                InicializarControles();
                MensajeEstado("Reserva registrada correctamente", false, true);
            }
            else
            {
                MensajeEstado("Hubo un error al ingresar la reserva", true, true);
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarIngresar() &&
                !string.IsNullOrEmpty(Textbox_CodigoReserva.Text))
            {
                ModificarReserva();
            }
            else
                MensajeEstado("Debe ingresar el código de reserva para modificar el registro", true, true);
        }

        private async void ModificarReserva()
        {
            Models.Reserva reservaModificada = new Models.Reserva();
            Models.Reserva reserva = new Models.Reserva()
            {
                RES_CODIGO = Int32.Parse(Textbox_CodigoReserva.Text),
                USU_CODIGO = VG.usuarioActual.USU_CODIGO,
                HAB_CODIGO = Int32.Parse(DropDownList_CodigoHabitacion.SelectedValue.ToString()),
                RES_FECHA_INGRESO = Calendar_FechaEntrada.SelectedDate,
                RES_FECHA_SALIDA = Calendar_FechaSalida.SelectedDate
            };

            reservaModificada =
                await reservaManager.Actualizar(reserva, VG.usuarioActual.CadenaToken);

            if (reservaModificada != null)
            {
                lblStatus.Text = "Reserva modificada correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al modificar la reserva";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Textbox_CodigoReserva.Text))
            {
                string codigoReservaEliminada = string.Empty;
                string codigoReserva = string.Empty;

                codigoReserva = Textbox_CodigoReserva.Text;

                codigoReservaEliminada =
                    await reservaManager.Eliminar(codigoReserva, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoReservaEliminada))
                {
                    MensajeEstado("Reserva eliminada correctamente", false, true);
                    InicializarControles();
                }
                else
                {
                    MensajeEstado("Hubo un error al eliminar la reserva", true, true);
                }
            }
            else
            {
                MensajeEstado("Debe ingresar el codigo de registro de reserva.", true, true);
            }
        }

        // Valida que el usuario a ingresado código de habitación y fecha
        // de salida y entrada.
        private bool ValidarIngresar()
        {
            if (DropDownList_CodigoHabitacion.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe ingresar un código de habitación.", true, true);
                return false;
            }

            if (Calendar_FechaEntrada.SelectedDate.Date == DateTime.MinValue)
            {
                MensajeEstado("Debe ingresar una fecha de entrada.", true, true);
                return false;
            }

            if (Calendar_FechaSalida.SelectedDate.Date == DateTime.MinValue)
            {
                MensajeEstado("Debe ingresar una fecha de salida.", true, true);
                return false;
            }

            return true;
        }

        // Valida si la fecha de entrada y salida no es antes de la fecha actual.
        private bool ValidarFecha()
        {
            DateTime FechaEntrada = Calendar_FechaEntrada.SelectedDate;
            DateTime FechaSalida = Calendar_FechaSalida.SelectedDate;
            if (FechaEntrada >= ObtenerFechaAyer() &&
                FechaSalida >= ObtenerFechaAyer())
            {
                return true;
            }
            MensajeEstado("No puede hacer una reserva con fecha antes de " + ObtenerFechaActual().ToString(), true, true);
            return false;
        }

        // Valida si la fecha de salida es despúes o el mismo día a la fecha de entrada.
        private bool ValidarFecha_SalidaAntesDeEntrada()
        {
            DateTime FechaEntrada = Calendar_FechaEntrada.SelectedDate;
            DateTime FechaSalida = Calendar_FechaSalida.SelectedDate;

            if (FechaSalida >= FechaEntrada)
            {
                return true;
            }
            MensajeEstado("La fecha de salida no puede ser antes de la fecha de entrada.", true, true);
            return false;
        }

        private DateTime ObtenerFechaActual()
        {
            return DateTime.Now;
        }

        // Regresa la fecha del día de ayer.
        private DateTime ObtenerFechaAyer()
        {
            return ObtenerFechaActual().AddDays(-1);
        }

        /// <summary>
        /// Muestra un mensaje en pantalla del lado del cliente.
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        /// <param name="error">True->Letras color rojo. False->Letras color verde.</param>
        /// <param name="visible">True=Mensaje visible. False=Mensaje no visible.</param>
        private void MensajeEstado(string mensaje, bool error, bool visible)
        {
            lblStatus.Text = mensaje;
            if (error)
                lblStatus.ForeColor = Color.Maroon;
            else
                lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Visible = visible;
        }
    }
}
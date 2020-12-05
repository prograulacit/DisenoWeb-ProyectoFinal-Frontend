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
    public partial class Habitacion : System.Web.UI.Page
    {
        IEnumerable<Models.Habitacion> habitaciones = new ObservableCollection<Models.Habitacion>();
        HabitacionManager habitacionManager = new HabitacionManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarDropdownCapacidad();
                RellenarDropdownHotelRelacionado();
            }
            InicializarControles();
        }

        private async void InicializarControles()
        {
            habitaciones = await habitacionManager.ObtenerHabitaciones(VG.usuarioActual.CadenaToken);
            grdHabitacion.DataSource = habitaciones.ToList();
            grdHabitacion.DataBind();
        }

        private void RellenarDropdownCapacidad()
        {
            DropDownList_capacidad.Items.Clear();
            DropDownList_capacidad.Items.Insert(0, new ListItem("12 personas", "12"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("11 personas", "11"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("10 personas", "10"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("9 personas", "9"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("8 personas", "8"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("7 personas", "7"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("6 personas", "6"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("5 personas", "5"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("4 personas", "4"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("3 personas", "3"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("2 personas", "2"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("1 persona", "1"));
            DropDownList_capacidad.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        private async void RellenarDropdownHotelRelacionado()
        {
            DropDownList_HotelRelacion.Items.Clear();
            
            // Trae la lista de hoteles disponibles de la base de datos.
            IEnumerable<Models.Hotel> iEnumerable_hoteles = new ObservableCollection<Models.Hotel>();
            HotelManager hotelManager = new HotelManager();
            iEnumerable_hoteles = await hotelManager.ObtenerHoteles(VG.usuarioActual.CadenaToken);
            List<Models.Hotel> lista_hoteles = iEnumerable_hoteles.ToList();

            if (lista_hoteles.Count > 0)
            { // Rellena el dropdown con los hoteles disponibles.
                lista_hoteles.Reverse();
                for (int i = 0; i < lista_hoteles.Count(); i++)
                {
                    DropDownList_HotelRelacion.Items
                        .Insert(0, new ListItem(lista_hoteles[i].HOT_NOMBRE
                        , "" + lista_hoteles[i].HOT_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un hotel.
            DropDownList_HotelRelacion.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        protected async void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Habitacion habitacionIngresada = new Models.Habitacion();
                Models.Habitacion habitacion = new Models.Habitacion()
                {
                    HOT_CODIGO = Int32.Parse(DropDownList_HotelRelacion.SelectedValue.ToString()),
                    HAB_NUMERO = Int32.Parse(TextBox_NumeroHabitacion.Text),
                    HAB_CAPACIDAD = Int32.Parse(DropDownList_capacidad.SelectedValue.ToString()),
                    HAB_TIPO = TextBox_TipoHabitacion.Text,
                    HAB_DESCRIPCION = TextBox_descripcion.Text,
                    HAB_ESTADO = TextBox_Estado.Text,
                    HAB_PRECIO = Int32.Parse(TextBox_precio.Text),
                };

                habitacionIngresada =
                    await habitacionManager.Ingresar(habitacion, VG.usuarioActual.CadenaToken);

                if (habitacionIngresada != null)
                {
                    lblStatus.Text = "Habitación registrada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la habitación";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        protected async void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Habitacion habitacionModificada = new Models.Habitacion();
                Models.Habitacion habitacion = new Models.Habitacion()
                {
                    HAB_CODIGO = Int32.Parse(txtCodigo.Text),
                    HOT_CODIGO = Int32.Parse(DropDownList_HotelRelacion.SelectedValue.ToString()),
                    HAB_NUMERO = Int32.Parse(TextBox_NumeroHabitacion.Text),
                    HAB_CAPACIDAD = Int32.Parse(DropDownList_capacidad.SelectedValue),
                    HAB_TIPO = TextBox_TipoHabitacion.Text,
                    HAB_DESCRIPCION = TextBox_descripcion.Text,
                    HAB_ESTADO = TextBox_Estado.Text,
                    HAB_PRECIO = Int32.Parse(TextBox_precio.Text),
                };

                habitacionModificada =
                    await habitacionManager.Actualizar(habitacion, VG.usuarioActual.CadenaToken);

                if (habitacionModificada != null)
                {
                    lblStatus.Text = "Habitación modificada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la habitación";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        protected async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                string codigoHabitacionEliminada = string.Empty;
                string codigoHabitacion = string.Empty;

                codigoHabitacion = txtCodigo.Text;

                codigoHabitacionEliminada =
                    await habitacionManager.Eliminar(codigoHabitacion, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoHabitacionEliminada))
                {
                    lblStatus.Text = "Habitación eliminada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Código de habitación proporcionado no existe";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            else
            {
                lblStatus.Text = "Debe ingresar el codigo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        private bool ValidarInsertar()
        {
            if (DropDownList_HotelRelacion.SelectedValue.ToString().Equals("seleccionar"))
            {
                lblStatus.Text = "Debe seleccionar el hotel al cual quiere registrar la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_NumeroHabitacion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_TipoHabitacion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el tipo de habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_descripcion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar una descripción a la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_precio.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe darle un precio a la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (DropDownList_capacidad.SelectedValue.ToString().Equals("0"))
            {
                lblStatus.Text = "Debe seleccionar la capacidad de personas de la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }

        private bool ValidarModificar()
        {
            if (txtCodigo.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el codigo del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (DropDownList_HotelRelacion.SelectedValue.ToString().Equals("seleccionar"))
            {
                lblStatus.Text = "Debe seleccionar el hotel al cual quiere registrar la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_NumeroHabitacion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_TipoHabitacion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el tipo de habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_descripcion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar una descripción a la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TextBox_precio.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe darle un precio a la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (DropDownList_capacidad.SelectedValue.ToString().Equals("0"))
            {
                lblStatus.Text = "Debe seleccionar la capacidad de personas de la habitación.";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}
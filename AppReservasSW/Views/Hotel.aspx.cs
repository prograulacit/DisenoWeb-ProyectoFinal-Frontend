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
    public partial class Hotel : System.Web.UI.Page
    {
        IEnumerable<Models.Hotel> hoteles = new ObservableCollection<Models.Hotel>();
        HotelManager hotelManager = new HotelManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            try
            {
                hoteles = await hotelManager.ObtenerHoteles(VG.usuarioActual.CadenaToken);
                grdHoteles.DataSource = hoteles.ToList();
                grdHoteles.DataBind();
            }
            catch (Exception)
            { // Token caduco, redirecciona al Login.
                Response.Redirect("~/Login.aspx");
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Hotel hotelIngresado = new Models.Hotel();
                Models.Hotel hotel = new Models.Hotel()
                {
                    HOT_NOMBRE = txtNombre.Text,
                    HOT_EMAIL = txtEmail.Text,
                    HOT_DIRECCION = txtDireccion.Text,
                    HOT_CATEGORIA = drpCategoria.SelectedValue.ToString(),
                    HOT_TELEFONO = txtTelefono.Text
                };

                hotelIngresado =
                    await hotelManager.Ingresar(hotel, VG.usuarioActual.CadenaToken);

                if (hotelIngresado != null)
                {
                    lblStatus.Text = "Hotel ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el hotel";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Hotel hotelModificado = new Models.Hotel();
                Models.Hotel hotel = new Models.Hotel()
                {
                    HOT_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    HOT_NOMBRE = txtNombre.Text,
                    HOT_EMAIL = txtEmail.Text,
                    HOT_DIRECCION = txtDireccion.Text,
                    HOT_CATEGORIA = drpCategoria.SelectedValue.ToString(),
                    HOT_TELEFONO = txtTelefono.Text
                };

                hotelModificado =
                    await hotelManager.Actualizar(hotel, VG.usuarioActual.CadenaToken);

                if (hotelModificado != null)
                {
                    lblStatus.Text = "Hotel modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el hotel";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text) &&
                VG.CadenaSoloNumeros(txtCodigo.Text))
            {
                string codigoHotelEliminado = string.Empty;
                string codigoHotel = string.Empty;

                codigoHotel = txtCodigo.Text;

                codigoHotelEliminado =
                    await hotelManager.Eliminar(codigoHotel, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoHotelEliminado))
                {
                    lblStatus.Text = "Hotel eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar el hotel";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            else
            {
                lblStatus.Text = "Debe ingresar el código del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private bool ValidarInsertar()
        {

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtEmail.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el email del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtDireccion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la direccion del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtTelefono.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el telefono del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (!VG.CadenaSoloNumeros(txtTelefono.Text))
            {
                lblStatus.Text = "Debe ingresar un telefono valido";
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

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtEmail.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el email del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtDireccion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la direccion del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtTelefono.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el telefono del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (!VG.CadenaSoloNumeros(txtTelefono.Text))
            {
                lblStatus.Text = "Debe ingresar un telefono valido";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (!VG.CadenaSoloNumeros(txtCodigo.Text))
            {
                lblStatus.Text = "Debe ingresar un código valido";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}
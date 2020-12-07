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
    public partial class Autos : System.Web.UI.Page
    {
        IEnumerable<Models.Autos> autos = new ObservableCollection<Models.Autos>();
        AutosManager autosManager = new AutosManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarDropdownMarca();
                RellenarDropdownModelo();
                RellenarDropdownCombustible();
                RellenarDropdownSucursal();
            }
            InicializarControles();
        }

        private async void RellenarDropdownMarca()
        {
            DropDownList_marca.Items.Clear();

            // Trae la lista de marcas disponibles de la base de datos.
            IEnumerable<Models.Marca> iEnumerable_marca = new ObservableCollection<Models.Marca>();
            MarcaManager marcaManager = new MarcaManager();
            iEnumerable_marca = await marcaManager.ObtenerMarcas(VG.usuarioActual.CadenaToken);
            List<Models.Marca> lista_marcas = iEnumerable_marca.ToList();

            if (lista_marcas.Count > 0)
            { // Rellena el dropdown con las marcas disponibles.
                lista_marcas.Reverse();
                for (int i = 0; i < lista_marcas.Count(); i++)
                {
                    DropDownList_marca.Items
                        .Insert(0, new ListItem(lista_marcas[i].MAR_NOMBRE
                        , "" + lista_marcas[i].MAR_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // una marca.
            DropDownList_marca.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void RellenarDropdownModelo()
        {
            DropDownList_modelo.Items.Clear();

            // Trae la lista de modelos disponibles de la base de datos.
            IEnumerable<Models.Modelo> IEnumerable_modelos = new ObservableCollection<Models.Modelo>();
            ModeloManager modeloManager = new ModeloManager();
            IEnumerable_modelos = await modeloManager.ObtenerModelos(VG.usuarioActual.CadenaToken);
            List<Models.Modelo> lista_modelos = IEnumerable_modelos.ToList();

            if (lista_modelos.Count > 0)
            { // Rellena el dropdown con los modelos disponibles.
                lista_modelos.Reverse();
                for (int i = 0; i < lista_modelos.Count(); i++)
                {
                    DropDownList_modelo.Items
                        .Insert(0,
                        new ListItem(
                            lista_modelos[i].MOD_NOMBRE + ". Color: " + lista_modelos[i].MOD_COLOR
                            , "" + lista_modelos[i].MOD_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un Modelo.
            DropDownList_modelo.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void RellenarDropdownCombustible()
        {
            DropDownList_combustible.Items.Clear();

            // Trae la lista de combustibles disponibles de la base de datos.
            IEnumerable<Models.Combustible> iEnumerable_combustibles = new ObservableCollection<Models.Combustible>();
            CombustibleManager combustibleManager = new CombustibleManager();
            iEnumerable_combustibles = await combustibleManager.ObtenerCombustibles(VG.usuarioActual.CadenaToken);
            List<Models.Combustible> lista_combustibles = iEnumerable_combustibles.ToList();

            if (lista_combustibles.Count > 0)
            { // Rellena el dropdown con los combustibles disponibles.
                lista_combustibles.Reverse();
                for (int i = 0; i < lista_combustibles.Count(); i++)
                {
                    DropDownList_combustible.Items
                        .Insert(0,
                        new ListItem(
                            lista_combustibles[i].COMB_TIPO, "" + lista_combustibles[i].COMB_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un combustible.
            DropDownList_combustible.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void RellenarDropdownSucursal()
        {
            DropDownList_sucursal.Items.Clear();

            // Trae la lista de sucursales disponibles de la base de datos.
            IEnumerable<Models.Sucursal> iEnumerable_sucursales = new ObservableCollection<Models.Sucursal>();
            SucursalManager sucursalManager = new SucursalManager();
            iEnumerable_sucursales = await sucursalManager.ObtenerSucursales(VG.usuarioActual.CadenaToken);
            List<Models.Sucursal> lista_sucursales = iEnumerable_sucursales.ToList();

            if (lista_sucursales.Count > 0)
            { // Rellena el dropdown con las sucursales disponibles.
                lista_sucursales.Reverse();
                for (int i = 0; i < lista_sucursales.Count(); i++)
                {
                    DropDownList_sucursal.Items
                        .Insert(0,
                        new ListItem(
                            lista_sucursales[i].SUC_NOMBRE, "" + lista_sucursales[i].SUC_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // una sucursal.
            DropDownList_sucursal.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void InicializarControles()
        {
            try
            {
                autos = await autosManager.ObtenerAutos(VG.usuarioActual.CadenaToken);
                gridView.DataSource = autos.ToList();
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
                Models.Autos autoAgregado = new Models.Autos();
                Models.Autos auto = new Models.Autos()
                {
                    MAR_CODIGO = Int32.Parse(DropDownList_marca.SelectedValue.ToString()),
                    MOD_CODIGO = Int32.Parse(DropDownList_modelo.SelectedValue.ToString()),
                    COMB_CODIGO = Int32.Parse(DropDownList_combustible.SelectedValue.ToString()),
                    SUC_CODIGO = Int32.Parse(DropDownList_sucursal.SelectedValue.ToString()),
                    AUTO_CANTIDAD = TextBox_cantidad.Text,
                    AUTO_PRECIO = Int32.Parse(TextBox_precio.Text)
                };

                autoAgregado =
                    await autosManager.Ingresar(auto, VG.usuarioActual.CadenaToken);

                if (autoAgregado != null)
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
                Models.Autos autoModificado = new Models.Autos();
                Models.Autos auto = new Models.Autos()
                {
                    AUTO_CODIGO = Int32.Parse(TextBox_codigo.Text),
                    MAR_CODIGO = Int32.Parse(DropDownList_marca.SelectedValue.ToString()),
                    MOD_CODIGO = Int32.Parse(DropDownList_modelo.SelectedValue.ToString()),
                    COMB_CODIGO = Int32.Parse(DropDownList_combustible.SelectedValue.ToString()),
                    SUC_CODIGO = Int32.Parse(DropDownList_sucursal.SelectedValue.ToString()),
                    AUTO_CANTIDAD = TextBox_cantidad.Text,
                    AUTO_PRECIO = Int32.Parse(TextBox_precio.Text)
                };

                autoModificado =
                    await autosManager.Actualizar(auto, VG.usuarioActual.CadenaToken);

                if (autoModificado != null)
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
                    await autosManager.Eliminar(codigoElemento, VG.usuarioActual.CadenaToken);

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
            if (DropDownList_marca.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar una marca", true, true);
                return false;
            }

            if (DropDownList_modelo.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un modelo", true, true);
                return false;
            }

            if (DropDownList_combustible.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un combustible", true, true);
                return false;
            }

            if (DropDownList_sucursal.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar una sucursal", true, true);
                return false;
            }

            if (TextBox_cantidad.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la cantidad de autos en posesión", true, true);
                return false;
            }

            if (!Int32.TryParse(TextBox_cantidad.Text, out int num1))
            {
                MensajeEstado("Debe introducir una cantidad de autos valida", true, true);
                return false;
            }

            if (TextBox_precio.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el precio del auto para guardar", true, true);
                return false;
            }

            if (!Int32.TryParse(TextBox_precio.Text, out int num))
            {
                MensajeEstado("Debe introducir un precio valido", true, true);
                return false;
            }

            return true;
        }

        private bool ValidarModificar()
        {
            if (DropDownList_marca.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar una marca", true, true);
                return false;
            }

            if (DropDownList_modelo.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un modelo", true, true);
                return false;
            }

            if (DropDownList_combustible.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un combustible", true, true);
                return false;
            }

            if (DropDownList_sucursal.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar una sucursal", true, true);
                return false;
            }

            if (TextBox_cantidad.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la cantidad de autos en posesión", true, true);
                return false;
            }

            if (!Int32.TryParse(TextBox_cantidad.Text, out int num1))
            {
                MensajeEstado("Debe introducir una cantidad de autos valida", true, true);
                return false;
            }

            if (TextBox_precio.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el precio del auto", true, true);
                return false;
            }

            if (!Int32.TryParse(TextBox_precio.Text, out int num))
            {
                MensajeEstado("Debe introducir un precio valido", true, true);
                return false;
            }

            if (!Int32.TryParse(TextBox_codigo.Text, out int num2))
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
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
    public partial class Renta : System.Web.UI.Page
    {
        IEnumerable<Models.Renta> autos = new ObservableCollection<Models.Renta>();
        RentaManager rentaManager = new RentaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarDropdownAuto();
                RellenarDropdownEmpleado();
            }
            InicializarControles();
        }

        private async void RellenarDropdownAuto()
        {
            DropDownList_auto.Items.Clear();

            // Trae la lista de autos disponibles de la base de datos.
            IEnumerable<Models.Autos> iEnumerable_autos = new ObservableCollection<Models.Autos>();
            AutosManager autosManager = new AutosManager();
            iEnumerable_autos = await autosManager.ObtenerAutos(VG.usuarioActual.CadenaToken);
            List<Models.Autos> lista_autos = iEnumerable_autos.ToList();

            if (lista_autos.Count > 0)
            { // Rellena el dropdown con los autos disponibles.
                lista_autos.Reverse();
                foreach (var auto in lista_autos)
                {
                    string autoInfo = "Auto código: " + auto.AUTO_CODIGO;
                    DropDownList_auto.Items
                        .Insert(0, new ListItem(autoInfo, "" + auto.AUTO_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un auto.
            DropDownList_auto.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void RellenarDropdownEmpleado()
        {
            DropDownList_empleado.Items.Clear();

            // Trae la lista de empleados disponibles de la base de datos.
            IEnumerable<Models.Empleado> IEnumerable_empleados = new ObservableCollection<Models.Empleado>();
            EmpleadoManager empleadoManager = new EmpleadoManager();
            IEnumerable_empleados = await empleadoManager.ObtenerEmpleados(VG.usuarioActual.CadenaToken);
            List<Models.Empleado> lista_empleados = IEnumerable_empleados.ToList();

            if (lista_empleados.Count > 0)
            { // Rellena el dropdown con los empleados disponibles.
                lista_empleados.Reverse();
                foreach (var empleado in lista_empleados)
                {
                    string empleadoInfo =
                        empleado.EMP_APELLIDO1 + " " +
                        empleado.EMP_APELLIDO2 + ", " +
                        empleado.EMP_NOMBRE;

                    DropDownList_empleado.Items.Insert(0,
                       new ListItem(empleadoInfo, "" + empleado.EMP_CODIGO));
                }
            }

            // Coloca la opcion "Seleccionar" al inicio del dropdown para obligar al usuario a seleccionar
            // un empleado.
            DropDownList_empleado.Items.Insert(0, new ListItem("Seleccionar", "seleccionar"));
        }

        private async void InicializarControles()
        {
            try
            {
                autos = await rentaManager.ObtenerRentas(VG.usuarioActual.CadenaToken);
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
                try
                {
                    Models.Renta rentaAgregada = new Models.Renta();
                    Models.Renta renta = new Models.Renta()
                    {
                        USU_IDENTIFICACION = VG.usuarioActual.USU_IDENTIFICACION,
                        AUTO_CODIGO = Int32.Parse(DropDownList_auto.SelectedValue.ToString()),
                        EMP_CODIGO = Int32.Parse(DropDownList_empleado.SelectedValue.ToString()),
                        REN_DESCRIPCION = TextBox_descripcion.Text,
                        REN_CANTIDAD = Int32.Parse(TextBox_cantidadRenta.Text),
                        REN_PRECIO = Int32.Parse(TextBox_precio.Text),
                        REN_FEC_RETIRO = Calendar_FechaRetiro.SelectedDate,
                        REN_FEC_RETORNO = Calendar_FechaRetorno.SelectedDate
                    };

                    rentaAgregada =
                        await rentaManager.Ingresar(renta, VG.usuarioActual.CadenaToken);

                    if (rentaAgregada != null)
                    {
                        MensajeEstado("Registro guardado con exito", false, true);
                        InicializarControles();
                    }
                    else
                    {
                        MensajeEstado("Ha habido un error al guardar el registro", true, true);
                    }


                }
                catch (OverflowException)
                {
                    MensajeEstado("No puede establecer un precio o cantidad excesiva", true, true);
                }
            }
        }

        protected async void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                try
                {
                    Models.Renta rentaModificada = new Models.Renta();
                    Models.Renta renta = new Models.Renta()
                    {
                        REN_CODIGO = Int32.Parse(TextBox_codigo.Text),
                        USU_IDENTIFICACION = VG.usuarioActual.USU_IDENTIFICACION,
                        AUTO_CODIGO = Int32.Parse(DropDownList_auto.SelectedValue.ToString()),
                        EMP_CODIGO = Int32.Parse(DropDownList_empleado.SelectedValue.ToString()),
                        REN_DESCRIPCION = TextBox_descripcion.Text,
                        REN_CANTIDAD = Int32.Parse(TextBox_cantidadRenta.Text),
                        REN_PRECIO = Int32.Parse(TextBox_precio.Text),
                        REN_FEC_RETIRO = Calendar_FechaRetiro.SelectedDate,
                        REN_FEC_RETORNO = Calendar_FechaRetorno.SelectedDate
                    };

                    rentaModificada =
                        await rentaManager.Actualizar(renta, VG.usuarioActual.CadenaToken);

                    if (rentaModificada != null)
                    {
                        MensajeEstado("Registro modificado con exito", false, true);
                        InicializarControles();
                    }
                    else
                    {
                        MensajeEstado("Hubo un error al intentar modificar el registro", true, true);
                    }
                }
                catch (OverflowException)
                {
                    MensajeEstado("No puede establecer un precio o cantidad excesiva", true, true);
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
                    await rentaManager.Eliminar(codigoElemento, VG.usuarioActual.CadenaToken);

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
            if (DropDownList_auto.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un auto", true, true);
                return false;
            }

            if (DropDownList_empleado.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un empleado", true, true);
                return false;
            }

            if (TextBox_descripcion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar una descripción a al renta", true, true);
                return false;
            }

            if (TextBox_cantidadRenta.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la cantidad de vehiculos a rentar", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_cantidadRenta.Text))
            {
                MensajeEstado("Debe introducir una cantidad de renta valida", true, true);
                return false;
            }

            if (TextBox_precio.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el precio de la renta", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_precio.Text))
            {
                MensajeEstado("Debe introducir la cantidad del precio de forma valida", true, true);
                return false;
            }

            if (Calendar_FechaRetiro.SelectedDate.Date == DateTime.MinValue)
            {
                MensajeEstado("Debe ingresar una fecha de retiro del auto.", true, true);
                return false;
            }

            if (Calendar_FechaRetorno.SelectedDate.Date == DateTime.MinValue)
            {
                MensajeEstado("Debe ingresar una fecha de retorno del auto.", true, true);
                return false;
            }

            if (!ValidarFecha_DespuesDeFechaActual())
                return false;

            if (!ValidarFecha_SalidaAntesDeEntrada())
                return false;

            return true;
        }

        private bool ValidarModificar()
        {
            if (DropDownList_auto.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un auto", true, true);
                return false;
            }

            if (DropDownList_empleado.SelectedValue.ToString().Equals("seleccionar"))
            {
                MensajeEstado("Debe seleccionar un empleado", true, true);
                return false;
            }

            if (TextBox_descripcion.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar una descripción a al renta", true, true);
                return false;
            }

            if (TextBox_cantidadRenta.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar la cantidad de vehiculos a rentar", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_cantidadRenta.Text))
            {
                MensajeEstado("Debe introducir una cantidad de renta valida", true, true);
                return false;
            }

            if (TextBox_precio.Text.IsNullOrWhiteSpace())
            {
                MensajeEstado("Debe ingresar el precio de la renta", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_precio.Text))
            {
                MensajeEstado("Debe introducir la cantidad del precio de forma valida", true, true);
                return false;
            }

            if (Calendar_FechaRetiro.SelectedDate.Date == DateTime.MinValue)
            {
                MensajeEstado("Debe ingresar una fecha de retiro del auto.", true, true);
                return false;
            }

            if (Calendar_FechaRetorno.SelectedDate.Date == DateTime.MinValue)
            {
                MensajeEstado("Debe ingresar una fecha de retorno del auto.", true, true);
                return false;
            }

            if (!VG.CadenaSoloNumeros(TextBox_codigo.Text))
            {
                MensajeEstado("Debe ingresar un código valido", true, true);
                return false;
            }

            return true;
        }

        // Valida si la fecha de entrada y salida no es antes de la fecha actual.
        private bool ValidarFecha_DespuesDeFechaActual()
        {
            DateTime FechaRetiro = Calendar_FechaRetiro.SelectedDate;
            DateTime FechaRetorno = Calendar_FechaRetorno.SelectedDate;
            if (FechaRetiro >= ObtenerFechaAyer() &&
                FechaRetorno >= ObtenerFechaAyer())
            {
                return true;
            }
            MensajeEstado("No puede establecer una fecha antes de "
                + ObtenerFechaActual().ToString(), true, true);
            return false;
        }

        // Valida si la fecha de salida es despúes o el mismo día a la fecha de entrada.
        private bool ValidarFecha_SalidaAntesDeEntrada()
        {
            DateTime FechaRetiro = Calendar_FechaRetiro.SelectedDate;
            DateTime FechaRetorno = Calendar_FechaRetorno.SelectedDate;

            if (FechaRetorno >= FechaRetiro)
            {
                return true;
            }
            MensajeEstado("La fecha de retiro no puede ser antes de la fecha de retorno.", true, true);
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
            Label_status.Text = mensaje;
            if (error)
                Label_status.ForeColor = Color.Maroon;
            else
                Label_status.ForeColor = Color.DarkGreen;
            Label_status.Visible = visible;
        }
    }
}
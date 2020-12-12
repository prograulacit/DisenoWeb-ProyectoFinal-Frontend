using System;
using AppReservasSW.Models;
using AppReservasSW.Controllers;
namespace AppReservasSW
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtFechaNac_TextChanged(object sender, EventArgs e)
        {
           
        }

        async protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario()
            {
                USU_IDENTIFICACION = Identificacion.Text,
                USU_NOMBRE = Nombre.Text,
                USU_PASSWORD = Password.Text,
                USU_EMAIL = Email.Text,
                USU_FECHA_NACIMIENTO = DateTime.Today,
                USU_TELEFONO="8888-8888",
                USU_ESTADO = "A"
            };

            var usuarioManager = new UsuarioManager();

            Usuario usuarioRegistrado = await usuarioManager.Registrar(usuario);

            if (!string.IsNullOrEmpty(usuarioRegistrado.USU_NOMBRE))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                FailureText.Text = "Número de identificación del cliente no existe. " +
                    "Debe crear un registro de cliente en la base de datos.";
                ErrorMessage.Visible = true;
            }
        }
    }
}
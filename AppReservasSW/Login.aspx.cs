using System;
using AppReservasSW.Controllers;
using AppReservasSW.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Security;
namespace AppReservasSW
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioManager usuarioManager = new UsuarioManager();
                Usuario usuario = await usuarioManager.Validar(Identificacion.Text, Password.Text);
                JwtSecurityToken securityToken;
                if (usuario != null)
                {
                    if (!string.IsNullOrEmpty(usuario.CadenaToken))
                    {
                        var jwtHandler = new JwtSecurityTokenHandler();
                        securityToken = jwtHandler.ReadJwtToken(usuario.CadenaToken);
                        FormsAuthentication.RedirectFromLoginPage(
                            Identificacion.Text, true);

                        VG.usuarioActual = usuario;
                    }
                    else
                    {
                        FailureText.Text = "Credenciales inválidas";
                        FailureText.Visible = true;
                    }
                }
                else
                {
                    FailureText.Text = "El usuario no existe";
                    FailureText.Visible = true;
                }
            }
            catch (Exception)
            {
                FailureText.Text = "El usuario no existe";
                FailureText.Visible = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;


namespace TechStoreWeb
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Users usuario = new Users();
                UsersNegocio usuarioNegocio = new UsersNegocio();
                EmailService emailService = new EmailService();

                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                usuario.Id = usuarioNegocio.insertarNuevo(usuario);
                Session.Add("Users", usuario);

                emailService.armarCorreo(usuario.Email, "Bienvenido Usuario", "Bienvenido a TechStore Web");
                emailService.enviarEmail();
                Response.Redirect("Home.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
            }
        }
    }
}
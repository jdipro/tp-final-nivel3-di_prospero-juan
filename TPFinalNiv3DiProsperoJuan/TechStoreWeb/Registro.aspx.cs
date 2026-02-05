using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Mail;


namespace TechStoreWeb
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            //Validación para evitar campos txtEmail y txtPassword vacíos.

            if (!Validacion.validaTextoVacio(txtEmail) || !Validacion.validaTextoVacio(txtPassword)) //validación para evitar campos vacíos a través de clase Validacion.
            {
                    Session.Add("error", "Los campos Email y Password deben estar completos para seguir.");
                    Response.Redirect("Error.aspx");
                return;
            }
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
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
            }
        }
    }
}
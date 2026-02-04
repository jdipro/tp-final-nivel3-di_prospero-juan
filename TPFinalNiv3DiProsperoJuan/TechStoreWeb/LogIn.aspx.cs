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
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Users usuario = new Users();
            UsersNegocio negocio = new UsersNegocio();

            try
            {

                if(Validacion.validaTextoVacio(txtEmail) || Validacion.validaTextoVacio(txtPassword)) //validación para evitar campos vacíos a través de clase Validacion.
                {
                    Session.Add("error", "Los campos Email y Password deben estar completos para seguir.");
                    Response.Redirect("Error.aspx");
                }
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                if (negocio.LogIn(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("MiPerfil.aspx", false);
                }
                else
                {
                    Session.Add("error", "User o Pass incorrectos");
                    Response.Redirect("Error.aspx", false);
                }



            }
            catch (System.Threading.ThreadAbortException ex) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();


            Session.Add("error", exc.ToString());
            //Response.Redirect("Error.aspx");
            Server.Transfer("Error.aspx");
        }
    }
}
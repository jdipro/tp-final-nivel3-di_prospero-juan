using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TechStoreWeb
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";
            if (!(Page is LogIn || Page is Registro || Page is Home || Page is Contacto || Page is Error))
            {
                //Para que accedan sólo los perfiles logeados:
                if (!Seguridad.sesionActiva(Session["usuario"]))
                    Response.Redirect("LogIn.aspx", false);
                else
                {
                    Users usuario = (Users)Session["usuario"];
                    lblUser.Text = usuario.Email;
                    if (!string.IsNullOrEmpty(usuario.urlImagenPerfil))
                        imgAvatar.ImageUrl = "~/Images/" + usuario.urlImagenPerfil;
                }

            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LogIn.aspx");

        }
    }
}
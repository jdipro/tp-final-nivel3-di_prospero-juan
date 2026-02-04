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
            //Este código fue hecho junto a chatgpt para poder visualizar el email
            //y la foto del usuario logueado en todas las páginas.
            //De la forma original perdía la visualización en las pág abiertas a todo público.

            imgAvatar.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";


            bool logueado = Seguridad.sesionActiva(Session["usuario"]);

            // NAVBAR (siempre)
            if (logueado)
            {
                Users usuario = (Users)Session["usuario"];
                lblUser.Text = usuario.Email;

                if (!string.IsNullOrEmpty(usuario.urlImagenPerfil))
                    imgAvatar.ImageUrl = "~/Images/" + usuario.urlImagenPerfil;


            }

            // AUTORIZACIÓN
            if (!logueado && PageRequiereLogin())
            {
                Response.Redirect("LogIn.aspx", false);
            }
        }

        private bool PageRequiereLogin()
        {
            return !(Page is LogIn ||
                     Page is Registro ||
                     Page is Home ||
                     Page is Contacto ||
                     Page is Error);
        }

        //Codigo original en base a la cursada:
        // {
        //    protected void Page_Load(object sender, EventArgs e)
        //    {
        //        imgAvatar.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";
        //        if (!(Page is LogIn || Page is Registro || Page is Home || Page is Contacto || Page is Error))
        //        {
        //            //Para que accedan sólo los perfiles logeados:
        //            if (!Seguridad.sesionActiva(Session["usuario"]))
        //                Response.Redirect("LogIn.aspx", false);
        //            else
        //            {
        //                Users usuario = (Users)Session["usuario"];
        //                lblUser.Text = usuario.Email;
        //                if (!string.IsNullOrEmpty(usuario.urlImagenPerfil))
        //                    imgAvatar.ImageUrl = "~/Images/" + usuario.urlImagenPerfil;
        //            }

        //        }
        //   }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LogIn.aspx");

        }
    }
}
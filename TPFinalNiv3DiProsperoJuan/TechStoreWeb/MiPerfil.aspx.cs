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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["usuario"]))
                    {
                        Users usuario = (Users)Session["usuario"];
                        txtEmail.Text = usuario.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;                       
                        if (!string.IsNullOrEmpty(usuario.urlImagenPerfil))
                            imgNuevoPerfil.ImageUrl = "~/Images/" + usuario.urlImagenPerfil;
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
            }


        }

        //Lógica para guardar imgen de perfil cargada por el usuario.
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                UsersNegocio negocio = new UsersNegocio();
                Users usuario = (Users)Session["usuario"];
                //escribe img si se cargó algo.
                if (txtImagen.PostedFile.FileName != "")
                {
                string ruta = Server.MapPath("./Images/");              
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");
                usuario.urlImagenPerfil = "perfil-" + usuario.Id + ".jpg";
                }
                
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;

                //Guardar datos perfil
                negocio.actualizar(usuario);
               
                //Leer imagen
                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Images/" + usuario.urlImagenPerfil;
            }
            catch (Exception ex)
            {

                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
            }
        }
    }
}
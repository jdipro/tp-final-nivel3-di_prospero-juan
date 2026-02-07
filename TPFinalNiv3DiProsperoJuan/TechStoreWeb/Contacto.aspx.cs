using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mail;

namespace TechStoreWeb
{
    public partial class Contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            EmailService emailService = new EmailService(); //acá se crea el objeto EmailService() (mail) con los datos que puse en el constructor del método en clase Negocio.
            emailService.armarCorreo(txtEmail.Text, txtAsunto.Text, txtMensaje.Text); //En el constructor del método se pueden poner más parámetros.
            try
            {
                emailService.enviarEmail();
                // Mostrar cartel de éxito.
                lblMensaje.Text = "El mensaje fue enviado correctamente.";
                lblMensaje.Visible = true;

                // Deshabilitar botón aceptar.
                btnAceptar.Enabled = false;

                // Limpiar campos.
                txtEmail.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtApellido.Text = string.Empty;
                txtAsunto.Text = string.Empty;
                txtMensaje.Text = string.Empty;

                
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        



    }
}
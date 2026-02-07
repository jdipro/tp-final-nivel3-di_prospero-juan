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
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                //Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                CargarFavoritos();
            }



        }

        //Lógica para agregar un artículo a favoritos.
        private void CargarFavoritos()
        {
            List<int> idsFavoritos = Session["Favoritos"] as List<int>;

            if (idsFavoritos == null || idsFavoritos.Count == 0)
            {
                repFavoritos.DataSource = null; //repFavoritos es el repetidor en el front.
                repFavoritos.DataBind();
                lblVacio.Visible = true;
                return;
            }

            ArticuloNegocio negocio = new ArticuloNegocio();

            // Método que trae artículos por lista de IDs
            List<Articulo> lista = negocio.ListarPorIds(idsFavoritos);

            repFavoritos.DataSource = lista;
            repFavoritos.DataBind();
            lblVacio.Visible = false;
        }

        //Lógica para quitar el artículo favorito desde favoritos.
        protected void btnQuitarFavorito_Command(object sender, CommandEventArgs e)
        {
            int idArticulo = int.Parse(e.CommandArgument.ToString());

            List<int> favoritos = Session["Favoritos"] as List<int>;

            if (favoritos != null) //&& favoritos.Contains(idArticulo)
            {
                favoritos.Remove(idArticulo);
                //Session["Favoritos"] = favoritos;
            }

            // Actualiza la lista.
            CargarFavoritos();

            //UpdatePanel.
            updFavoritos.Update();
        }
    }
}
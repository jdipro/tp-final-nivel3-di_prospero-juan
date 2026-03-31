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
            try
            {
                Users usuario = (Users)Session["Usuario"];

                FavoritoNegocio favNegocio = new FavoritoNegocio();
                List<int> ids = favNegocio.listarIdsPorUsuario(usuario.Id); // Traigo los Ids. Mi confución que terminó en un gran mareo fue principalmente pensar que tenía que guardar los artículos, no los Ids de los artículos. Claramente me olvidé del concepto de relaciones.

                if (ids == null || ids.Count == 0)
                {
                    repFavoritos.DataSource = null;
                    repFavoritos.DataBind();
                    lblVacio.Visible = true;
                    return;
                }

                ArticuloNegocio artNegocio = new ArticuloNegocio();
                List<Articulo> lista = artNegocio.ListarPorIds(ids); // Acá traigo los artículos asociados al Id.

                repFavoritos.DataSource = lista;
                repFavoritos.DataBind();
                lblVacio.Visible = false;
            }
            catch (Exception ex)
            {
                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
            }
        }



        //Lógica para quitar el artículo favorito desde favoritos.


        protected void btnQuitarFavorito_Command(object sender, CommandEventArgs e)
        {
            int idArticulo = int.Parse(e.CommandArgument.ToString());
            Users usuario = (Users)Session["Usuario"];

            FavoritoNegocio negocio = new FavoritoNegocio();
            negocio.toggleFavorito(usuario.Id, idArticulo); // elimina

            // recargar desde DB
            CargarFavoritos();

            updFavoritos.Update();
        }



        //Lógica para quitar artículo de favoritos desde la session del navegador - no db.

        //protected void btnQuitarFavorito_Command(object sender, CommandEventArgs e)
        //{
        //    int idArticulo = int.Parse(e.CommandArgument.ToString());

        //    List<int> favoritos = Session["Favoritos"] as List<int>;

        //    if (favoritos != null) //&& favoritos.Contains(idArticulo)
        //    {
        //        favoritos.Remove(idArticulo);
        //        //Session["Favoritos"] = favoritos;
        //    }

        //    // Actualiza la lista.
        //    CargarFavoritos();

        //    //UpdatePanel.
        //    updFavoritos.Update();
        //}



        // Comento aquí, el cod original para guardar en session (no en DB) hecho por chatgpt.
        // private void CargarFavoritos()
        //{
        //    try
        //    {
        //    Favorito favoritos = new Favorito();
        //    FavoritoNegocio favoritosNegocio = new FavoritoNegocio();

        //        favoritos.Id = Session["Id"].Int;
        //     //   usuario.Id = usuarioNegocio.insertarNuevo(usuario);


        //        Session.Add("Favorito", favoritos);

        //    }
        //    catch (System.Threading.ThreadAbortException) { }
        //    catch (Exception ex)
        //    {
        //        Session.Add("error", Seguridad.manejoError(ex));
        //        Response.Redirect("error.aspx");
        //    }          
        //}


        //private void CargarFavoritos()
        //{
        //    List<int> idsFavoritos = Session["Favoritos"] as List<int>;

        //    if (idsFavoritos == null || idsFavoritos.Count == 0)
        //    {
        //        repFavoritos.DataSource = null; //repFavoritos es el repetidor en el front.
        //        repFavoritos.DataBind();
        //        lblVacio.Visible = true;
        //        return;
        //    }

        //    ArticuloNegocio negocio = new ArticuloNegocio();

        //    // Método que trae artículos por lista de IDs
        //    List<Articulo> lista = negocio.ListarPorIds(idsFavoritos);

        //    repFavoritos.DataSource = lista;
        //    repFavoritos.DataBind();
        //    lblVacio.Visible = false;
        //}

    }
}
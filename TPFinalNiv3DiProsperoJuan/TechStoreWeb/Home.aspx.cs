using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Web.UI.HtmlControls; //agregado para usar HtmlGenericControl

namespace TechStoreWeb
{
    public partial class Home : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }

        List<int> favoritosIds;         //agregado para poder actualizar el botón de favoritos según la condición de un artíiculo en la db.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Lógica para que siempre cargue favoritos. Favoritos ya tiene los datos. Al cargar la pág, va primero allí. Así el "corazón" carga y descarga tanto en Favoritos.aspx como en Home.aspx.
            if (Session["Usuario"] != null)
            {
                Users usuario = (Users)Session["Usuario"];

                FavoritoNegocio favNegocio = new FavoritoNegocio();  
                favoritosIds = favNegocio.listarIdsPorUsuario(usuario.Id);
            }

            //Lo de abajo sólo ocurrirá en la primer carga de la pág. o si no hay un usuario.
            if (!IsPostBack)
            {
                //configuracón del repetidor.
                ArticuloNegocio negocio = new ArticuloNegocio();
                ListaArticulo = negocio.ListarConSP();

                //configuración DGV.
                repRepetidor.DataSource = ListaArticulo;
                repRepetidor.DataBind(); 
            }

            

        }

        protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int idArticulo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id"));

                var iconEmpty = (HtmlGenericControl)e.Item.FindControl("iconHeartEmpty");
                var iconFill = (HtmlGenericControl)e.Item.FindControl("iconHeartFill");

                bool esFavorito = favoritosIds != null && favoritosIds.Contains(idArticulo);

                iconFill.Visible = esFavorito;
                iconEmpty.Visible = !esFavorito;
            }
        }

        //Lógica para el botón favorito guardando en DB:
        protected void btnFavorito_Command(object sender, CommandEventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            Users usuario = (Users)Session["Usuario"];
            int idArticulo = int.Parse(e.CommandArgument.ToString());

            FavoritoNegocio negocio = new FavoritoNegocio();
            negocio.toggleFavorito(usuario.Id, idArticulo);

            // mantener esto (para UI)
            LinkButton btn = (LinkButton)sender;

            var iconEmpty = (HtmlGenericControl)btn.FindControl("iconHeartEmpty");
            var iconFill = (HtmlGenericControl)btn.FindControl("iconHeartFill");

            bool esFavorito = iconEmpty.Visible;

            iconEmpty.Visible = !esFavorito;
            iconFill.Visible = esFavorito;

            FavoritoNegocio favNegocio = new FavoritoNegocio();
            favoritosIds = favNegocio.listarIdsPorUsuario(usuario.Id);
        }


        //Lógica para el botón de favorito guardado en Session.


        //protected void btnFavorito_Command(object sender, CommandEventArgs e)
        //{
        //    //Validación sólo para la acción de tocar el botón de favorito.

        //    if (Session["Usuario"] == null)
        //    {
        //        Response.Redirect("Login.aspx", false);
        //        Context.ApplicationInstance.CompleteRequest();
        //        return;
        //    }


        //    int idArticulo = int.Parse(e.CommandArgument.ToString());

        //    // Inicializa la lista si no existe.
        //    if (Session["Favoritos"] == null)
        //        Session["Favoritos"] = new List<int>();

        //    List<int> favoritos = (List<int>)Session["Favoritos"];

        //    bool ahoraEsFavorito;

        //    // Evitar duplicados,  agrega nuevos y quita seleccionado.
        //    if (favoritos.Contains(idArticulo))
        //    {
        //        // Quitar favorito
        //        favoritos.Remove(idArticulo);
        //        ahoraEsFavorito = false;
        //    }
        //    else
        //    {
        //        // Agregar favorito
        //        favoritos.Add(idArticulo);
        //        ahoraEsFavorito = true;
        //    }

        //    LinkButton btn = (LinkButton)sender;

        //    var iconEmpty = (HtmlGenericControl)btn.FindControl("iconHeartEmpty");
        //    var iconFill = (HtmlGenericControl)btn.FindControl("iconHeartFill");

        //    iconEmpty.Visible = !ahoraEsFavorito;
        //    iconFill.Visible = ahoraEsFavorito;



        //}
        
        // Rep repetidor para guardado en session del navegador.

        //protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item ||
        //        e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        int idArticulo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id"));

        //        var iconEmpty = (HtmlGenericControl)e.Item.FindControl("iconHeartEmpty");
        //        var iconFill = (HtmlGenericControl)e.Item.FindControl("iconHeartFill");

        //        List<int> favoritos = Session["Favoritos"] as List<int>;

        //        bool esFavorito = favoritos != null && favoritos.Contains(idArticulo);

        //        iconFill.Visible = esFavorito;
        //        iconEmpty.Visible = !esFavorito;
        //    }
        //}
    }
}
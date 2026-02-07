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
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            //Lo de abajo sólo ocurrirá en la primer carga de la pág.
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

        //Lógica para el botón de favorito.
        protected void btnFavorito_Command(object sender, CommandEventArgs e)
        {
            //Validación sólo para la acción de tocar el botón de favorito.

            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }


            int idArticulo = int.Parse(e.CommandArgument.ToString());

            // Inicializa la lista si no existe.
            if (Session["Favoritos"] == null)
                Session["Favoritos"] = new List<int>();

            List<int> favoritos = (List<int>)Session["Favoritos"];

            bool ahoraEsFavorito;

            // Evitar duplicados,  agrega nuevos y quita seleccionado.
            if (favoritos.Contains(idArticulo))
            {
                // Quitar favorito
                favoritos.Remove(idArticulo);
                ahoraEsFavorito = false;
            }
            else
            {
                // Agregar favorito
                favoritos.Add(idArticulo);
                ahoraEsFavorito = true;
            }

            LinkButton btn = (LinkButton)sender;

            var iconEmpty = (HtmlGenericControl)btn.FindControl("iconHeartEmpty");
            var iconFill = (HtmlGenericControl)btn.FindControl("iconHeartFill");

            iconEmpty.Visible = !ahoraEsFavorito;
            iconFill.Visible = ahoraEsFavorito;



        }

        protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int idArticulo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id"));

                var iconEmpty = (HtmlGenericControl)e.Item.FindControl("iconHeartEmpty");
                var iconFill = (HtmlGenericControl)e.Item.FindControl("iconHeartFill");

                List<int> favoritos = Session["Favoritos"] as List<int>;

                bool esFavorito = favoritos != null && favoritos.Contains(idArticulo);

                iconFill.Visible = esFavorito;
                iconEmpty.Visible = !esFavorito;
            }
        }
    }
}
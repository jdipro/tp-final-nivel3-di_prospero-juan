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
    public partial class ArticulosLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           ArticuloNegocio negocio = new ArticuloNegocio();
           dgvArticulos.DataSource = negocio.ListarConSP();
           dgvArticulos.DataBind();                        //enlace de datos web a db
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e) //Captura el Id del artículo seleccionado.
        { 
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulos.aspx" + id);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e) //Paginado a partir del EventArgs e
         {
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataBind();
        }
    }
}
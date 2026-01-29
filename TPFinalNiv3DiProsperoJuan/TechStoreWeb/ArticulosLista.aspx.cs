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
        public bool FiltroAvanzado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                Session.Add("error", "Se requiere permisos de admin para acceder a esta pantalla");
                Response.Redirect("Error.aspx");
            }
            
            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session["listaArticulos"] = negocio.ListarConSP(); //Session.Add("listaArticulos", negocio.ListarConSP());
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();                             //enlace de datos web a db
            }



        }

        

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e) //Paginado a partir del EventArgs e
         {
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.DataBind();
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e) //Captura el Id del artículo seleccionado.
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulos.aspx?id=" + id);
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtfiltro.Text.ToUpper()));
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtfiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if(ddlCampo.SelectedItem.ToString() == "Clasificación")
            {
                ddlCriterio.Items.Add("Celulares");
                ddlCriterio.Items.Add("Televisores");
                ddlCriterio.Items.Add("Media");
                ddlCriterio.Items.Add("Audio");
            }
            else
            {
                ddlCriterio.Items.Add("Samsung");
                ddlCriterio.Items.Add("Apple");
                ddlCriterio.Items.Add("Sony");
                ddlCriterio.Items.Add("Huawei");
                ddlCriterio.Items.Add("Motorola");
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(), 
                    txtFiltroAvanzado.Text);
                dgvArticulos.DataBind();

            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnBorrarFiltro_Click(object sender, EventArgs e)
        {
            txtfiltro.Text = string.Empty;

            // Volver a cargar la grilla sin filtro

            
            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.DataBind();


        }
    }
}
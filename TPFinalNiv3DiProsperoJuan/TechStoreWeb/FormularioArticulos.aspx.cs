using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using System.Globalization;


namespace TechStoreWeb
{
    public partial class FormularioArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            try
            {
                if (!IsPostBack) //traigo elementos de CategoriaNegocio para cargar los desplegables.
                {
                    //Categoria
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    List<Categoria> categoria = categoriaNegocio.listar();

                    ddlCategoria.DataSource = categoria;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    //Marca
                    MarcaNegocio marcaNegocio = new MarcaNegocio();
                    List<Marca> marca = marcaNegocio.listar();

                    ddlMarca.DataSource = marca;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                throw;
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                //Resolución del problema con la coma en el tipo decimal/money:
                //nuevo.Precio = decimal.Parse(txtPrecio.Text, CultureInfo.InvariantCulture);

                //Precio
                decimal precio;

                if (!decimal.TryParse(
                    txtPrecio.Text,
                    NumberStyles.Number,
                    new CultureInfo("es-AR"),
                    out precio))
             
                {
                    txtPrecio.Text = "El formato de precio incorrecto. Ejemplo: xx.xxx,xx";
                    return;
                }

                nuevo.Precio = precio;

                //Formato:
                

                //Imagen
                nuevo.ImagenUrl = txtImagenUrl.Text;

                //Desplegbles:
                nuevo.Empresa = new Marca();
                nuevo.Empresa.Id = int.Parse(ddlMarca.SelectedValue);

                nuevo.Clasificacion = new Categoria();
                nuevo.Clasificacion.Id = int.Parse(ddlCategoria.SelectedValue);
                
                //Guardar nuevo artículo y redirección:
                negocio.AgregarConSP(nuevo);
                Response.Redirect("ArticulosLista.aspx", false); 



            }

            catch (Exception ex)
            {

                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {

        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtImagenUrl.Text;
        }
    }
}
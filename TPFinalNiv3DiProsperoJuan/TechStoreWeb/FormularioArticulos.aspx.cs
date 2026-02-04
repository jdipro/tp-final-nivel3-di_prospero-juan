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
        public bool ConfirmaEliminacion { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;

            try
            {   //Configuración Inicial.
                if (!IsPostBack) //traigo elementos de CategoriaNegocio para cargar los desplegables.
                {
                    //Categoria - Desplegable.
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    List<Categoria> categoria = categoriaNegocio.listar();

                    ddlCategoria.DataSource = categoria;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    //Marca - Desplegable.
                    MarcaNegocio marcaNegocio = new MarcaNegocio();
                    List<Marca> marca = marcaNegocio.listar();

                    ddlMarca.DataSource = marca;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    //Configuración para MODIFICAR un articulo.
                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                    if (id != "" && !IsPostBack)
                    {
                        ArticuloNegocio negocio = new ArticuloNegocio();
                        //List<Articulo> lista = negocio.listar(id);
                        // Articulo seleccionado = lista[0];
                        Articulo seleccionado = (negocio.listar(id))[0];

                        //pre carga todos los campos para poder modificar.
                        txtId.Text = id;
                        txtCodigo.Text = seleccionado.Codigo;
                        txtNombre.Text = seleccionado.Nombre;
                        txtDescripcion.Text = seleccionado.Descripcion;
                        txtImagenUrl.Text = seleccionado.ImagenUrl;

                        //Precio
                        txtPrecio.Text = seleccionado.Precio.ToString(
                            "N2", new CultureInfo("es-AR"));

                        //Desplegables.
                        ddlCategoria.SelectedValue = seleccionado.Clasificacion.Id.ToString();
                        ddlMarca.SelectedValue = seleccionado.Empresa.Id.ToString();
                        //Forzado de carga de imagen.
                        txtImagenUrl_TextChanged(sender, e);
                    }



                }
            }
            catch (Exception ex)
            {

                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
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

                //Desplegbles:
                nuevo.Empresa = new Marca();
                nuevo.Empresa.Id = int.Parse(ddlMarca.SelectedValue);

                nuevo.Clasificacion = new Categoria();
                nuevo.Clasificacion.Id = int.Parse(ddlCategoria.SelectedValue);
                
                //ImagenURl
                nuevo.ImagenUrl =  txtImagenUrl.Text;

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

            

                //Guardar nuevo artículo + Modificación de Artículo:
                if (Request.QueryString["id"] != null) //si no está null es que el pedido es para modificar.
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"]); //int.Parse(txtId.Text)
                    negocio.ModificarConSP(nuevo);
                }
                else
                    negocio.AgregarConSP(nuevo);

                //Redireccionamiento.
                Response.Redirect("ArticulosLista.aspx", false); 



            }

            catch (Exception ex)
            {

                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
            }
        }

        

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {  
                imgArticulo.ImageUrl = txtImagenUrl.Text; 
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.Eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ArticulosLista.aspx");

                }

            }
            catch (Exception ex)
            {

                Session.Add("error", Seguridad.manejoError(ex));
                Response.Redirect("error.aspx");
            }
        }
    }
}
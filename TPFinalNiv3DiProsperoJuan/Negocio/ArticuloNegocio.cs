
using System;
using System.Collections.Generic;
using System.Data.SqlClient; //hacer uso de funcionalidades relacionadas con la red (por ejemplo, descargando datos desde una API, enviando correos electrónicos, o similar). 
using Dominio;


namespace Negocio
{
    public class ArticuloNegocio
    {

        //Lógica para conectar a la DB y Leer artículos..
        public List<Articulo> listar()  //preparo conexion a la db.
        {   
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Id AS IdMarca, M.Descripcion AS Empresa, C.Id AS IdCategoria, C.Descripcion AS Clasificacion, A.ImagenUrl, A.Precio FROM ARTICULOS A JOIN CATEGORIAS C ON A.IdCategoria = C.Id JOIN MARCAS M ON  A.IdMarca = M.Id";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader(); //realizo un lectura de esos datos.

                while (lector.Read())
                {
                    
                    //Para leerlos hago un while. El .Read() se fijará si hay lectura. De ser así, posicionará el puntero en la primera fila y devuelve true.
                    //Luego bajará al otro y así hasta q no haya más. Cada vez q baje reutilizará la variable. Creará una nueva var en el stack de objetos.

                    Articulo aux = new Articulo(); //Genero objeto Articulo auxiliar y lo empiezo a cargar con los datos del lector de ese registro.
                    aux.Id = (int)lector["Id"];      //lector.GetInt32(0);
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    aux.Empresa= new Marca();
                    aux.Empresa.Id = (int)lector["IdMarca"];
                    aux.Empresa.Descripcion = (string)lector["Empresa"];

                    aux.Clasificacion = new Categoria();
                    aux.Clasificacion.Id = (int)lector["IdCategoria"];
                    aux.Clasificacion.Descripcion = (string)lector["Clasificacion"];

                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)lector["ImagenUrl"];

                    aux.Precio = lector["Precio"] != DBNull.Value ? (decimal)lector["Precio"] : 0m;
                    

                    lista.Add(aux);
                }

                conexion.Close(); 
                return lista; 

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los Artículos", ex);
            }

        }

        //Lógica para utilizar Stored Procedures.
        public List<Articulo> ListarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //string consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Empresa, C.Descripcion Clasificacion, A.ImagenUrl, A.Precio from ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdCategoria = C.Id And A.IdMarca = M.Id";

                //datos.setearConsulta(consulta);

                datos.setearProcedimiento("storedListar");
                
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];      //lector.GetInt32(0);
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Empresa = new Marca();
                    aux.Empresa.Descripcion = (string)datos.Lector["Empresa"];
                    aux.Clasificacion = new Categoria();
                    aux.Clasificacion.Descripcion = (string)datos.Lector["Clasificacion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Precio")) ? 0m : datos.Lector.GetDecimal(datos.Lector.GetOrdinal("Precio"));

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ListarConSP", ex);
            }
        }



        //Lógica para conectar a la DB y Agregar articulos.
        public void AgregarConSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {   
                datos.setearProcedimiento("storedAltaArticulos");

                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Empresa.Id);                         
                datos.setearParametro("@IdCategoria", nuevo.Clasificacion.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al conectarse a la base de datos para agregar un artículo: {ex.Message}");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //Lógica para conectar a la DB y Modificar articulos.
        public void Modificar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {   //seteo los parámetros de esta manera para evitar confusiones.
                datos.setearConsulta("UPDATE  ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio Where Id = @Id");

                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Empresa.Id);                        
                datos.setearParametro("@IdCategoria", nuevo.Clasificacion.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@id", nuevo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectarse a la base de datos para modificar un artículo: {ex.Message}");
                
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //Lógica para conectar a la DB y Filtrar artículos en filtro espcial.
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Empresa, C.Descripcion Clasificacion, A.ImagenUrl, A.Precio from ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdCategoria = C.Id And A.IdMarca = M.Id And ";
                
                if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }             
                else
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "A.Precio < " + filtro;
                            break;
                        default:
                            consulta += "A.Precio = " + filtro;
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];      //lector.GetInt32(0);
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Empresa = new Marca();
                    aux.Empresa.Descripcion = (string)datos.Lector["Empresa"];
                    aux.Clasificacion = new Categoria();
                    aux.Clasificacion.Descripcion = (string)datos.Lector["Clasificacion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Precio")) ? 0m : datos.Lector.GetDecimal(datos.Lector.GetOrdinal("Precio"));

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Lógica para conectar a la DB y Filtrar artículos en comboBox Empresa.
        public List<Articulo> filtrarEmpresa(string empresa)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Empresa, C.Descripcion Clasificacion, A.ImagenUrl, A.Precio from ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdCategoria = C.Id And A.IdMarca = M.Id And ";

                
                    switch (empresa)
                    {
                        case "Samsung":
                            consulta += "M.Descripcion like '" + empresa + "'";
                             break;
                        case "Apple":
                            consulta += "M.Descripcion like '" + empresa + "'";
                            break;
                        case "Sony":
                            consulta += "M.Descripcion like '" + empresa + "'";
                            break;
                        case "Huawei":
                             consulta += "M.Descripcion like '" + empresa + "'";
                            break;
                        default:
                            consulta += "M.Descripcion like '" + empresa + "'";
                        break;
                }
               

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];      //lector.GetInt32(0);
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Empresa = new Marca();
                    aux.Empresa.Descripcion = (string)datos.Lector["Empresa"];
                    aux.Clasificacion = new Categoria();
                    aux.Clasificacion.Descripcion = (string)datos.Lector["Clasificacion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Precio")) ? 0m : datos.Lector.GetDecimal(datos.Lector.GetOrdinal("Precio"));

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Lógica para conectar a la DB y Elimar articulos.
        public void Eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos(); 
                datos.setearConsulta("delete from ARTICULOS where id = @id"); 
                datos.setearParametro("@id", id); 
                datos.ejecutarAccion(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectarse a la base de datos para eliminar un artículo: {ex.Message}");
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        //Lógica para hacer el listado del los datos de la tabla Categorias leyéndoda desde la DB.
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion From CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read()) //leer las propiedades y tomar esos datos, como el el otro archivo pero es mucho menos.
                {
                    Categoria aux = new Categoria(); //creo una instancia de la clase elemento y agrego lo que agregamos en otros archivos.
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}

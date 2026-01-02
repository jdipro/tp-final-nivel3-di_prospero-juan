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
    public class MarcaNegocio
    {
        //Lógica para hacer el listado del los datos de la tabla Marcas leyéndoda desde la DB.
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Descripcion From MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read()) //leer las propiedades y tomar esos datos, como el otro archivo pero es mucho menos.
                {
                    Marca aux = new Marca(); //creo una instancia de la clase marca y agrego lo que agregamos en otros archivos.
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

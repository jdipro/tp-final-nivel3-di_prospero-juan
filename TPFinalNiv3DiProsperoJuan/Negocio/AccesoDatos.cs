using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Negocio
{
    //Lógica para habilitar la posibilidad de conexión a la DB.
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        //Lógica para realizar conexión a la DB.
        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true");
            comando = new SqlCommand(); 
        }

        //Lógica para ejecución de la lectura contra DB.
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        //Lógica para setear la consulta del Stored Procedure.
        public void setearProcedimiento(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        //Lógica para ejecución de la lectura contra DB.
        public void ejecutarLectura() 
        {
            comando.Connection = conexion; 
            try                        
            {
                conexion.Open(); 
                lector = comando.ExecuteReader(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Lógica para inserción de artículos a la DB.
        public void ejecutarAccion()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Lógica para obtener el Id (int) de los artículos de la DB.
        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString()); //devuelve la primer columna. Se castea para que devuelva un int.
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Seteo de parametros para el método Agregar()  en la clase ArticuloNegocio y Actualizar() UsersNegocio.
        public void setearParametro(string nombre, object valor )
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        //Cierre de conexion.
        public void cerrarConexion() //Tengo que agregar el cierre de la conexión y cierro el lector, si es q alguno conectánose.
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

    }
}

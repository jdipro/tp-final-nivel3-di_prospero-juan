using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class FavoritoNegocio
    {
        //Método listado de los datos de la tabla FAVORITOS leyéndoda desde la DB más específico para el idUser: (chatgpt)

        public List<int> listarIdsPorUsuario(int idUser)
        {
            List<int> lista = new List<int>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdArticulo FROM FAVORITOS WHERE IdUser = @idUser");
                datos.setearParametro("@idUser", idUser);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add((int)datos.Lector["IdArticulo"]);
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

        //Método Toggle favorito, agragado por chatGPt para 

        public void toggleFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            IF EXISTS (SELECT 1 FROM FAVORITOS WHERE IdUser = @idUser AND IdArticulo = @idArticulo)
                DELETE FROM FAVORITOS WHERE IdUser = @idUser AND IdArticulo = @idArticulo
            ELSE
                INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES (@idUser, @idArticulo)
        ");

                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArticulo", idArticulo);

                datos.ejecutarAccion();
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





        //Lógica para hacer el listado de los datos de la tabla FAVORITOS leyéndoda desde la DB.

        //Método hecho por mí, más genérico según chatgpt.

        //public List<Favorito> listar()
        //{
        //    List<Favorito> lista = new List<Favorito>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("Select Id, IdUser, IdArticulo From FAVORITOS");
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())             //leer las propiedades y tomar esos datos.
        //        {
        //            Favorito aux = new Favorito();      //creo una instancia de la clase Favorito y agrego lo que agregamos en otros archivos.
        //            aux.Id = (int)datos.Lector["Id"];
        //            aux.IdUser = (int)datos.Lector["IdUser"];
        //            aux.IdArticulo = (int)datos.Lector["IdArticulo"];

        //            lista.Add(aux);
        //        }

        //        return lista;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }     

        //}




    }
}

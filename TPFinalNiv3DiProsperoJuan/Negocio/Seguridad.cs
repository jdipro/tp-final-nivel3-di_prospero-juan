using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            //Para que accedan sólo los perfiles logeados:
            Users usuario = user != null ? (Users)user : null;
            if (usuario != null && usuario.Id != 0)
                return true;
            else
                return false;

        }

        public static bool esAdmin(object user)
        {
            Users usuario = user != null ? (Users)user : null;
            return usuario != null ? usuario.Admin : false;
        }

        public static string manejoError(Exception ex)
        {
            return ex.Message;
        }
    }
}

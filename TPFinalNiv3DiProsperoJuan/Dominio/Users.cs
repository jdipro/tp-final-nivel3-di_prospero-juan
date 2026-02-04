using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
   
    public class Users
    {
        public int Id { get; set; }
        private string email; //validación Email

        public string Email //validación Email
        {
            get { return email; } 
            set 
            {
                if (value != "")
                    email = value;
                else
                    throw new Exception("el campo Email está vacío");
            }
        }

        public string Pass { get; set; }
        public string Nombre{ get; set; }

        public string Apellido { get; set; }

        public string urlImagenPerfil { get; set; }

        public bool Admin { get; set; }

    }
}

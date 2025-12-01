using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //Esta biblioteca me permite usar DisplayName -> se conoce como Anotation.

namespace Dominio
{
    //Lógica para tomar las columnas de la tabla Articulos de la db y anezar de la tabla Marcas y Categorias.
    public class Articulo 
    {
        public int Id { get; set; } 

        [DisplayName("Código")]
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public Marca Empresa { get; set; }

        [DisplayName("Clasificación")]
        public Categoria Clasificacion { get; set; }

        public string ImagenUrl { get; set; }

        public decimal Precio { get; set; }

    }
}

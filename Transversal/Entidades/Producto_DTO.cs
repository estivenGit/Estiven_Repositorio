using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Interfaces;

namespace Transversal.Entidades
{
    public class Producto_DTO:IProducto
    {
        public int id { get; set; }
        public  string nombre { get; set; }
        public string descripcion { get; set; }
        public Categoria_DTO categoria { get; set;}
        public string imagen { get; set;}
        public bool activo { get; set;}
        public DateTime FechaCreacion { get; set; }

        public Producto_DTO()
        {
            id = 0;
            nombre = "";
            descripcion = "";
            categoria= new Categoria_DTO();
            imagen = "";
        }
    }
}

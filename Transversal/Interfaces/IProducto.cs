using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;

namespace Transversal.Interfaces
{
    public interface IProducto
    {
        int id { get; set; }
        string nombre { get; set; }
        string descripcion { get; set; }
        Categoria_DTO categoria { get; set; }
        string imagen { get; set; }
        bool activo { get; set; }
    }
}

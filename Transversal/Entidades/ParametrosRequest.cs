using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Entidades
{
    public class ParametrosRequest
    {
        public Producto_DTO Producto { get; set; }
        public OrdenamientoBase Ordenamiento { get; set; }
        public ParamPaginacion Paginacion { get; set; }
    }

}

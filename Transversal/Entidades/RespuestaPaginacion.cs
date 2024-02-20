using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Entidades
{
    public class RespuestaPaginacion<T>
    {
        public List<T> Datos { get; set; }
        public int TotalRegistros { get; set; }
    }
}

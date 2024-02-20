using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Entidades
{
    public class ParamPaginacion
    {
        public int paginaActual { get; set; }
        public int elementosPorPagina { get; set; }

        public ParamPaginacion()
        {
            paginaActual = 1;
            elementosPorPagina = 10;
        }
    }
}

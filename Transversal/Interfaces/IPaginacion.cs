using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;

namespace Transversal.Interfaces
{
    public interface IPaginacion<T>
    {
        int PaginaActual { get; }
        int ElementosPorPagina { get; }
        int TotalElementos { get; }

        RespuestaPaginacion<T> AplicarPaginacion(List<T> lista);
    }

}

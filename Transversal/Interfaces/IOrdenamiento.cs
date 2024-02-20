using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Transversal.Entidades.OrdenamientoBase;

namespace Transversal.Interfaces
{
    public interface IOrdenamiento
    {
        TipoOrdenamiento Ordenamiento { get; set; }
        CampoOrdenamiento Campo { get; set; }
    }
}

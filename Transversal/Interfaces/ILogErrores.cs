using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Interfaces
{
    public interface ILogErrores
    {
        int id { get; set; }
        string Mensaje { get; set; }
        string Linea { get; set; }
        string Ayuda_en_linea { get; set; }
        string Fuente { get; set; }
        string Metodo { get; set; }
        DateTime fecha { get; set; }
    }
}

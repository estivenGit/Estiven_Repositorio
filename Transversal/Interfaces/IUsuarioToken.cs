using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Interfaces
{
    public interface IUsuarioToken
    {
        int id_Tokens { get; set; }
        int id_usuario { get; set; }
        string Token { get; set; }
        int tiempoMinutos { get; set; }
        DateTime fechaInicio { get; set; }
        DateTime fechaFin { get; set; }
    }
}

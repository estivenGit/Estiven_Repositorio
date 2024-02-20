using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Interfaces
{
    public interface IUsuario
    {
        int Id { get; set; }
        string Usuario { get; set; }
        string Pass { get; set; }
        bool Activo { get; set; }
    }
}

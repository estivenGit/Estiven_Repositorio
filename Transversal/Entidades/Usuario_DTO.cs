using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Interfaces;

namespace Transversal.Entidades
{
    public class Usuario_DTO: IUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Pass { get; set; }
        public bool Activo { get; set; }

    }
}

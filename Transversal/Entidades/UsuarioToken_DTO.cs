using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Interfaces;

namespace Transversal.Entidades
{
    public class UsuarioToken_DTO: IUsuarioToken
    {
        public int id_Tokens { get; set; }
        public int id_usuario { get; set; }
        public string Token { get; set; }
        public int tiempoMinutos { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
    }
}

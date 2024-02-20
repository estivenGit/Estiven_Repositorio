using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Interfaces;

namespace Transversal.Entidades
{
    public class Categoria_DTO : ICategoria
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public Categoria_DTO()
        {
            id = 0;
            nombre = "";
        }
    }
}

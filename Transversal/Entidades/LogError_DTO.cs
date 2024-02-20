using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Interfaces;

namespace Transversal.Entidades
{
    public class LogError_DTO: ILogErrores
    {
        public int id {  get; set; }
        public string Mensaje {  get; set; }
        public string Linea {  get; set; }
        public string Ayuda_en_linea {  get; set; }
        public string Fuente {  get; set; }
        public string Metodo {  get; set; }
        public DateTime fecha {  get; set; }

    }
}

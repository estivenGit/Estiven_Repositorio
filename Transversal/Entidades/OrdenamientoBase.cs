using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Interfaces;

namespace Transversal.Entidades
{
    public class OrdenamientoBase : IOrdenamiento
    {
        public TipoOrdenamiento Ordenamiento { get; set; }
        public CampoOrdenamiento Campo { get; set; }


        public enum TipoOrdenamiento
        {
            Ascendente = 1,
            Descendente = 0
        }

        public enum CampoOrdenamiento
        {
            Nombre,
            Categoria
        }

        public OrdenamientoBase()
        {
            Ordenamiento = TipoOrdenamiento.Ascendente;
            Campo = CampoOrdenamiento.Nombre;

        }
    }
}

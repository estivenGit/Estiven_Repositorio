using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;
using Transversal.Interfaces;

namespace Transversal.Paginacion
{
    /// <summary>
    /// Clase que proporciona funcionalidad de paginación para una lista de elementos de tipo T.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos contenidos en la lista.</typeparam>
    public class Paginacion<T> :IPaginacion<T>
    {
        public int PaginaActual { get; private set; }
        public int ElementosPorPagina { get; private set; }
        public int TotalElementos { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Paginacion con la página actual y la cantidad de elementos por página.
        /// </summary>
        /// <param name="paginaActual">Número de la página actual.</param>
        /// <param name="elementosPorPagina">Cantidad de elementos por página.</param>
        public Paginacion(int paginaActual, int elementosPorPagina)
        {
            PaginaActual = paginaActual;
            ElementosPorPagina = elementosPorPagina;
        }

        /// <summary>
        /// Aplica la paginación a la lista proporcionada y devuelve una respuesta de paginación.
        /// </summary>
        /// <param name="lista">Lista de elementos a paginar.</param>
        /// <returns>Respuesta de paginación que contiene la página actual y la cantidad de elementos en la página.</returns>
        public RespuestaPaginacion<T> AplicarPaginacion(List<T> lista)
        {
            TotalElementos = lista.Count;

            int inicio = (PaginaActual - 1) * ElementosPorPagina;
            var elementosPagina = lista.Skip(inicio).Take(ElementosPorPagina).ToList();

            return new RespuestaPaginacion<T>
            {
                Datos = elementosPagina,
                TotalRegistros = TotalElementos
            };
        }
    }
}

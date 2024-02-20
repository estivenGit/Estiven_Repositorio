using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.db;
using AccesoDatos.Productos;
using Transversal.Entidades;
using Transversal.Interfaces;
using Transversal.LogErrores;
using Transversal.Paginacion;
using static Transversal.Entidades.OrdenamientoBase;

namespace LogicaNegocio.Productos
{
    /// <summary>
    /// Clase que implementa la lógica de negocio para la gestión de productos.
    /// Implementa la interfaz ILogicaNegocioProducto.
    /// </summary>
    public class Productos_LN :ILogicaNegocioProducto
    {
        private IAdminLogErrores _log { get; set; }
        private IAccesoDatosProducto _AccesoDatosProducto { get; set; }

        /// <summary>
        /// Constructor de la clase que recibe instancias de la interfaz IAdminLogErrores y IAccesoDatosProducto.
        /// </summary>
        /// <param name="log">Instancia de la interfaz IAdminLogErrores para el registro de errores.</param>
        /// <param name="AccesoDatosProducto">Instancia de la interfaz IAccesoDatosProducto para el acceso a datos de productos.</param>
        public Productos_LN(IAdminLogErrores log, IAccesoDatosProducto AccesoDatosProducto)
        {
            _log = log;
            _AccesoDatosProducto = AccesoDatosProducto;
        }

        /// <summary>
        /// Guarda o actualiza un registro de producto en la base de datos.
        /// </summary>
        /// <param name="productoEntity">DTO que contiene la información del producto a guardar o actualizar.</param>
        /// <returns>1 si se guarda o actualiza correctamente, -1 si hay un error.</returns>
        public int GuardarOActualizarRegistro(Producto_DTO productoEntity)
        {
            try
            {        
                //crear producto
                if (productoEntity.id == 0)
                {
                    productoEntity.FechaCreacion = DateTime.Now;
                    return _AccesoDatosProducto.Guardar(productoEntity);
                }
                else//actualizar producto
                {
                    return _AccesoDatosProducto.Actualizar(productoEntity);
                }
            }
            catch (Exception ex)
            {

                _log.LogError(ex, this.GetType().Name);
                return -1;
            }
        }

        /// <summary>
        /// Elimina un registro de producto de la base de datos por su identificador.
        /// </summary>
        /// <param name="idProducto">Identificador del producto a eliminar.</param>
        /// <returns>1 si se elimina correctamente, -1 si hay un error.</returns>
        public int EliminarRegistro(int idProducto)
        {
            try
            {
                return _AccesoDatosProducto.Eliminar(idProducto);
            }
            catch (Exception ex)
            {

                _log.LogError(ex, this.GetType().Name);
                return -1;
            }
        }

        /// <summary>
        /// Obtiene registros de productos paginados y ordenados según los criterios especificados.
        /// </summary>
        /// <param name="productoEntity">DTO que contiene los criterios de filtrado.</param>
        /// <param name="ordenamiento">Objeto que especifica el campo y tipo de ordenamiento.</param>
        /// <param name="paginacion">Objeto que define la paginación.</param>
        /// <returns>Respuesta paginada de productos en formato DTO.</returns>
        public RespuestaPaginacion<Producto_DTO> ObtenerRegistros(Producto_DTO productoEntity, OrdenamientoBase ordenamiento, ParamPaginacion paginacion)
        {
            try
            {

                RespuestaPaginacion<Producto_DTO> Datospaginados = new RespuestaPaginacion<Producto_DTO>();
                List<Producto_DTO> productos = _AccesoDatosProducto.FiltrarPorNombreDescripcion(productoEntity);
                if (productos != null) { 
                switch (ordenamiento.Campo)
                {
                    case CampoOrdenamiento.Nombre:
                        productos = OrdenarProductos(productos, x => x.nombre, ordenamiento.Ordenamiento);
                        break;
                    case CampoOrdenamiento.Categoria:
                        productos = OrdenarProductos(productos, x => x.categoria.nombre, ordenamiento.Ordenamiento);
                        break;
                }

                    var classPaginacion = new Paginacion<Producto_DTO>(paginacion.paginaActual, paginacion.elementosPorPagina);

                    Datospaginados = classPaginacion.AplicarPaginacion(productos);
                 
                }
                return Datospaginados;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return null;
            }
        }

        /// <summary>
        /// Ordena una lista de productos según el criterio de ordenamiento especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de dato del campo de ordenamiento.</typeparam>
        /// <param name="productos">Lista de productos a ordenar.</param>
        /// <param name="keySelector">Función que selecciona el campo de ordenamiento.</param>
        /// <param name="tipoOrdenamiento">Tipo de ordenamiento (ascendente o descendente).</param>
        /// <returns>Lista de productos ordenada.</returns>
        private List<Producto_DTO> OrdenarProductos<T>(
            List<Producto_DTO> productos,
            Func<Producto_DTO, T> keySelector,
            TipoOrdenamiento tipoOrdenamiento)
        {
            return tipoOrdenamiento == TipoOrdenamiento.Ascendente
                ? productos.OrderBy(keySelector).ToList()
                : productos.OrderByDescending(keySelector).ToList();
        }


    }
}

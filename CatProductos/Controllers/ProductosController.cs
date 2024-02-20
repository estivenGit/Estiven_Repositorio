using LogicaNegocio.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using Transversal.Entidades;
using Transversal.Interfaces;
using Transversal.LogErrores;
using Transversal.Paginacion;

namespace CatProductos.Controllers
{
    /// <summary>
    /// Controlador para manejar operaciones relacionadas con productos.
    /// </summary>
    [Authorize]
    public class ProductosController : ApiController
    {
        private IAdminLogErrores _log { get; set; }
        private ILogicaNegocioProducto _logicaNegocio { get; set; }

        /// <summary>
        /// Constructor de la clase ProductosController.
        /// </summary>
        /// <param name="log">Instancia de la interfaz IAdminLogErrores para el registro de errores.</param>
        /// <param name="logicaNegocio">Instancia de la interfaz ILogicaNegocioProducto para la lógica de negocio relacionada con productos.</param>
        public ProductosController(IAdminLogErrores log, ILogicaNegocioProducto logicaNegocio)
        {
            _log = log;
            _logicaNegocio = logicaNegocio;
        }

        /// <summary>
        /// Obtiene una lista de productos según los parámetros proporcionados.
        /// </summary>
        /// <param name="ParametrosRequest">Parámetros de la solicitud que incluyen filtros, ordenamiento y paginación.</param>
        /// <returns>Una respuesta HTTP con la lista de productos o un mensaje de error.</returns>
        // GET: api/Productos
        public IHttpActionResult Get([FromBody] ParametrosRequest ParametrosRequest)
        {
            try
            {
                var rta = _logicaNegocio.ObtenerRegistros(
                     ParametrosRequest.Producto ?? new Producto_DTO(),
                     ParametrosRequest.Ordenamiento ?? new OrdenamientoBase(),
                     ParametrosRequest.Paginacion ?? new ParamPaginacion()
                 );
                if (rta != null) { 
                    return Ok(rta);
                }else {
                    return BadRequest("Error obteniendo los Productos");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return BadRequest("Error obteniendo los Productos");
            }
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="producto">Datos del producto a ser creado.</param>
        /// <returns>Una respuesta HTTP indicando si la operación fue exitosa o un mensaje de error.</returns>
        // POST: api/Productos
        public IHttpActionResult Post([FromBody] Producto_DTO producto)
        {
            try
            {
                var rta = _logicaNegocio.GuardarOActualizarRegistro(producto);
                if (rta != -1)
                {
                    return Ok("Producto almacenado Correctamente");
                }
                else
                {
                    return BadRequest("Error guardando el Producto");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return BadRequest("Error guardando el Producto");
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="producto">Datos actualizados del producto.</param>
        /// <returns>Una respuesta HTTP indicando si la operación fue exitosa o un mensaje de error.</returns>
        // PUT: api/Productos
        public IHttpActionResult Put([FromBody] Producto_DTO producto)
        {
            try
            {
                var rta = _logicaNegocio.GuardarOActualizarRegistro(producto);
                if (rta != -1)
                {
                    return Ok("Producto actualizado Correctamente");
                }
                else
                {
                    return BadRequest("Error actualizando el Producto");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return BadRequest("Error actualizando el Producto");
            }
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a ser eliminado.</param>
        /// <returns>Una respuesta HTTP indicando si la operación fue exitosa o un mensaje de error.</returns>
        // DELETE: api/Productos/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var rta = _logicaNegocio.EliminarRegistro(id);
                if (rta != -1)
                {
                    return Ok("Producto eliminado Correctamente");
                }
                else
                {
                    return BadRequest("Error eliminando el Producto");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return BadRequest("Error eliminando el Producto");
            }
        }
    }
}

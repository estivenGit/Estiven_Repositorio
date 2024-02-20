using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Transversal.db;
using Transversal.Entidades;
using Transversal.Interfaces;
using Transversal.LogErrores;
using CatagoloProductosDBEntities = AccesoDatos.db.CatagoloProductosDBEntities;

namespace AccesoDatos.Productos
{
    /// <summary>
    /// Clase que proporciona acceso a datos para la gestión de productos.
    /// Implementa la interfaz IAccesoDatosProducto y la interfaz IDisposable.
    /// </summary>
    public class Productos_DA : IAccesoDatosProducto, IDisposable
    {
        private IAdminLogErrores _log { get; set; }

        /// <summary>
        /// Constructor de la clase que recibe una instancia de la interfaz IAdminLogErrores para el registro de errores.
        /// </summary>
        /// <param name="log">Instancia de la interfaz IAdminLogErrores.</param>
        public Productos_DA(IAdminLogErrores log)
        {
            _log = log;
        }

        private readonly CatagoloProductosDBEntities _context = new CatagoloProductosDBEntities();

        /// <summary>
        /// Guarda un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="productoEntity">DTO que contiene la información del producto a guardar.</param>
        /// <returns>1 si se guarda correctamente, -1 si hay un error.</returns>
        public int Guardar(Producto_DTO productoEntity)
        {
            try
            {
                AccesoDatos.db.Productos entity = new AccesoDatos.db.Productos
                {
                    Nombre = productoEntity.nombre,
                    Descripcion = productoEntity.descripcion,
                    imagen_Producto = productoEntity.imagen,
                    id_Categoria = productoEntity.categoria.id,
                    FechaModificacion = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    Activo = true
                };

                // Almacenar                
                _context.Productos.Add(entity);
                _context.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return -1;
            }
        }

        /// <summary>
        /// Actualiza la información de un producto existente en la base de datos.
        /// </summary>
        /// <param name="productoEntity">DTO que contiene la información actualizada del producto.</param>
        /// <returns>1 si se actualiza correctamente, -1 si hay un error.</returns>
        public int Actualizar(Producto_DTO productoEntity)
        {
            try
            {
                AccesoDatos.db.Productos entity = new AccesoDatos.db.Productos
                {
                    id_Producto = productoEntity.id,
                    Nombre = productoEntity.nombre,
                    Descripcion = productoEntity.descripcion,
                    imagen_Producto = productoEntity.imagen,
                    id_Categoria = productoEntity.categoria.id,
                    FechaModificacion = DateTime.Now,
                    Activo = true
                };
                if (!_context.Productos.Local.Any(e => e.id_Producto == entity.id_Producto))
                {
                    _context.Productos.Attach(entity);
                }

                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return -1;
            }
        }

        /// <summary>
        /// Elimina un producto de la base de datos por su identificador.
        /// </summary>
        /// <param name="idProducto">Identificador del producto a eliminar.</param>
        /// <returns>1 si se elimina correctamente, -1 si hay un error.</returns>
        public int Eliminar(int idProducto)
        {
            try
            {
                var entidad = _context.Productos.Find(idProducto);
                if (entidad != null)
                {
                    if (!_context.Productos.Local.Any(e => e.id_Producto == entidad.id_Producto))
                    {
                        _context.Productos.Attach(entidad);
                    }

                    _context.Productos.Remove(entidad);
                    _context.SaveChanges();

                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return -1;
            }
        }

        /// <summary>
        /// Obtiene todos los productos activos de la base de datos en formato DTO.
        /// </summary>
        /// <returns>Lista de productos en formato DTO.</returns>
        public List<Producto_DTO> ObtenerTodos()
        {
            try
            {
                return _context.Productos
                    .Where(p => p.Activo == true)
                    .Select(item => new Producto_DTO
                    {
                        id = item.id_Producto,
                        nombre = item.Nombre,
                        descripcion = item.Descripcion,
                        categoria = new Categoria_DTO
                        {
                            id = item.Categorias.id_Categoria,
                            nombre = item.Nombre
                        }
                    }).ToList();

            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return null;
            }
        }

        /// <summary>
        /// Filtra productos por nombre, descripción y categoría en la base de datos en formato DTO.
        /// </summary>
        /// <param name="productoEntity">DTO que contiene los criterios de filtrado.</param>
        /// <returns>Lista de productos filtrados en formato DTO.</returns>
        public List<Producto_DTO> FiltrarPorNombreDescripcion(Producto_DTO productoEntity)
        {
            try
            {
                var query = _context.Productos.AsQueryable();

                // Aplicar filtrado solo si el filtroNombre no es nulo ni vacío
                if (!string.IsNullOrEmpty(productoEntity.nombre))
                {
                    query = query.Where(p => p.Nombre.Contains(productoEntity.nombre));
                }

                // Aplicar filtrado solo si el filtroDescripcion no es nulo ni vacío
                if (!string.IsNullOrEmpty(productoEntity.descripcion))
                {
                    query = query.Where(p => p.Descripcion.Contains(productoEntity.descripcion));
                }

                if (!string.IsNullOrEmpty(productoEntity.categoria.nombre))
                {
                    query = query.Where(p => p.Categorias.Nombre.Contains(productoEntity.categoria.nombre));
                }

                // Proyectar los resultados en DTO y convertir a lista
                return query.Select(item => new Producto_DTO
                {
                    id = item.id_Producto,
                    nombre = item.Nombre,
                    descripcion = item.Descripcion,
                    categoria = new Categoria_DTO
                    {
                        id = item.Categorias.id_Categoria,
                        nombre = item.Nombre
                    },
                    imagen=item.imagen_Producto
                }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

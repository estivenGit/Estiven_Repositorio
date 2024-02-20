using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;

namespace Transversal.Interfaces
{
    public interface IAccesoDatosProducto
    {
        int Guardar(Producto_DTO entity);
        int Actualizar(Producto_DTO entity);
        int Eliminar(int id);
        List<Producto_DTO> ObtenerTodos();
        List<Producto_DTO> FiltrarPorNombreDescripcion(Producto_DTO entidad);
    }
}

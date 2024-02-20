using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;

namespace Transversal.Interfaces
{
    public interface ILogicaNegocioProducto
    {
        int GuardarOActualizarRegistro(Producto_DTO entidadDTO);
        int EliminarRegistro(int idProducto);
        RespuestaPaginacion<Producto_DTO> ObtenerRegistros(Producto_DTO entidadDTO, OrdenamientoBase ordenamiento, ParamPaginacion paginacion);
    }
}

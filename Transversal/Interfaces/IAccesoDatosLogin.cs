using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;

namespace Transversal.Interfaces
{
    public interface IAccesoDatosLogin 
    {
        List<Usuario_DTO> ConsultarXUsuario(Usuario_DTO usuario);
        List<UsuarioToken_DTO> ConsultarTokenUsuario(Usuario_DTO usuario);
        int guardarToken(UsuarioToken_DTO token);
    }
}

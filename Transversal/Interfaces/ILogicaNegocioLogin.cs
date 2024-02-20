using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;

namespace Transversal.Interfaces
{
    public interface ILogicaNegocioLogin
    {
        string validarUsuarioToken(Usuario_DTO usuario);
        bool validarUsuario(Usuario_DTO usuario);
        string validarToken(Usuario_DTO usuario);
        int guardarToken(UsuarioToken_DTO token);
        int consultarIdUsuario(Usuario_DTO usuario);
    }
}

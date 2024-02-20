using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.db;
using AccesoDatos.Login;
using AccesoDatos.Productos;
using Transversal.Entidades;
using Transversal.Interfaces;
using Transversal.LogErrores;
using static Transversal.Entidades.OrdenamientoBase;

namespace LogicaNegocio.Login
{
    /// <summary>
    /// Clase que implementa la lógica de negocio para la autenticación de usuarios y gestión de tokens.
    /// Implementa la interfaz ILogicaNegocioLogin.
    /// </summary>
    public class Login_LN:ILogicaNegocioLogin
    {
        private IAdminLogErrores _log { get; set; }

        /// <summary>
        /// Constructor de la clase que recibe una instancia de la interfaz IAdminLogErrores para el registro de errores.
        /// </summary>
        /// <param name="log">Instancia de la interfaz IAdminLogErrores.</param>
        public Login_LN(IAdminLogErrores log)
        {
            _log = log;
        }

        private readonly Login_DA _AccesoDatos = new Login_DA();

        /// <summary>
        /// Valida un usuario y su token. Retorna el token válido si el usuario está activo y el token no ha expirado.
        /// </summary>
        /// <param name="usuario">DTO que contiene la información del usuario.</param>
        /// <returns>Token válido, "0" si el usuario no está activo, "2" si el token ha expirado.</returns>
        public string validarUsuarioToken(Usuario_DTO usuario)
        {
            try
            {
                bool UsuarioActivo = validarUsuario(usuario);
                if (UsuarioActivo)
                {
                    return validarToken(usuario);
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// Valida si un usuario está activo en la base de datos.
        /// </summary>
        /// <param name="usuario">DTO que contiene la información del usuario.</param>
        /// <returns>true si el usuario está activo, false si no está activo.</returns>
        public bool validarUsuario(Usuario_DTO usuario)
        {
            try
            {
                List<Usuario_DTO> usuarios = _AccesoDatos.ConsultarXUsuario(usuario);
                if (usuarios.Any(x => x.Pass == usuario.Pass && x.Activo == true))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// Valida si un token de usuario está vigente en la base de datos.
        /// </summary>
        /// <param name="usuario">DTO que contiene la información del usuario.</param>
        /// <returns>Token válido, "2" si el token ha expirado.</returns>
        public string validarToken(Usuario_DTO usuario)
        {
            try
            {
                List<UsuarioToken_DTO> UsuarioToken = _AccesoDatos.ConsultarTokenUsuario(usuario);
                if (UsuarioToken != null && UsuarioToken.Any(x => x.fechaFin > DateTime.Now))
                {
                    return UsuarioToken.Where(x => x.fechaFin > DateTime.Now).OrderByDescending(x => x.fechaFin).Select(x=>x.Token).FirstOrDefault();
                }
                else
                {
                    return "2";
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// Guarda un token de usuario en la base de datos.
        /// </summary>
        /// <param name="token">DTO que contiene la información del token a guardar.</param>
        /// <returns>1 si se guarda correctamente, -1 si hay un error.</returns>
        public int guardarToken(UsuarioToken_DTO token)
        {
            try
            {
                return _AccesoDatos.guardarToken(token);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                throw;
            }
        }

        /// <summary>
        /// Consulta el identificador de un usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">DTO que contiene la información del usuario.</param>
        /// <returns>Identificador del usuario.</returns>
        public int consultarIdUsuario(Usuario_DTO usuario)
        {
            try
            {
                List<Usuario_DTO> usuarios = _AccesoDatos.ConsultarXUsuario(usuario);
                return usuarios.Select(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                throw;
            }
        }

    }
}

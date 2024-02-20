using AccesoDatos.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Entidades;
using Transversal.Interfaces;
using Transversal.LogErrores;

namespace AccesoDatos.Login
{
    /// <summary>
    /// Clase que proporciona acceso a datos para la autenticación y gestión de tokens de usuarios.
    /// Implementa la interfaz IAccesoDatosLogin y la interfaz IDisposable.
    /// </summary>
    public class Login_DA : IAccesoDatosLogin, IDisposable
    {
        
        private readonly AdminLogErrores _log = new AdminLogErrores();
        private readonly CatagoloProductosDBEntities _context = new CatagoloProductosDBEntities();

        /// <summary>
        /// Consulta un usuario por su nombre de usuario y devuelve una lista de usuarios en formato DTO.
        /// </summary>
        /// <param name="usuario">DTO que contiene la información del usuario a consultar.</param>
        /// <returns>Lista de usuarios en formato DTO.</returns>
        public List<Usuario_DTO> ConsultarXUsuario(Usuario_DTO usuario)
        {
            try
            {
                var query = _context.Usuarios.AsQueryable();
                query = query.Where(p => p.Usuario.Equals(usuario.Usuario));

                return query.Select(item => new Usuario_DTO
                {
                    Id = item.id_usuario,
                    Nombre = item.Nombre,
                    Pass = item.Pass,
                    Activo = (bool)item.Activo
                }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return null;
            }
        }
        /// <summary>
        /// Consulta los tokens de un usuario y devuelve una lista de tokens en formato DTO.
        /// </summary>
        /// <param name="usuario">DTO que contiene la información del usuario para el cual se consulta el token.</param>
        /// <returns>Lista de tokens en formato DTO.</returns>
        public List<UsuarioToken_DTO> ConsultarTokenUsuario(Usuario_DTO usuario)
        {
            try
            {
                var query = _context.UsuariosTokens.Where(p => p.id_usuario==usuario.Id)
                    .Select(item => new UsuarioToken_DTO
                    {
                        id_Tokens = item.id_Tokens,
                        id_usuario = item.id_usuario ?? 0,
                        Token = item.Token,
                        fechaFin = item.fechaFin ?? DateTime.MinValue
                    }).ToList();

                return query;

            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return null;
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
                AccesoDatos.db.UsuariosTokens entity = new AccesoDatos.db.UsuariosTokens
                {
                    id_usuario = token.id_usuario,
                    Token = token.Token,
                    tiempoMinutos = token.tiempoMinutos,
                    fechaInicio = token.fechaInicio,
                    fechaFin = token.fechaFin
                };

                // Almacenar                
                _context.UsuariosTokens.Add(entity);
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
        /// Libera los recursos utilizados por la instancia de la clase.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

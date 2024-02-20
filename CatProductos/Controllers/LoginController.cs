using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Transversal.Entidades;
using LogicaNegocio.Productos;
using LogicaNegocio.Login;
using Transversal.LogErrores;
using Transversal.Interfaces;

namespace CatProductos.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones relacionadas con la autenticación de usuarios.
    /// Hereda de ApiController.
    /// </summary>
    [System.Web.Http.AllowAnonymous]
    [System.Web.Http.RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private IAdminLogErrores _log { get; set; }
        private ILogicaNegocioLogin _logicaNegocio { get; set; }

        /// <summary>
        /// Constructor de la clase que recibe instancias de IAdminLogErrores e ILogicaNegocioLogin.
        /// </summary>
        /// <param name="log">Instancia de IAdminLogErrores para el registro de errores.</param>
        /// <param name="logicaNegocio">Instancia de ILogicaNegocioLogin para la lógica de negocio de autenticación.</param>
        public LoginController(IAdminLogErrores log, ILogicaNegocioLogin logicaNegocio)
        {
            _log = log;
            _logicaNegocio = logicaNegocio;
        }

        /// <summary>
        /// Método HTTP GET que responde a la ruta "echoping".
        /// Retorna un resultado HTTP OK con valor booleano true.
        /// </summary>
        /// <returns>Resultado HTTP OK con valor true.</returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }


        /// <summary>
        /// Método HTTP GET que responde a la ruta "echouser".
        /// Retorna un resultado HTTP OK con información del usuario actual.
        /// </summary>
        /// <returns>Resultado HTTP OK con información del usuario actual.</returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        /// <summary>
        /// Método HTTP POST que responde a la ruta "authenticate".
        /// Autentica al usuario utilizando las credenciales proporcionadas en el cuerpo de la solicitud.
        /// </summary>
        /// <param name="login">Objeto que contiene las credenciales del usuario (Username y Password).</param>
        /// <returns>
        /// Resultado HTTP OK con el token de autenticación si la autenticación es exitosa.
        /// Resultado HTTP Unauthorized si las credenciales no son válidas.
        /// Resultado HTTP BadRequest en caso de error durante la autenticación.
        /// </returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            try
            {


                if (login == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);


                Usuario_DTO usuario = new Usuario_DTO
                {
                    Usuario = login.Username,
                    Pass = login.Password
                };


                usuario.Id = _logicaNegocio.consultarIdUsuario(usuario);
                var userToken = _logicaNegocio.validarUsuarioToken(usuario);
                /////////userToken----->0 USUARIO NO VALIDO*
                /////////userToken----->LLEGA EL TOKEN TOKEN ACTIVO
                /////////userToken----->null TOKEN VENCIDO

                if (userToken == "0")
                {
                    return Unauthorized();
                }
                else if (userToken == "2")
                {
                    var token = TokenGenerator.GenerateTokenJwt(login.Username);
                    token.id_usuario = _logicaNegocio.consultarIdUsuario(usuario);
                    _logicaNegocio.guardarToken(token);

                    return Ok(token.Token);
                }
                else
                {
                    return Ok(userToken);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, this.GetType().Name);
                return BadRequest("Error Autenticando el usuario");
            }
        }
    }
}
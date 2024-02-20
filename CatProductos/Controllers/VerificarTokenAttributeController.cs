using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CatProductos.Controllers
{
    /// <summary>
    /// Manejador de validación de token utilizado para autenticar las solicitudes HTTP mediante JWT.
    /// </summary>
    internal class TokenValidationHandler : DelegatingHandler
    {
        /// <summary>
        /// Intenta recuperar el token de autorización del encabezado de la solicitud.
        /// </summary>
        /// <param name="request">Mensaje de solicitud HTTP.</param>
        /// <param name="token">Token recuperado (si se encuentra).</param>
        /// <returns>True si se recupera el token correctamente, false en caso contrario.</returns>
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        /// <summary>
        /// Sobrescribe el método SendAsync para realizar la validación del token antes de procesar la solicitud.
        /// </summary>
        /// <param name="request">Mensaje de solicitud HTTP.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una tarea que representa la operación asincrónica de enviar la solicitud HTTP.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            // determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // Extract and assign Current Principal and user
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        /// <summary>
        /// Validador de tiempo de vida del token.
        /// </summary>
        /// <param name="notBefore">Fecha y hora antes de la cual el token no es válido.</param>
        /// <param name="expires">Fecha y hora de caducidad del token.</param>
        /// <param name="securityToken">Token de seguridad.</param>
        /// <param name="validationParameters">Parámetros de validación del token.</param>
        /// <returns>True si el token es válido en términos de tiempo de vida, false en caso contrario.</returns>
        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}
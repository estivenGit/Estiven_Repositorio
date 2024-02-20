using System;
using System.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Transversal.Entidades;

namespace CatProductos.Controllers
{
    /// <summary>
    /// JWT Token generator class using "secret-key"
    /// more info: https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html
    /// </summary>
    internal static class TokenGenerator
    {
        /// <summary>
        /// Genera un token JWT para el usuario especificado.
        /// </summary>
        /// <param name="username">Nombre de usuario para el cual se genera el token.</param>
        /// <returns>Objeto UsuarioToken_DTO que contiene la información del token generado.</returns>
        public static UsuarioToken_DTO GenerateTokenJwt(string username)
        {
            // appsetting for Token JWT
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.Now,
                issuedAt: DateTime.Now,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            return new UsuarioToken_DTO
            {
                Token= jwtTokenString,
                fechaInicio= DateTime.Now,
                fechaFin= DateTime.Now.AddMinutes(Convert.ToInt32(expireTime)),
                tiempoMinutos= Convert.ToInt32(expireTime)
            };
        }
    }
}
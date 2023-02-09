using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using APIWeb.Models;

namespace APIWeb.Services {
    public class TokenService : ITokenService {
        private readonly SymmetricSecurityKey? _ssKey = null;

        public TokenService(IConfiguration configuration) {
            string? palabraSecreta = configuration["Token"];
            if (!string.IsNullOrEmpty(palabraSecreta)) {
                _ssKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(palabraSecreta));
            }
        }

        public string CreateToken(Usuario usuario) {
            var claims = new List<Claim>();
            if (usuario.CorreoElectronico != null) {
                claims.Add(
                  new Claim(JwtRegisteredClaimNames.NameId, usuario.CorreoElectronico)
                );
            }
            var credenciales = new SigningCredentials(_ssKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
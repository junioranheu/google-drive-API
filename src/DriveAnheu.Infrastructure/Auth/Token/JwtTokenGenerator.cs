using DriveAnheu.Infrastructure.Auth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Infrastructure.Auth.Token
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GerarToken(int usuarioId, Guid id)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret ?? string.Empty)),
                algorithm: SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claims = new(new Claim[]
            {
                new(type: ClaimTypes.NameIdentifier, usuarioId.ToString()),
                new(type: ClaimTypes.Thumbprint, id.ToString())
            });

            DateTime horarioBrasiliaAjustado = HorarioBrasiliaAjustado();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = _jwtSettings.Issuer, // Emissor do token;
                IssuedAt = horarioBrasiliaAjustado,
                Audience = _jwtSettings.Audience,
                NotBefore = horarioBrasiliaAjustado,
                Expires = horarioBrasiliaAjustado.AddMinutes(_jwtSettings.TokenExpiryMinutes),
                Subject = claims,
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }

        private static DateTime HorarioBrasiliaAjustado()
        {
            // Por algum motivo inexplicável é necessário ajustar a hora por uma diferença apresentada quando publicado em produção no Azure;
            // Produção: +3 horas;
            DateTime horarioBrasiliaAjustado = GerarHorarioBrasilia().AddHours(+3);

#if DEBUG
            // Dev: horário normal;
            horarioBrasiliaAjustado = GerarHorarioBrasilia();
#endif

            return horarioBrasiliaAjustado;
        }
    }
}
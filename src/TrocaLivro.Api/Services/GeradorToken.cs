using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrocaLivro.Api.Configuracoes;
using TrocaLivro.Api.Interfaces;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Api.Services
{
    public class GeradorToken : IGeradorToken
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly JwtConfiguracao _jwtConfig;

        public GeradorToken(UserManager<Usuario> userManager, IOptionsMonitor<JwtConfiguracao> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<string> Gerar(string login, Usuario usuarioEncontrado)
        {
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var rolesDoUsuario = await _userManager.GetRolesAsync(usuarioEncontrado);

            var role = rolesDoUsuario.First();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", usuarioEncontrado.Id),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}

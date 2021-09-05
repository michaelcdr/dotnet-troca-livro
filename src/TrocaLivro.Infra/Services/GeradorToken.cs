using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Configuracoes;

namespace TrocaLivro.Infra.Services
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

        public async Task<AppResponse<TokenResultado>> Gerar(string login)
        {
            Usuario usuario = await _userManager.FindByNameAsync(login);

            if (usuario == null) return new AppResponse<TokenResultado>(false, $"O usuario {login} não foi encontrado.");

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var rolesDoUsuario = await _userManager.GetRolesAsync(usuario);

            if (rolesDoUsuario == null || rolesDoUsuario.Count == 0) return new AppResponse<TokenResultado>(false, "Tipo de usuário não encontrado.");

            var role = rolesDoUsuario.First();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", usuario.Id),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new AppResponse<TokenResultado>(true, "Token gerado com sucesso", new TokenResultado(jwtToken));
        }

        public async Task<AppResponse<TokenResultado>> GerarSimples(string login)
        {
            Usuario usuario = await _userManager.FindByNameAsync(login);

            if (usuario == null) return new AppResponse<TokenResultado>(false, $"O usuario {login} não foi encontrado.");

            var direitos = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "WebApp",
                audience: "Postman",
                claims: direitos,
                signingCredentials: credenciais,
                expires: DateTime.Now.AddMinutes(30)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AppResponse<TokenResultado>(true, "Token gerado com sucesso", new TokenResultado(tokenString));
        }
    }
}

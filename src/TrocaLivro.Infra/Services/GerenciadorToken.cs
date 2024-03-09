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
    public class GerenciadorToken : IGerenciadorToken
    {
        private const string MSG_NOTFOUND = "Tipo de usuário não encontrado.";
        private readonly UserManager<Usuario> _userManager;
        private readonly JwtConfiguracao _jwtConfig;

        public GerenciadorToken(UserManager<Usuario> userManager, IOptionsMonitor<JwtConfiguracao> optionsMonitor)
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

            if (rolesDoUsuario == null || rolesDoUsuario.Count == 0) return new AppResponse<TokenResultado>(false, MSG_NOTFOUND);

            var role = rolesDoUsuario.First();

            var identityClaims = new ClaimsIdentity(new[]
            {
                new Claim("Id", usuario.Id),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new AppResponse<TokenResultado>(true, "Token gerado com sucesso", new TokenResultado(jwtToken, role, usuario.Id));
        }

        public string ObterNomeUsuario(string token)
        {
            if (token.Contains("Bearer "))
                token = token.Replace("Bearer ", "");

            string secret = _jwtConfig.Secret;
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            var nomeUsuario = claims.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;
            return nomeUsuario;
        } 
    }
}

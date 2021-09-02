using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.Services
{
    public class GeradorDadosPadroesDaAplicacao
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoUsuario> _roleManager;
        private const string ROLE_ADM = "admin";
        private const string ROLE_COMUN = "comum";
        public GeradorDadosPadroesDaAplicacao(UserManager<Usuario> userManager, RoleManager<TipoUsuario> roleManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Gerar()
        {
            IList<Usuario> admins = await _userManager.GetUsersInRoleAsync(ROLE_ADM);
            IList<Usuario> comuns = await _userManager.GetUsersInRoleAsync(ROLE_COMUN);

            if(!await _roleManager.RoleExistsAsync(ROLE_ADM))
                await _roleManager.CreateAsync(new TipoUsuario { Name = ROLE_ADM, NormalizedName = ROLE_ADM });

            if (!await _roleManager.RoleExistsAsync(ROLE_COMUN))
                await _roleManager.CreateAsync(new TipoUsuario { Name = ROLE_COMUN, NormalizedName = ROLE_COMUN });

            if (!admins.Any(e => e.UserName == "michael"))
            {
                var usuario = new Usuario("Michael", "michael", "michaelcdr@hotmail.com");

                IdentityResult result = await _userManager.CreateAsync(usuario, "123456");

                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_ADM);
            }

            if (!comuns.Any(e => e.UserName == "michael.comum"))
            {
                var usuario = new Usuario("Michael", "michael.comum", "michaelcdr@hotmail.com");

                IdentityResult result = await _userManager.CreateAsync(usuario, "123456");

                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_COMUN);
            }
        }
    }
}

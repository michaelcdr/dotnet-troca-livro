using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.Services
{
    public class GeradorDadosPadroesDaAplicacao
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoUsuario> _roleManager;
        private readonly ApplicationDbContext context;
        private const string ROLE_ADM = "admin";
        private const string ROLE_COMUN = "comum";
        public GeradorDadosPadroesDaAplicacao(UserManager<Usuario> userManager, RoleManager<TipoUsuario> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.context = context;
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
                var usuario = new Usuario("Michael", "michael", "michaelcdr@hotmail.com","Costa dos Reis");

                IdentityResult result = await _userManager.CreateAsync(usuario, "123456");

                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_ADM);
            }

            if (!comuns.Any(e => e.UserName == "michael.comum"))
            {
                var usuario = new Usuario("Michael", "michael.comum", "michaelcdr@hotmail.com", "Costa dos Reis");

                IdentityResult result = await _userManager.CreateAsync(usuario, "123456");

                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_COMUN);
            }

            if (await this.context.Autores.CountAsync() == 0)
            {
                this.context.Autores.Add(new Autor("Robert C. Martin"));
                this.context.Autores.Add(new Autor("Eric Evans"));
                this.context.Autores.Add(new Autor("George Martin"));
                this.context.Autores.Add(new Autor("Andrei Fernandes"));
                this.context.Autores.Add(new Autor("Martin Fowler"));
            }

            if (await this.context.Editoras.CountAsync() == 0)
            {
                this.context.Editoras.Add(new Editora("Prentice Hall PTR"));
                this.context.Editoras.Add(new Editora("Alta Books"));
            }

            if (await this.context.Categorias.CountAsync() == 0)
            {
                this.context.Categorias.Add(new Categoria("Livros de Computação, Informática e Mídias Digitais"));
            }
            
            await this.context.SaveChangesAsync();
        }
    }
}

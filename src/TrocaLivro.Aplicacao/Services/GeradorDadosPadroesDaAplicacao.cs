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

        public GeradorDadosPadroesDaAplicacao(
            UserManager<Usuario> userManager, 
            RoleManager<TipoUsuario> roleManager,
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
            
            await this.context.SaveChangesAsync();

            if (await this.context.Categorias.CountAsync() == 0)
            {
                this.context.Categorias.AddRange(
                    new List<Categoria>
                    {
                        new Categoria("Administração"),
                        new Categoria("Agropecuária"),
                        new Categoria("Artes"),
                        new Categoria("Autoajuda"),
                        new Categoria("Ciências Biológicas"),
                        new Categoria("Ciências Exatas"),
                        new Categoria("Ciências Humanas e Sociais"),
                        new Categoria("Contabilidade"),
                        new Categoria("Gastronomia"),
                        new Categoria("Cursos e Idiomas"),
                        new Categoria("Didáticos"),
                        new Categoria("Dicionários e Manuais de Conversação"),
                        new Categoria("Direito"),
                        new Categoria("Economia"),
                        new Categoria("Engenharia e Tecnologia"),
                        new Categoria("Esoterismo"),
                        new Categoria("Espiritismo"),
                        new Categoria("Esportes e Lazer"),
                        new Categoria("Geografia e Historia"),
                        new Categoria("Informática"),
                        new Categoria("Linguística"),
                        new Categoria("Literatura Estrangeira"),
                        new Categoria("Literatura Infantojuvenil"),
                        new Categoria("Literatura Brasileira"),
                        new Categoria("Medicina"),
                        new Categoria("Pocket Books"),
                        new Categoria("Psicologia e Psicanálise"),
                        new Categoria("Religião"),
                        new Categoria("Turismo"),
                        new Categoria("Audiolivro"),
                        new Categoria("Diversos"),
                        new Categoria("Produtos Descontinuados"),
                        new Categoria("Hqs")
                    }
                );
                await this.context.SaveChangesAsync();

                Categoria categoriaInformatica = await this.context.Categorias.SingleAsync(e => e.Nome == "Informática");

                categoriaInformatica.AdicionarSubCategorias(
                    new List<SubCategoria>()
                    {
                        new SubCategoria("Analise de Sistemas"),
                        new SubCategoria("Banco de Dados"),
                        new SubCategoria("Computação Gráfica"),
                        new SubCategoria("Hardware"),
                        new SubCategoria("Jogos"),
                        new SubCategoria("Linguagens"),
                        new SubCategoria("Multimídia"),
                        new SubCategoria("Planilhas"),
                        new SubCategoria("Processadores de Textos"),
                        new SubCategoria("Programas"),
                        new SubCategoria("Realidade Virtual"),
                        new SubCategoria("Redes"),
                        new SubCategoria("Sistemas Operacionais")
                    }
                );

                await this.context.SaveChangesAsync();
            }
        }
    }
}

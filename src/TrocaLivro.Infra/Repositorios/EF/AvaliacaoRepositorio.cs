using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Infra.Repositorios.EF
{
    public class AvaliacaoRepositorio : Repositorio<Avaliacao>, IAvaliacaoRepositorio
    {
        public AvaliacaoRepositorio(ApplicationDbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocaQueryHandler : IRequestHandler<ObterTrocaQuery, AppResponse<ObterTrocaResultado>>
    {
        private readonly ApplicationDbContext _db;

        public ObterTrocaQueryHandler(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<AppResponse<ObterTrocaResultado>> Handle(ObterTrocaQuery request, CancellationToken cancellationToken)
        {
            Troca livroDisponibilizado = await _db.Trocas.AsNoTracking()
                .Include(disponibilizacao => disponibilizacao.Livro).ThenInclude(livro => livro.Imagens)
                .Include(disponibilizacao => disponibilizacao.UsuarioQueDisponibilizouParaTroca)
                .SingleAsync(troca => troca.Id == request.Id);

            var resultado = ObterTrocaResultado.CriarPor(livroDisponibilizado);

            return new AppResponse<ObterTrocaResultado>(true, "Dados obtidos com sucesso.", resultado);
        }
    }
}
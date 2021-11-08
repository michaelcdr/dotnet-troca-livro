using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasQueryHandler : IRequestHandler<ObterTrocasSolicitadasQuery, AppResponse<ObterTrocasSolicitadasResultado>>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper mapper;
        public ObterTrocasSolicitadasQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            this._db = context;
            this.mapper = mapper;
        }

        public async Task<AppResponse<ObterTrocasSolicitadasResultado>> Handle(ObterTrocasSolicitadasQuery request, CancellationToken cancellationToken)
        {
            List<Troca> trocas = await _db.Trocas.AsNoTracking().Where(trocaAtual =>
                 trocaAtual.Status == Dominio.Enums.StatusTroca.TrocaSolicitada &&
                 trocaAtual.UsuarioQueDisponibilizouParaTroca.UserName == request.UsuarioQueDisponibilizouTroca
            ).Include(trocaAtual => trocaAtual.Livro).ThenInclude(livroAtual => livroAtual.Imagens)
             .Include(trocaAtual => trocaAtual.UsuarioQueDisponibilizouParaTroca)
             .Include(trocaAtual => trocaAtual.UsuarioQueSolicitouTroca)
             .ToListAsync();

            var trocasSolicitadas = new List<TrocaSolicitadaViewModel>();

            foreach (Troca troca in trocas)
            {
                LivroCardModel livroCard = this.mapper.Map<Livro, LivroCardModel>(troca.Livro);

                trocasSolicitadas.Add(new TrocaSolicitadaViewModel(troca, livroCard));
            }

            var resultado = new ObterTrocasSolicitadasResultado(trocasSolicitadas);

            return new AppResponse<ObterTrocasSolicitadasResultado>(true, "Trocas obtidas com sucesso", resultado);
        }
    }
}
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
    public class ObterTrocasSolicitadasAoUsuarioLogadoQueryHandler : IRequestHandler<ObterTrocasSolicitadasAoUsuarioLogadoQuery, AppResponse<ObterTrocasSolicitadasAoUsuarioLogadoResultado>>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper mapper;
        public ObterTrocasSolicitadasAoUsuarioLogadoQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            this._db = context;
            this.mapper = mapper;
        }

        public async Task<AppResponse<ObterTrocasSolicitadasAoUsuarioLogadoResultado>> Handle(ObterTrocasSolicitadasAoUsuarioLogadoQuery request, CancellationToken cancellationToken)
        {
            List<Troca> trocas = await _db.Trocas.AsNoTracking().Where(trocaAtual =>
                 (
                    trocaAtual.Status == Dominio.Enums.StatusTroca.TrocaSolicitada ||
                    trocaAtual.Status == Dominio.Enums.StatusTroca.TrocaAprovada ||
                    trocaAtual.Status == Dominio.Enums.StatusTroca.LivroEnviado ||
                    trocaAtual.Status == Dominio.Enums.StatusTroca.LivroRecebido
                 ) &&
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

            var resultado = new ObterTrocasSolicitadasAoUsuarioLogadoResultado(trocasSolicitadas);

            return new AppResponse<ObterTrocasSolicitadasAoUsuarioLogadoResultado>(true, "Trocas obtidas com sucesso", resultado);
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasPeloUsuarioLogadoQueryHandler
        : IRequestHandler<ObterTrocasSolicitadasPeloUsuarioLogadoQuery, AppResponse<ObterTrocasSolicitadasPeloUsuarioLogadoResultado>>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ObterTrocasSolicitadasPeloUsuarioLogadoQueryHandler(ApplicationDbContext db, IMapper mapper)
        {
            this._mapper= mapper;
            this._db= db;
        }
        public async Task<AppResponse<ObterTrocasSolicitadasPeloUsuarioLogadoResultado>> Handle(ObterTrocasSolicitadasPeloUsuarioLogadoQuery request, CancellationToken cancellationToken)
        {
            List<Troca> trocas = await _db.Trocas.AsNoTracking().Where(trocaAtual =>
                 (
                    trocaAtual.Status == Dominio.Enums.StatusTroca.TrocaSolicitada ||
                    trocaAtual.Status == Dominio.Enums.StatusTroca.TrocaAprovada ||
                    trocaAtual.Status == Dominio.Enums.StatusTroca.LivroEnviado ||
                    trocaAtual.Status == Dominio.Enums.StatusTroca.LivroRecebido
                 ) &&
                 trocaAtual.UsuarioQueSolicitouTroca.UserName == request.UsuarioQueSolicitouTroca
            ).Include(trocaAtual => trocaAtual.Livro).ThenInclude(livroAtual => livroAtual.Imagens)
             .Include(trocaAtual => trocaAtual.UsuarioQueDisponibilizouParaTroca)
             .Include(trocaAtual => trocaAtual.UsuarioQueSolicitouTroca)
             .ToListAsync();

            var trocasViewModel = new List<TrocaSolicitadaViewModel>();

            foreach (Troca troca in trocas)
            {
                LivroCardModel livroCard = this._mapper.Map<Livro, LivroCardModel>(troca.Livro);

                trocasViewModel.Add(new TrocaSolicitadaViewModel(troca, livroCard));
            }
            
            var response = new ObterTrocasSolicitadasPeloUsuarioLogadoResultado(trocasViewModel);

            return new AppResponse<ObterTrocasSolicitadasPeloUsuarioLogadoResultado>(true,"Trocas obtidas com sucesso.",response);
        }
    }
}

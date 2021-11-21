using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class MarcarLivroComoRecebidoCommandHandler : IRequestHandler<MarcarLivroComoRecebidoCommand, AppCommandResponse>
    {
        private readonly ApplicationDbContext _db;

        public MarcarLivroComoRecebidoCommandHandler(ApplicationDbContext context)
        {
            this._db = context;
        }

        public async Task<AppCommandResponse> Handle(MarcarLivroComoRecebidoCommand request, CancellationToken cancellationToken)
        {
            Troca troca = await _db.Trocas.Include(trocaAtual => trocaAtual.UsuarioQueDisponibilizouParaTroca)
                .Include(trocaAtual => trocaAtual.UsuarioQueSolicitouTroca)
                .SingleAsync(trocaAtual => trocaAtual.Id == request.TrocaId);

            try
            {
                troca.MarcarComoRecebido();
                await _db.SaveChangesAsync();

                return new AppCommandResponse(true, "Livro marcado como recebido com sucesso");
            }
            catch (InvalidOperationException ex)
            {
                return new AppCommandResponse("Não foi possivel marcar o livro como recebido.", false, new List<Notificacao>
                {
                    new Notificacao(ex.Message, "Pontos")
                });
            }
        }
    }
}
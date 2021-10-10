using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class DisponibilizarLivroParaTrocaHandler : IRequestHandler<DisponibilizarLivroParaTrocaCommand, AppResponse<DisponibilizarLivroParaTrocaResultado>>
    {
        private readonly IUnitOfWork uow;
        public DisponibilizarLivroParaTrocaHandler(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public async Task<AppResponse<DisponibilizarLivroParaTrocaResultado>> Handle(
            DisponibilizarLivroParaTrocaCommand commando, 
            CancellationToken cancellationToken)
        {
            Usuario usuario = await uow.Usuarios.ObterPorLogin(commando.Usuario);

            var disponibilizacao = new LivroDisponibilizadoParaTroca
            {
                Descritivo = commando.Descritivo,
                DisponibilizadoEm = DateTime.Now,
                UsuarioQueDisponibilizouParaTrocaId = usuario.Id,
                Pontos = commando.Pontos,
                LivroId = commando.LivroId,
                Status = Dominio.Enums.StatusTroca.Disponibilizado
            };

            if (!disponibilizacao.TaValido())
                return new AppResponse<DisponibilizarLivroParaTrocaResultado>("Erro.", false, disponibilizacao.ObterErros());

            await uow.Livros.DisponibilizarParaTroca(disponibilizacao);

            await uow.CommitAsync();

            return new AppResponse<DisponibilizarLivroParaTrocaResultado>(
                true, 
                "Livro disponibilizado para troca com sucesso.",
                new DisponibilizarLivroParaTrocaResultado()
            );
        }
    }
}
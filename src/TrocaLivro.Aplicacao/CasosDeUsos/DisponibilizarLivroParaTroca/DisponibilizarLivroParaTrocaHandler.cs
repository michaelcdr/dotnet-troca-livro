using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Helpers;
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
            DisponibilizarLivroParaTrocaCommand comando, 
            CancellationToken cancellationToken)
        {
            Usuario usuario = await uow.Usuarios.ObterPorLogin(comando.Usuario);

            var troca = new Troca
            {
                Descritivo = comando.Descritivo,
                DisponibilizadoEm = DateTime.Now,
                UsuarioQueDisponibilizouParaTrocaId = usuario.Id,
                Pontos = comando.Pontos,
                LivroId = comando.LivroId,
                Status = Dominio.Enums.StatusTroca.Disponibilizado
            };

            if (comando.Imagens != null && comando.Imagens.Count > 0)
                foreach (IFormFile imagemFormFile in comando.Imagens)
                    troca.AdicionarImagem(new ImagemLivroEmTroca(FileHelper.ConvertToBytes(imagemFormFile)));

            if (!troca.TaValido())
                return new AppResponse<DisponibilizarLivroParaTrocaResultado>("Erro.", false, troca.ObterErros());

            await uow.Livros.DisponibilizarParaTroca(troca);

            await uow.CommitAsync();

            return new AppResponse<DisponibilizarLivroParaTrocaResultado>(
                true, 
                "Livro disponibilizado para troca com sucesso.",
                new DisponibilizarLivroParaTrocaResultado()
            );
        }
    }
}
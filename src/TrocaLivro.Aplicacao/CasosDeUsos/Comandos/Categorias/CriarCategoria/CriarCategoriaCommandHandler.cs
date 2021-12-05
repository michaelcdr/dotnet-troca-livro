using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarCategoriaCommandHandler : IRequestHandler<CriarCategoriaCommand, AppCommandResponse>
    {
        private readonly IUnitOfWork _uow;
        private const string MSG_ERRO = "Não foi possivel criar a categoria.";
        private const string MSG_SUCESSO = "Categoria criada com sucesso.";

        public CriarCategoriaCommandHandler(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<AppCommandResponse> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
        {
            Categoria categoria = new(request.Nome);

            bool existe = await _uow.Categorias.Existe(request.Nome);

            if (existe)
                return new AppCommandResponse(MSG_ERRO, false, new List<Notificacao> {
                    new Notificacao($"A categoria {request.Nome} já está cadastrada.","Nome")
                });

            if (!categoria.TaValido())
                return new AppCommandResponse(MSG_ERRO, false, categoria.ObterErros());

            _uow.Categorias.Add(categoria);
            await _uow.CommitAsync(); 

            return new AppCommandResponse(true, MSG_SUCESSO);
        }
    }
} 
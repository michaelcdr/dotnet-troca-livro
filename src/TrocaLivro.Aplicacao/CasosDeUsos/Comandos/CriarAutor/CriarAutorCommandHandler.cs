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
    public class CriarAutorCommandHandler : IRequestHandler<CriarAutorCommand, AppResponse<CriarAutorResultado>>
    {
        private const string MSG_ERRO = "Não foi possivel criar o autor.";
        private readonly IUnitOfWork _uow;

        public CriarAutorCommandHandler(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<AppResponse<CriarAutorResultado>> Handle(CriarAutorCommand request, CancellationToken cancellationToken)
        {
            Autor autor = new(request.Nome);

            bool existe = await _uow.Autores.Existe(request.Nome);

            if (existe)
                return new AppResponse<CriarAutorResultado>(MSG_ERRO, false, new List<Notificacao> {
                    new Notificacao($"O autor {request.Nome} já esta cadastrado.","Nome")
                });

            if (!autor.TaValido())
                return new AppResponse<CriarAutorResultado>(MSG_ERRO, false, autor.ObterErros());

            _uow.Autores.Add(autor);
            await _uow.CommitAsync();

            var resultado = new CriarAutorResultado(autor.Id);

            return new AppResponse<CriarAutorResultado>(true, "Autor criado com sucesso.", resultado); 
        }
    }
}
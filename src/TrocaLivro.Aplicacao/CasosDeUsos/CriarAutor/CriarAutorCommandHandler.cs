using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarAutorCommandHandler : IRequestHandler<CriarAutorCommand, AppResponse<CriarAutorResultado>>
    {
        private readonly IUnitOfWork _uow;

        public CriarAutorCommandHandler(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public async Task<AppResponse<CriarAutorResultado>> Handle(CriarAutorCommand request, CancellationToken cancellationToken)
        {
            Autor autor = new(request.Nome);

            if (!autor.TaValido())
                return new AppResponse<CriarAutorResultado>("Não foi possivel criar o autor.", false, autor.ObterErros());

            _uow.Autores.Add(autor);
            await _uow.CommitAsync();

            var resultado = new CriarAutorResultado(autor.Id);

            return new AppResponse<CriarAutorResultado>(true, "Autor criado com sucesso.", resultado); 
        }
    }
}
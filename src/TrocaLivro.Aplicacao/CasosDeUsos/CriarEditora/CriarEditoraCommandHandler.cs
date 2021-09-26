using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos.CriarEditora
{
    public class CriarEditoraCommandHandler : IRequestHandler<CriarEditoraCommand, AppResponse<CriarEditoraResultado>>
    {
        private readonly IUnitOfWork uow;

        public CriarEditoraCommandHandler(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public async Task<AppResponse<CriarEditoraResultado>> Handle(CriarEditoraCommand request, CancellationToken cancellationToken)
        {
            Editora editora = new Editora(request.Nome);

            if (!editora.TaValido())
                return new AppResponse<CriarEditoraResultado>("Não foi possivel criar a editora.", false, editora.ObterErros());

            uow.Editoras.Add(editora);
            await uow.CommitAsync();
            
            var resultado = new CriarEditoraResultado(editora.Id);

            return new AppResponse<CriarEditoraResultado>(true, "Editora criada com sucesso.", resultado);
        } 
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterTodasEditoras
{
    public class ObterTodasEditorasQueryHandler : IRequestHandler<ObterTodasEditorasQuery, AppResponse<ObterTodasEditorasResultado>>
    {
        private readonly IUnitOfWork uow;
        public ObterTodasEditorasQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<AppResponse<ObterTodasEditorasResultado>> Handle(ObterTodasEditorasQuery request, CancellationToken cancellationToken)
        {
            IList<Editora> editoras = await uow.Editoras.ObterTodos();

            List<EditoraDTO> editorasDto = editoras.Select(e => new EditoraDTO { Nome = e.Nome, Id = e.Id }).ToList();

            var response = new ObterTodasEditorasResultado(editorasDto);

            return new AppResponse<ObterTodasEditorasResultado>(true, "Editoras obtidas com sucesso.", response);
        }
    }
}

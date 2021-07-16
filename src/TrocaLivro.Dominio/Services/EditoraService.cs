using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Extensions;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Dominio.Services
{
    public class EditoraService : IEditoraService
    {
        private readonly IUnitOfWork unitOfWork;

        public EditoraService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<AppResponse<Editora>> Criar(EditoraRequest request)
        {
            Editora editora = new Editora(request.Nome);

            if (!editora.TaValido())
                return new AppResponse<Editora>("Não foi possivel criar a editora.", false, editora.ObterErros());

            unitOfWork.Editoras.Add(editora);
            await unitOfWork.CommitAsync();

            return new AppResponse<Editora>(true, "Editora criada com sucesso.", editora);
        }

        public async Task<AppResponse<IList<EditoraDTO>>> ObterTodas()
        {
            IList<Editora> editoras = await unitOfWork.Editoras.ObterTodos();

            List<EditoraDTO> editorasDto = editoras.Select(e => e.ToDTO()).ToList();
            
            return new AppResponse<IList<EditoraDTO>>(true, "Editoras obtidas com sucesso.", editorasDto);
        }
    }
}

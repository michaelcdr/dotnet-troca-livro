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
    public class AutorService : IAutorService
    {
        private readonly IUnitOfWork unitOfWork;

        public AutorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<AppResponse<Autor>> Criar(AutorRequest request)
        {
            Autor autor = new(request.Nome);

            if (!autor.TaValido())
                return new AppResponse<Autor>("Não foi possivel criar o autor.", false, autor.ObterErros());

            unitOfWork.Autores.Add(autor);
            await unitOfWork.CommitAsync();

            return new AppResponse<Autor>(true, "Autor criado com sucesso.", autor);
        }

        public async Task<AppResponse<IList<AutorDTO>>> ObterTodas()
        {
            IList<Autor> autores = await unitOfWork.Autores.ObterTodos();

            List<AutorDTO> autoresDto = autores.Select(e => e.ToDTO()).ToList();

            return new AppResponse<IList<AutorDTO>>(true, "Autores obtidas com sucesso.", autoresDto);
        }
    }
}
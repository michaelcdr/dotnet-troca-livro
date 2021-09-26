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

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterTodosAutores
{
    public class ObterTodosAutoresQueryHandler : IRequestHandler<ObterTodosAutoresQuery, AppResponse<ObterTodosAutoresResultado>>
    {
        private readonly IUnitOfWork uow;

        public ObterTodosAutoresQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<AppResponse<ObterTodosAutoresResultado>> Handle(ObterTodosAutoresQuery request, CancellationToken cancellationToken)
        {
            IList<Autor> autores = await uow.Autores.ObterTodos();

            List<AutorDTO> autoresDto = autores.Select(e => new AutorDTO { Id = e.Id, Nome = e.Nome }).ToList();

            var resultado = new ObterTodosAutoresResultado(autoresDto);

            return new AppResponse<ObterTodosAutoresResultado>(true, "Autores obtidas com sucesso.", resultado);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Extensions;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Dominio.Services
{
    public class LivroService : ILivroService
    {
        private readonly IUnitOfWork uow;
        private readonly IHostingEnvironment environment;

        public LivroService(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            this.uow = unitOfWork;
            this.environment = environment;
        }
         

        public async Task<AppResponse<LivroDTO>> Obter(int livroId)
        {
            Livro livro = await uow.Livros.Obter(livroId);

            return new AppResponse<LivroDTO>(true, "Livro obtido com sucesso.", livro.ToDTO());
        }

        public async Task<AppResponse<IList<LivroDTO>>> ObterTodos(ObterTodosLivrosRequest request)
        {
            IList<Livro> livros = new List<Livro>();

            if (!string.IsNullOrEmpty(request.TermoPesquisa))
                livros = await uow.Livros.PesquisarLivrosComAutores(e => e.Titulo.Contains(request.TermoPesquisa));
            else
                livros = await uow.Livros.ObterLivrosComAutores();

            return new AppResponse<IList<LivroDTO>>(true, "Livros obtidos com sucesso.", livros.Select(e => e.ToDTO()).ToList());
        }
    }
}
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

        public async Task<AppResponse<LivroDTO>> Criar(LivroRequest request)
        {
            Livro livro = request.ToLivro();
            livro.DataCadastro = DateTime.Now;
            livro.CadastradoPor = "michael";

            if (!livro.TaValido())
                return new AppResponse<LivroDTO>( "Livro criado com sucesso.", false, livro.ObterErros());

            uow.Livros.Add(livro);
            await uow.CommitAsync();

            Livro livroCriado = await uow.Livros.Obter(livro.Id);
            
            string diretorio = Path.Combine(environment.WebRootPath, "img", "livro");

            var imagens = new Imagem();

            if (request.Imagens.Count > 0) 
            {
                foreach (IFormFile imagemFormFile in request.Imagens)
                {
                    string extensao = Path.GetExtension(imagemFormFile.FileName);
                    string novoNome = Guid.NewGuid() + extensao;
                    string diretorioImg = Path.Combine(diretorio, imagemFormFile.FileName);
                    string diretorioImgComNomeNovo = Path.Combine(diretorio, novoNome);

                    livro.AdicionarImagem(new Imagem(livroCriado.Id, FileHelper.ConvertToBytes(imagemFormFile), 0, 0));
                }
                await uow.CommitAsync();
            }
            return new AppResponse<LivroDTO>(true, "Livro criado com sucesso.", livroCriado.ToDTO());
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
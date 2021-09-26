using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro
{
    public class CadastrarLivroCommandHandler : IRequestHandler<CadastrarLivroCommand, AppResponse<CadastrarLivroResultado>>
    {
        private const string ERRO_EXTENSAO = "As imagens devem estar no formato JPG";
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;
        private readonly IHostingEnvironment environment;
        private string msgSuccess = "Livro criado com sucesso.";

        public CadastrarLivroCommandHandler(IMapper mapper, IUnitOfWork uow, IHostingEnvironment env )
        {
            this.mapper = mapper;
            this.uow = uow;
            this.environment = env;
        }

        public async Task<AppResponse<CadastrarLivroResultado>> Handle(CadastrarLivroCommand commando, CancellationToken cancellationToken)
        {
            Livro livro = mapper.Map<Livro>(commando);
            livro.DataCadastro = DateTime.Now;
            livro.CadastradoPor = commando.Usuario;

            if (commando.Imagens != null && commando.Imagens.Count > 0)
                foreach (IFormFile imagemFormFile in commando.Imagens)
                    livro.AdicionarImagem(new Imagem(FileHelper.ConvertToBytes(imagemFormFile), 0, 0));

            if (!livro.TaValido())
                return new AppResponse<CadastrarLivroResultado>("Erro.", false, livro.ObterErros());

            if (await uow.Livros.VerificarExistencia(livro.ISBN))
                livro.AdicionarErro("Livro já cadastrado.", "");
            
            if (commando.Imagens.Count > 0) 
            { 
                bool contemImagensNaoJpg = commando.Imagens.Any(e => Path.GetExtension(e.FileName).ToLower() != ".jpg");

                if (contemImagensNaoJpg)
                    livro.AdicionarErro(ERRO_EXTENSAO, "Imagem");
            }
            
            if (livro.ObterErros().Count > 0)
                return new AppResponse<CadastrarLivroResultado>("Não foi possível cadastrar o livro.", false, livro.ObterErros());

            uow.Livros.Add(livro);
            await uow.CommitAsync();

            Livro livroCriado = await uow.Livros.Obter(livro.Id);
            CadastrarLivroResultado resultado = mapper.Map<CadastrarLivroResultado>(livro);
            
            return new AppResponse<CadastrarLivroResultado>(true, msgSuccess, resultado);
        }
    }
}

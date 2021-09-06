using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Extensions;
using AutoMapper;
using TrocaLivro.Dominio.Transacoes;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro
{
    public class CadastrarLivroCommandHandler : IRequestHandler<CadastrarLivroCommand, AppResponse<CadastrarLivroResultado>>
    {
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

            if (!livro.TaValido())
                return new AppResponse<CadastrarLivroResultado>("Erro.", false, livro.ObterErros());

            if (await uow.Livros.VerificarExistencia(livro.ISBN)) 
                return new AppResponse<CadastrarLivroResultado>("Não foi possível cadastrar o livro.", false, 
                    new List<Notificacao>() { new Notificacao("Livro já cadastrado.","") });

            uow.Livros.Add(livro);
            await uow.CommitAsync();

            Livro livroCriado = await uow.Livros.Obter(livro.Id);
            CadastrarLivroResultado resultado = mapper.Map<CadastrarLivroResultado>(livro);

            //string diretorio = Path.Combine(environment.WebRootPath, "img", "livro");
            //var imagens = new Imagem();

            if (commando.Imagens != null && commando.Imagens.Count > 0)
            {
                bool contemImagensNaoJpg = commando.Imagens.Any(e => Path.GetExtension(e.FileName).ToLower() != ".jpg");
                
                if (contemImagensNaoJpg)
                    return new AppResponse<CadastrarLivroResultado>(false, "As imagens devem estar no formato JPG");

                foreach (IFormFile imagemFormFile in commando.Imagens)
                {
                    //string extensao = Path.GetExtension(imagemFormFile.FileName);
                    //string novoNome = Guid.NewGuid() + extensao;
                    //string diretorioImg = Path.Combine(diretorio, imagemFormFile.FileName);
                    //string diretorioImgComNomeNovo = Path.Combine(diretorio, novoNome);
                    livro.AdicionarImagem(new Imagem(livroCriado.Id, FileHelper.ConvertToBytes(imagemFormFile), 0, 0));
                }
                await uow.CommitAsync();
            }
            return new AppResponse<CadastrarLivroResultado>(true, msgSuccess, resultado);
        }
    }
}

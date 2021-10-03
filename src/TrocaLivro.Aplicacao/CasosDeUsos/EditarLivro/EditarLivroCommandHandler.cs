using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos.EditarLivro
{
    public class EditarLivroCommandHandler : IRequestHandler<EditarLivroCommand, AppResponse<EditarLivroResultado>>
    {
        private const string msgErro = "Não foi possível atualizar o livro.";
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;
        private readonly IHostingEnvironment environment;
        private string msgSuccess = "Livro criado com sucesso.";

        public EditarLivroCommandHandler(IMapper mapper, IUnitOfWork uow, IHostingEnvironment env )
        {
            this.mapper = mapper;
            this.uow = uow;
            this.environment = env;
        }

        public async Task<AppResponse<EditarLivroResultado>> Handle(EditarLivroCommand commando, CancellationToken cancellationToken)
        {
            Livro livro = mapper.Map<Livro>(commando);
            livro.DataAlteracao = DateTime.Now;
            livro.AlteradoPor = commando.Usuario;

            if (!livro.TaValido())
                return new AppResponse<EditarLivroResultado>("Erro.", false, livro.ObterErros());

            if (await uow.Livros.VerificarExistencia(livro.ISBN,livro.Id)) 
                return new AppResponse<EditarLivroResultado>(msgErro, false, new List<Notificacao>() { new Notificacao("Livro já cadastrado.","") });

            if (commando.Imagens != null && commando.Imagens.Count > 0)
            {
                bool contemImagensNaoJpg = commando.Imagens.Any(e => Path.GetExtension(e.FileName).ToLower() != ".jpg");

                if (contemImagensNaoJpg)
                    return new AppResponse<EditarLivroResultado>(false, "As imagens devem estar no formato JPG");

                foreach (IFormFile imagemFormFile in commando.Imagens)
                {
                    livro.AdicionarImagem(new Imagem(commando.Id, FileHelper.ConvertToBytes(imagemFormFile), 0, 0));
                }
                await uow.CommitAsync();
            }

            await uow.CommitAsync();

            Livro livroCriado = await uow.Livros.Obter(livro.Id);
            EditarLivroResultado resultado = mapper.Map<EditarLivroResultado>(livro);

            
            return new AppResponse<EditarLivroResultado>(true, msgSuccess, resultado);
        }
    }
}

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
        private string msgSuccess = "Livro criado com sucesso.";

        public EditarLivroCommandHandler(IMapper mapper, IUnitOfWork uow )
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        public async Task<AppResponse<EditarLivroResultado>> Handle(EditarLivroCommand command, CancellationToken cancellationToken)
        {
            Livro livro = await uow.Livros.Obter(command.Id);

            livro.Atualizar(
                command.Titulo, command.Subtitulo, command.Descricao, command.ISBN, 
                (int)command.Ano, (int)command.NumeroPaginas, command.Usuario,
                command.AutorId, (int)command.EditoraId, (int)command.SubCategoriaId
            );
            
            List<int> idsImagensAtuais = await uow.Livros.ObterIdsImagens(livro.Id);

            if (command.Imagens != null && command.Imagens.Count > 0)
            {
                bool contemImagensNaoJpg = command.Imagens.Any(e => Path.GetExtension(e.FileName).ToLower() != ".jpg");

                if (contemImagensNaoJpg)
                    return new AppResponse<EditarLivroResultado>(false, "As imagens devem estar no formato JPG");

                foreach (IFormFile imagemFormFile in command.Imagens)
                    livro.AdicionarImagem(new Imagem(command.Id, FileHelper.ConvertToBytes(imagemFormFile), 0, 0));
            }

            if (!livro.TaValido())
                return new AppResponse<EditarLivroResultado>("Erro.", false, livro.ObterErros());

            if (await uow.Livros.VerificarExistencia(livro.ISBN,livro.Id)) 
                return new AppResponse<EditarLivroResultado>(msgErro, false, new List<Notificacao>() { new Notificacao("Livro já cadastrado.","") });

            List<int> idsImagens = command.ImagensAtuaisId == null 
                ? new List<int>() 
                : command.ImagensAtuaisId.Split(",").Select(idImagemAtual => int.Parse(idImagemAtual)).ToList();

            List<int> idsImagensRemovidas = idsImagensAtuais.Except(idsImagens).ToList();

            uow.Livros.Atualizar(livro);

            if (idsImagensRemovidas != null)
                uow.Livros.RemoverImagens(idsImagensRemovidas);

            await uow.CommitAsync();

            EditarLivroResultado resultado = mapper.Map<EditarLivroResultado>(livro);
            
            return new AppResponse<EditarLivroResultado>(true, msgSuccess, resultado);
        }
    }
}

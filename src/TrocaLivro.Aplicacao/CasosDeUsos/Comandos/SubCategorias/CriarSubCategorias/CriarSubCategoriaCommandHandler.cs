using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.Validators;
using TrocaLivro.Dominio;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarSubCategoriaCommandHandler : IRequestHandler<CriarSubCategoriaCommand, AppCommandResponse>
    {
        private readonly ApplicationDbContext _contexto;  
        public CriarSubCategoriaCommandHandler(ApplicationDbContext context)
        {
            this._contexto = context;
        }

        public async Task<AppCommandResponse> Handle(CriarSubCategoriaCommand comando, CancellationToken cancellationToken)
        {
            bool jaTemCadastrado = await _contexto.SubCategorias
                .AnyAsync(subCategoriaAtual => subCategoriaAtual.Nome == comando.Nome && subCategoriaAtual.CategoriaId == comando.CategoriaId);

            if (jaTemCadastrado)
                return new AppCommandResponse( "Não foi possivel cadastrar a SubCategoria informada.", false, new List<Notificacao> {
                    new Notificacao($"A sub categoria {comando.Nome} já esta cadastrada na categoria informada","Nome")
                });

            var validator = new CriarSubCategoriaCommandValidator().Validate(comando);

            if (!validator.IsValid)
            {
                var notificacoes = validator.Errors.Select(err => new Notificacao(err.ErrorMessage, err.PropertyName)).ToList();
                return new AppCommandResponse("Não foi possivel cadastrar a SubCategoria informada.", false, notificacoes);
            }

            this._contexto.SubCategorias.Add(new SubCategoria(comando.Nome, (int)comando.CategoriaId));
            await this._contexto.SaveChangesAsync();

            return new AppCommandResponse(true,"SubCategoria cadastrada com sucesso." );
        }
    }
}

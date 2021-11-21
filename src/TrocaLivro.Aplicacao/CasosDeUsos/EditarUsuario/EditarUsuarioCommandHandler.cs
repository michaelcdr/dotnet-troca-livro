using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class EditarUsuarioCommandHandler : IRequestHandler<EditarUsuarioCommand, AppCommandResponse>
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostingEnv;
        private const string MSGERRO = "Não foi possivel atualizar o usuario verfique os erros ocorridos.";

        public EditarUsuarioCommandHandler(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnv = hostingEnvironment;
        }

        public async Task<AppCommandResponse> Handle(
            EditarUsuarioCommand request, 
            CancellationToken cancellationToken)
        {
            var validator = new EditarUsuarioCommandValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                List<Notificacao> notificacaos = result.Errors
                    .Select(e => new Notificacao(e.ErrorMessage, e.PropertyName)).ToList();

                return new AppCommandResponse(MSGERRO, false, notificacaos);
            }
            else
            {
                Usuario usuario = await context.Usuarios.SingleAsync(u => u.Id == request.UsuarioId);

                string nomeAvatar = usuario.Avatar;
                if (request.Avatar != null && request.Avatar.Length > 0)
                {
                    var bytesImg = FileHelper.ConvertToBytes(request.Avatar);
                    nomeAvatar = Guid.NewGuid().ToString() + ".jpg";
                    using (var ms = new MemoryStream(bytesImg))
                    {
                        Image image = Image.FromStream(ms);
                        image.Save(Path.Combine(hostingEnv.WebRootPath, "Avatar", nomeAvatar));
                    }
                }

                usuario.Atualizar(request.Nome, request.Sobrenome, request.Email, nomeAvatar);
                await context.SaveChangesAsync();

                return new AppCommandResponse(true, "Usuário atualizado com sucesso.");
            }
        }
    }
}

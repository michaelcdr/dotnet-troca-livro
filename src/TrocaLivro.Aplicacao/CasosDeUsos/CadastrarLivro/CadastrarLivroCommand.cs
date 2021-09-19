using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro
{
    public class CadastrarLivroCommand : IRequest<AppResponse<CadastrarLivroResultado>>
    {
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int? EditoraId { get; set; }
        public List<int> AutorId { get; set; }

        public string ISBN { get; set; }

        public int? Ano { get; set; }

        public int? NumeroPaginas { get; set; }

        public string Subtitulo { get; set; }

        public int? SubCategoriaId { get; set; }
        public string Usuario { get; set; }

        public List<IFormFile> Imagens { get; set; }
        public CadastrarLivroCommand()
        {
            Imagens = new List<IFormFile>();
        }
    }
}

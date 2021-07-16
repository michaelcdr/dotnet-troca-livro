using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Requests
{
    public class LivroRequest
    {
        public LivroRequest()
        {

        }

        public LivroRequest(Livro livro)
        {
            this.EditoraId = livro.EditoraId;
            this.Ano = livro.Ano;
            this.ISBN = livro.ISBN;
            this.Titulo = livro.Titulo;
            this.Subtitulo = livro.Subtitulo;
            this.NumeroPaginas = livro.NumeroPaginas;
            this.Descricao = livro.Descricao;
            this.AutorId = livro.Autores.Select(e => e.AutorId).ToList();
            this.CategoriaId = livro.CategoriaId;
        }

        public string Descricao { get;  set; }
        public string Subtitulo { get; set; }
        public string ISBN { get;  set; }
        public string Titulo { get;  set; }
        public int NumeroPaginas { get;  set; }
        public int Ano { get;  set; }
        public int EditoraId { get; set; }
        public List<int> AutorId { get; set; }
        public List<IFormFile> Imagens { get; set; }
        public int CategoriaId { get; set; }
    }
}

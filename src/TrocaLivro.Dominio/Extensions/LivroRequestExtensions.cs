using System.Collections.Generic;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Requests;

namespace TrocaLivro.Dominio.Extensions
{
    public static class LivroRequestExtensions
    {
        public static Livro ToLivro(this LivroRequest livroRequest)
        {
            return new Livro(
                livroRequest.ISBN,
                livroRequest.Titulo,
                livroRequest.Descricao,
                livroRequest.NumeroPaginas,
                livroRequest.Ano,          
                livroRequest.AutorId,
                livroRequest.EditoraId,
                livroRequest.Subtitulo,livroRequest.CategoriaId);
        }
    }
}
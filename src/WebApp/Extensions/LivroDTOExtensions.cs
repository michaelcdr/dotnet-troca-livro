using TrocaLivro.Dominio.DTO;
using WebApp.Models;

namespace WebApp.Extensions
{
    public static class LivroDTOExtensions
    {
        public static LivroCard ToModel(this LivroDTO livro)
        {
            return new LivroCard(livro.Id,livro.Titulo,livro.Descricao,livro.CapaBase64);
        }
    }
}

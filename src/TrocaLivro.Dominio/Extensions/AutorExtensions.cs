using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Extensions
{
    public static class AutorExtensions
    {
        public static AutorDTO ToDTO(this Autor autor)
        {
            return new AutorDTO { Nome = autor.Nome, Id = autor.Id };
        }
    }
}
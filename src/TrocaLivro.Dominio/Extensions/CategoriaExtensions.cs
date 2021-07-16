using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Extensions
{
    public static class CategoriaExtensions
    {
        public static CategoriaDTO ToDTO(this Categoria categoria)
        {
            return new CategoriaDTO(categoria.Id, categoria.Nome);
        }
    }
}
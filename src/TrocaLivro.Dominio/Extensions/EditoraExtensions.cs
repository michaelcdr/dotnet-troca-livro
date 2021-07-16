using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Extensions
{
    public static class EditoraExtensions
    {
        public static EditoraDTO ToDTO(this Editora editora)
        {
            return new EditoraDTO { Nome = editora.Nome, Id = editora.Id };
        }
    }
}

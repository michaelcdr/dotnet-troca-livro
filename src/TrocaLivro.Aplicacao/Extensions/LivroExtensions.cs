using Microsoft.AspNetCore.Http;
using System.IO;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.Extensions
{
    public static class LivroExtensions
    {
        public static LivroDTO ToDTO(this Livro livro)
        {
            return new LivroDTO(livro);
        }

        public static byte[] ConvertToBytes(this IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            using (var inputStream = image.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}
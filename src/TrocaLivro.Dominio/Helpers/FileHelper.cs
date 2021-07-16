using Microsoft.AspNetCore.Http;
using System.IO;

namespace TrocaLivro.Dominio.Helpers
{
    public static class FileHelper
    {
        public static byte[] ConvertToBytes(IFormFile image)
        {
            if (image == null) return null;

            using (var inputStream = image.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}

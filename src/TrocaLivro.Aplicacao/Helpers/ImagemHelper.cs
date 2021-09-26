using System;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.Helpers
{
    public static class ImagemHelper
    {
        public static string ToBase64(ImagemCardLivro imagem)
        {
            if (imagem.Nome == null) return string.Empty;

            int imgLength = imagem.Nome.Length;
            var imgData = imagem.Nome;
            string base64String = "data:image/jpg;base64," + Convert.ToBase64String(imgData, 0, imgLength);
            return base64String;
        }
    }
}

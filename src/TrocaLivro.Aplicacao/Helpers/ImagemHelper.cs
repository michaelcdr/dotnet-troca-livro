using System;
using System.Linq;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.Helpers
{
    public static class ImagemHelper
    {
        public static string ToBase64(Imagem imagem)
        {
            
            int imgLength = imagem.Nome.Length;
            var imgData = imagem.Nome;
            string base64String = "data:image/jpg;base64," + Convert.ToBase64String(imgData, 0, imgLength);
            return base64String;
        }
    }
}

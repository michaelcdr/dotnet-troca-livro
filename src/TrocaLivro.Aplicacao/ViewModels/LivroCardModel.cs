using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.Helpers;

namespace TrocaLivro.Aplicacao.ViewModels
{
    public class LivroCardModel
    {
        public int Id { get; set; }
        public List<ImagemCardLivro> Imagens { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public string DescricaoFormatadaParaCard 
        {
            get { return Descricao.Length > 150 ? Descricao.Substring(0,150) + "..." : Descricao; }
        }

        public string Imagem
        { 
            get { 
                if (this.Imagens != null && this.Imagens.Count > 0)
                    return ImagemHelper.ToBase64(this.Imagens.First());

                return string.Empty;
            }
        }
    }
}
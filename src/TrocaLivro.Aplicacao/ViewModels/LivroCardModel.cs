using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.Helpers;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.ViewModels
{
    public class LivroCardModel
    {
        public int Id { get; set; }
        public List<Imagem> Imagens { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public string Imagem { 
            get { 
                return ImagemHelper.ToBase64(this.Imagens.First()); 
            }
        }
    }
}
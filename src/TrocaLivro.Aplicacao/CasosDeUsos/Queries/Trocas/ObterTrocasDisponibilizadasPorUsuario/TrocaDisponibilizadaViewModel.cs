using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class TrocaDisponibilizadaViewModel
    {
        public int TrocaId { get; set; }
        public LivroCardModel Livro { get; set; }
        public int Pontos { get; set; }
        public TrocaDisponibilizadaViewModel()
        {

        }
        public TrocaDisponibilizadaViewModel(int trocaId, int pontos, LivroCardModel livroCardModel)
        {
            this.Livro = livroCardModel;
            this.TrocaId = trocaId;
            this.Pontos = pontos;
        }
        public string ObterPontos()
        {
            return this.Pontos > 1 ? $"{this.Pontos} Pontos" : $"{this.Pontos} Ponto";
        }
    }
}

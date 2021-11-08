using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class TrocaSolicitadaViewModel
    {
        public int TrocaId { get; set; }
        public string Solicitante { get; set; }
        public string Disponibilizador { get; set; }
        public LivroCardModel Livro { get; set; }
        public int Pontos { get; set; }
        public TrocaSolicitadaViewModel()
        {

        }
        public TrocaSolicitadaViewModel(Troca trocaAtual, LivroCardModel livroCardModel)
        {
            Livro = livroCardModel;
            Pontos = trocaAtual.Pontos;
            Disponibilizador = trocaAtual.UsuarioQueDisponibilizouParaTroca.ObterNomeCompleto();
            Solicitante = trocaAtual.UsuarioQueSolicitouTroca.ObterNomeCompleto();
            TrocaId = trocaAtual.Id;
        }
        public string ObterPontos()
        {
            return this.Pontos > 1 ? $"{this.Pontos} Pontos" : $"{this.Pontos} Ponto";
        }
    }
}

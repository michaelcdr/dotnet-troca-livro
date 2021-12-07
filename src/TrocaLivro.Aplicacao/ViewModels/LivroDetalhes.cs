using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.DTO;

namespace TrocaLivro.Aplicacao.ViewModels
{
    public class LivroDetalhes
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int NumeroPagina { get;  set; }
        public bool DisponibilizarParaTroca { get; set; }
        public string LoginUsuarioLogado { get; set; }
        public string Autores { get;  set; }
        public string ISBN { get;  set; }
        public string Editora { get; set; }

        public List<UsuarioOfertando> Usuarios { get; set; }
        public int Ano { get;  set; }
        public string Capa { get;  set; }
        public bool PodeEditar { get; set; }
        public bool PodeAvaliar { get; set; }
        public bool PodeSolicitarTroca { get; set; }
        public List<AvaliacaoLivro> Avaliacoes { get; set; }
        public LivroDetalhes()
        {
            
        }

        /// <summary>
        /// Gera um objeto LivroDetalhes usando um objeto LivroDTO
        /// </summary>
        /// <param name="livro">Objeto do tipo LivroDTO</param>
        /// <returns></returns>
        public static LivroDetalhes GerarPorLivroDTO(LivroDTO livro)
        {
            return new LivroDetalhes
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Ano = livro.Ano,
                Autores = string.Join(", ", livro.Autores.Select(e => e.Nome).ToList()),
                Descricao = livro.Descricao.Replace("\n", "<br />"),
                NumeroPagina = livro.NumeroPaginas,
                ISBN = livro.ISBN,
                Editora = livro.NomeEditora,
                Capa = livro.CapaBase64,
                DisponibilizarParaTroca = false,
                Usuarios = livro.UsuariosOfertando ?? new List<UsuarioOfertando>(),
                Avaliacoes = livro.Avaliacoes ?? new List<AvaliacaoLivro>()
            };
        }

        public decimal CalcularAvaliacao()
        {
            if (this.Avaliacoes.Count == 0)
                return 0;

            decimal total= this.Avaliacoes.Sum(e=>e.Nota.GetHashCode());

            return (decimal)total / (decimal)this.Avaliacoes.Count;
        }

        public string ObterAvaliacaoFormatada()
        {
            return "" + this.CalcularAvaliacao().ToString("N1") + " de 5";
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.DTO;

namespace WebApp.Models
{
    public class LivroDetalhes
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int NumeroPagina { get;  set; }

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
                Descricao = livro.Descricao.Replace("\n","<br />"),
                NumeroPagina = livro.NumeroPaginas,
                ISBN = livro.ISBN,
                Editora = livro.NomeEditora,
                Capa = livro.CapaBase64
            };
        }

        public string Autores { get;  set; }
        public string ISBN { get;  set; }
        public string Editora { get;  set; }

        public List<UsuarioOfertando> Usuarios { get; set; }
        public int Ano { get;  set; }
        public string Capa { get;  set; }
        public bool PodeEditar { get; set; }

        public LivroDetalhes()
        {
            Usuarios = new List<UsuarioOfertando>()
            {
                new UsuarioOfertando(){ Nome = "Fulano", LivrosEnviados = 10 },
                new UsuarioOfertando(){ Nome = "Fulana", LivrosEnviados = 20 },
                new UsuarioOfertando(){ Nome = "Ciclano", LivrosEnviados = 10 },
            };
        }
    }
}

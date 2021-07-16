using System;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Dominio.DTO;

namespace WebApp.Models
{
    public class LivroCard
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Capa { get; private set; } 

        public LivroCard(int livroId, string titulo, string descricao, string capaBase64)
        {
            this.Id = livroId;
            this.Titulo = titulo;
            this.Capa = capaBase64;
            this.Descricao = FormatarDescricao(descricao);
        }

        private string FormatarDescricao(string descricaoCompleta) 
        {
            int totalCaracteres = 100;

            if (descricaoCompleta.Length < 100)
                totalCaracteres = descricaoCompleta.Length;

            return descricaoCompleta.Substring(0, totalCaracteres);
        }
    }

    public class LivroDetalhes
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int NumeroPagina { get;  set; }

        public static LivroDetalhes CriarUsandoLivro(LivroDTO livro)
        {
            return new LivroDetalhes
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Ano = livro.Ano,
                Autores = string.Join(", ", livro.Autores.Select(e => e.Nome).ToList()),
                Descricao = livro.Descricao,
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

    public class UsuarioOfertando
    {
        public string Nome { get; set; }
        public int LivrosEnviados { get; set; }
    }
}

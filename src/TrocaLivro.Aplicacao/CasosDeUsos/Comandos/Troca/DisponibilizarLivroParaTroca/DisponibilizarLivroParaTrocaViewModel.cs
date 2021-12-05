using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.DTO;

namespace TrocaLivro.Aplicacao.ViewModels
{
    public class DisponibilizarLivroParaTrocaViewModel
    {
        public int LivroId { get; private set; }
        public string Descritivo { get; set; }
        public int? Pontos { get; set; }
        public List<IFormFile> Imagens { get; set; }

        public string Titulo { get; private set; }
        public string Imagem { get; private set; }
        public string Autores { get; private set; }
        public string ISBN { get; private set; }
        public int Ano { get; private set; }
        public int NumeroPagina { get; private set; }
        public int MaximoDePontos { get; private set; }

        


        public DisponibilizarLivroParaTrocaViewModel(LivroDTO livroDTO)
        {
            this.Imagem = livroDTO.Imagens.First().ImagemBase64;
            this.Titulo = livroDTO.Titulo;
            this.LivroId = livroDTO.Id;
            this.ISBN = livroDTO.ISBN;
            this.Ano = livroDTO.Ano;
            this.NumeroPagina = livroDTO.NumeroPaginas;
            this.MaximoDePontos = 3;
            this.Autores = string.Join(",", livroDTO.Autores.Select(e => e.Nome).ToList());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.DTO
{
    public class LivroDTO
    {
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
        public int NumeroPaginas { get; set; }
        public int Ano { get; set; }
        public int EditoraId { get; set; }
        public List<LivroAutorDTO> Autores { get; set; }
        public int Id { get; set; }
        public string NomeEditora { get; set; }
        public int CategoriaId { get; set; }
        public int SubCategoriaId { get; set; }
        public string CapaBase64 { get; set; }
        public List<ImagemDTO> Imagens { get; set; }
        public string CadastradoPor { get; set; }
        public List<UsuarioOfertando> UsuariosOfertando { get; set; }
        public List<AvaliacaoLivro> Avaliacoes { get; set; }
        public LivroDTO()
        {
            Imagens = new List<ImagemDTO>();
        }

        public LivroDTO(Livro livro)
        {
            Imagens = new List<ImagemDTO>();

            Id = livro.Id; 
            Ano = livro.Ano; 
            Descricao = livro.Descricao; 
            EditoraId = livro.EditoraId; 
            ISBN = livro.ISBN; 
            NumeroPaginas = livro.NumeroPaginas;
            Titulo = livro.Titulo;
            Subtitulo = livro.Subtitulo;
            CadastradoPor = livro.CadastradoPor;

            if (livro.SubCategoria != null)
                CategoriaId = livro.SubCategoria.CategoriaId;

            SubCategoriaId = livro.SubCategoriaId;

            if (livro.Editora != null )
                NomeEditora = livro.Editora.Nome;
            
            if (livro.Autores != null)
                Autores = livro.Autores.Select(e => new LivroAutorDTO(e.AutorId, e.Autor.Nome)).ToList();

            if (livro.Imagens.Count > 0)
            {
                Imagem img = livro.Imagens.Where(e => e.Nome != null).First();
                int imgLength = img.Nome.Length;
                var imgData = img.Nome;
                string base64String = "data:image/jpg;base64," + Convert.ToBase64String(imgData, 0, imgLength);

                this.CapaBase64 = base64String;

                foreach (Imagem imagem in livro.Imagens)
                { 
                    string base64Imagem = "data:image/jpg;base64," + Convert.ToBase64String(imagem.Nome, 0, imagem.Nome.Length);

                    this.Imagens.Add(new ImagemDTO { ImagemBase64 = base64Imagem, Id = imagem.Id });
                }
            }

            if (livro.DiponibilizacaoParaTrocas.Count > 0)
            {
                this.UsuariosOfertando = livro.DiponibilizacaoParaTrocas.Select(e => new UsuarioOfertando
                {
                    DisponibilizacaoTrocaId = e.Id,
                    Nome = e.UsuarioQueDisponibilizouParaTroca.Nome,
                    Pontos = e.Pontos,
                    LivrosEnviados = 0,
                    UserName = e.UsuarioQueDisponibilizouParaTroca.UserName
                }).ToList();
            }

            if (livro.Avaliacoes.Count > 0)
                this.Avaliacoes = livro.Avaliacoes
                    .Select(e => new AvaliacaoLivro(e.Titulo,e.Descricao,e.Nota,e.AvaliadoEm,e.Usuario.Nome))
                    .OrderByDescending(e => e.Data)
                    .ToList();
        }
    }
}
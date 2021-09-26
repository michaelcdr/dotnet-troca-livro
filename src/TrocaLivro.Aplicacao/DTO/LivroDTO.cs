using System;
using System.Collections.Generic;
using System.Linq;
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
        public byte[] Capa { get; set; }
        public int CategoriaId { get; set; }
        public int SubCategoriaId { get; set; }
        public string CapaBase64 { get; set; }

        public LivroDTO()
        {

        }

        public LivroDTO(Livro livro)
        {
            Id = livro.Id; 
            Ano = livro.Ano; 
            Descricao = livro.Descricao; 
            EditoraId = livro.EditoraId; 
            ISBN = livro.ISBN; 
            NumeroPaginas = livro.NumeroPaginas;
            Titulo = livro.Titulo;
            Subtitulo = livro.Subtitulo;

            if (livro.SubCategoria != null)
                CategoriaId = livro.SubCategoria.CategoriaId;

            SubCategoriaId = livro.SubCategoriaId;

            if (livro.Editora != null )
                NomeEditora = livro.Editora.Nome;
            
            if (livro.Autores != null)
                Autores = livro.Autores.Select(e => new LivroAutorDTO { AutorId = e.AutorId, Nome = e.Autor.Nome }).ToList();

            if (livro.Imagens.Count > 0)
            {
                Imagem img = livro.Imagens.Where(e => e.Nome != null).First();
                int imgLength = img.Nome.Length;
                var imgData = img.Nome;
                string base64String = "data:image/jpg;base64," + Convert.ToBase64String(imgData, 0, imgLength);

                this.CapaBase64 = base64String;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.DTO
{
    public class LivroDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ISBN { get; set; }
        public int NumeroPaginas { get; set; }
        public int Ano { get; set; }
        public int EditoraId { get; set; }
        public List<LivroAutorDTO> Autores { get; set; }
        public int Id { get; set; }
        public string NomeEditora { get; set; }
        public byte[] Capa { get; set; }

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
            if (livro.Editora != null )
                NomeEditora = livro.Editora.Nome;
            
            if (livro.Autores != null)
            {
                Autores = livro.Autores.Select(e => new LivroAutorDTO { AutorId = e.AutorId, Nome = e.Autor.Nome }).ToList();
            }

            if (livro.Imagens.Count > 0)
            {
                string base64String = "data:image/png;base64," + Convert.ToBase64String(
                    livro.Imagens.First().Nome, 0, livro.Imagens.First().Nome.Length
                );
                this.CapaBase64 = base64String;
            }
        }
    }
}
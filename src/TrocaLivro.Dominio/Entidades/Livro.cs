using System;
using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Livro : EntidadeBase
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Subtitulo { get; set; }
        public string Descricao { get; private set; }
        public string ISBN { get; private set; }
        public int Ano { get; private set; }
        public int NumeroPaginas { get; private set; }
        public List<LivroAutor> Autores { get; private set; }
        public DateTime DataCadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string AlteradoPor { get; set; }
        public Editora Editora { get; private set; }

       
        public int EditoraId { get; private set; }
        public string Tags { get; set; }
        public List<Imagem> Imagens { get; set; }
        public List<Arquivo> Arquivos { get; set; }
        public List<LivroDisponibilizadoParaTroca> Trocas { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public int SubCategoriaId { get; set; }

        public bool Deletado { get; private set; }
        public string DeletadoPor { get; private set; }
        public DateTime? DataDaDelecao { get; private set; }

        public Livro() 
        {
            this.Autores = new List<LivroAutor>();
            this.Imagens = new List<Imagem>();
            this.Arquivos = new List<Arquivo>();
            this.Trocas = new List<LivroDisponibilizadoParaTroca>();
        }

        public void Deletar(string usuario)
        {
            this.Deletado = true;
            this.DeletadoPor = usuario;
            this.DataDaDelecao = DateTime.Now;
            this.DataAlteracao = DateTime.Now;
            this.AlteradoPor = usuario;
        }

        private void AdicionarAutor(int idAutor)
        {
            if (this.Autores == null)
                this.Autores = new List<LivroAutor>();

            this.Autores.Add(new LivroAutor { AutorId = idAutor });
        }

        public override bool TaValido()
        {
            if (this.Autores.Count == 0)
                this.AdicionarErro("Informe o autor", "Autores");

            if (this.EditoraId == 0)
                this.AdicionarErro("Informe a editora", "EditoraId");

            if (string.IsNullOrEmpty(this.Titulo))
                this.AdicionarErro("Informe o titulo", "Titulo");

            if (string.IsNullOrEmpty(this.Descricao))
                this.AdicionarErro("Informe o descrição", "Descricao");
            
            if (this.Imagens == null || this.Imagens.Count == 0)
                this.AdicionarErro("Você deve conter ao menos uma imagem.", "");

            return this._erros.Count == 0;
        }

        public void AdicionarImagem(Imagem imagem)
        {
            if (this.Imagens == null)
                this.Imagens = new List<Imagem>();

            this.Imagens.Add(imagem);
        }
    }
}
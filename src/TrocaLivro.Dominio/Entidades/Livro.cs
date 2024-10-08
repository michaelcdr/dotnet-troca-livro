﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TrocaLivro.Dominio.Entidades
{
    public class Livro : Entidade
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Subtitulo { get; private set; }
        public string Descricao { get; private set; }
        public string ISBN { get; private set; }
        public int Ano { get; private set; }
        public int NumeroPaginas { get; private set; }
        public List<LivroAutor> Autores { get; private set; }
        public Editora Editora { get; private set; }
        public int EditoraId { get; private set; }
        public string Tags { get; set; }
        public List<Imagem> Imagens { get; set; }
        public List<Arquivo> Arquivos { get; set; }
        public List<Avaliacao> Avaliacoes { get; private set; }
        public List<Troca> DiponibilizacaoParaTrocas { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public int SubCategoriaId { get; set; }

        public DateTime DataCadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string AlteradoPor { get; set; }

        public bool Deletado { get; private set; }
        public string DeletadoPor { get; private set; }
        public DateTime? DataDaDelecao { get; private set; }

        public Livro()
        {
            Autores = new List<LivroAutor>();
            Imagens = new List<Imagem>();
            Arquivos = new List<Arquivo>();
            Avaliacoes = new List<Avaliacao>();
            DiponibilizacaoParaTrocas = new List<Troca>();
        }

        public void AdicionarAutores(List<LivroAutor> livroAutors)
        {
            this.Autores.AddRange(livroAutors);
        }

        public void Atualizar(string titulo, string subtitulo, string descricao, string isbn, 
                              int ano, int numeroPaginas, 
                              string alteradoPor, List<int> idsAutores, int editoraId, int subCategoriaId)
        {
            Titulo = titulo;
            Subtitulo = subtitulo;
            Descricao = descricao;
            ISBN = isbn;
            Ano = ano;
            NumeroPaginas = numeroPaginas;
            DataAlteracao = DateTime.Now;
            AlteradoPor = alteradoPor;
            SubCategoriaId = subCategoriaId;
            EditoraId = editoraId;

            List<int> autoresAtuais = this.Autores.Select(e => e.AutorId).ToList();

            List<int> autoresRemovidos = autoresAtuais.Except(idsAutores).ToList();

            if (autoresRemovidos != null)
                foreach (var autorIdRemovido in autoresRemovidos)
                {
                    LivroAutor livroAutor = this.Autores.Single(a => a.AutorId == autorIdRemovido);
                    this.Autores.Remove(livroAutor);
                }

            foreach (var autorIdSelecionado in idsAutores)
                if (!this.Autores.Any(e => e.AutorId == autorIdSelecionado))
                    this.Autores.Add(new LivroAutor { LivroId = this.Id, AutorId = autorIdSelecionado });
        }

        public string ObterCapaEmBase64()
        {
            Imagem img = this.Imagens.Where(e => e.Nome != null).First();
            int imgLength = img.Nome.Length;
            var imgData = img.Nome;
            string base64String = "data:image/jpg;base64," + Convert.ToBase64String(imgData, 0, imgLength);
            return base64String;
        }

        public void AdicionarEditora(Editora editora)
        {
            this.Editora = editora;
        }

        public void Deletar(string usuario)
        {
            this.Deletado = true;
            this.DeletadoPor = usuario;
            this.DataDaDelecao = DateTime.Now;
            this.DataAlteracao = DateTime.Now;
            this.AlteradoPor = usuario;
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

        public void AdicionarImagens(List<Imagem> imagems)
        {
            foreach (Imagem imagem in imagems)
                AdicionarImagem(imagem);
        }

        public void AdicionarImagem(Imagem imagem)
        {
            if (this.Imagens == null)
                this.Imagens = new List<Imagem>();

            this.Imagens.Add(imagem);
        }
    }
}
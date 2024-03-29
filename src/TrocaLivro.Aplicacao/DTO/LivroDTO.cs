﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Aplicacao.Helpers;
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
                this.CapaBase64 = livro.ObterCapaEmBase64();

                foreach (Imagem imagem in livro.Imagens)
                { 
                    string base64Imagem = "data:image/jpg;base64," + Convert.ToBase64String(imagem.Nome, 0, imagem.Nome.Length);

                    this.Imagens.Add(new ImagemDTO { ImagemBase64 = base64Imagem, Id = imagem.Id });
                }
            }

            if (livro.DiponibilizacaoParaTrocas.Count(e=>e.Status == Dominio.Enums.StatusTroca.Disponibilizado) > 0)
            {
                this.UsuariosOfertando = livro.DiponibilizacaoParaTrocas
                    .Where(e => e.Status == Dominio.Enums.StatusTroca.Disponibilizado)
                    .Select(e => new UsuarioOfertando
                {
                    DisponibilizacaoTrocaId = e.Id,
                    Nome = e.UsuarioQueDisponibilizouParaTroca.Nome,
                    Pontos = e.Pontos,
                    UserName = e.UsuarioQueDisponibilizouParaTroca.UserName,
                    Avatar = AmbienteConfigHelper.ObterDiretorioAvatar(e.UsuarioQueDisponibilizouParaTroca.Avatar)
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
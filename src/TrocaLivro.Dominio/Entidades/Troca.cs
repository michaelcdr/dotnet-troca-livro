using System;
using System.Collections.Generic;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Dominio.Entidades
{
    public class Troca : EntidadeBase
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int Pontos { get; set; }
        public DateTime DisponibilizadoEm { get; set; }
        public string UsuarioQueDisponibilizouParaTrocaId { get; set; }
        public Usuario UsuarioQueDisponibilizouParaTroca { get; set; }
        public StatusTroca Status { get; set; }
        public string Descritivo { get; set; }
        public List<ImagemLivroEmTroca> Imagens { get; private set; }
        public Usuario UsuarioQueSolicitouTroca { get; set; }
        public string UsuarioQueSolicitouTrocaId { get; set; }
        public DateTime? DataSolicitacaoTroca { get; private set; }
        public DateTime? DataAprovacaoTroca { get; private set; }
        public DateTime? DataEnvio { get; private set; }
        public DateTime? DataRecebimento { get; private set; }

        public int? EnderecoId { get; private set; }

        public void MarcarComoEnviado()
        {
            this.DataEnvio = DateTime.Now;
            this.Status = StatusTroca.LivroEnviado;
        }

        public void MarcarComoRecebido()
        {
            this.UsuarioQueSolicitouTroca.DebitarPontos(this.Pontos);
            this.UsuarioQueDisponibilizouParaTroca.AdicionarPontos(this.Pontos);
            this.DataRecebimento = DateTime.Now;
            this.Status = StatusTroca.LivroRecebido;
        }

        public Endereco Endereco { get; set; }

        public Troca()
        {
            Imagens = new List<ImagemLivroEmTroca>();
        }

        public void Aprovar()
        {
            this.Status = StatusTroca.TrocaAprovada;
            this.DataAprovacaoTroca = DateTime.Now;
        }

        public void AdicionarImagem(ImagemLivroEmTroca imagem)
        {
            if (this.Imagens == null)
                this.Imagens = new List<ImagemLivroEmTroca>();

            this.Imagens.Add(imagem);
        }

        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(Descritivo))
                this.AdicionarErro("Informe o descritivo",nameof(Descritivo));

            if (this.Pontos <= 0 || this.Pontos > 3)
                this.AdicionarErro("Numero de pontos inválidos, deve ser > 0 e <= 3", nameof(Pontos));

            if (this.LivroId == 0)
                this.AdicionarErro("Informe o livro", nameof(LivroId));

            return this._erros.Count == 0;
        }

        public void AdicionarEnderecoDeEntrega(int id)
        {
            this.EnderecoId = id;
        }

        public void MarcarComoTrocaSolicitada(string usuarioQueSolicitouTrocaId)
        {
            this.Status = StatusTroca.TrocaSolicitada;
            this.UsuarioQueSolicitouTrocaId = usuarioQueSolicitouTrocaId;
            this.DataSolicitacaoTroca = DateTime.Now;
        }
    }
}
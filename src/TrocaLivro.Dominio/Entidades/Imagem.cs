﻿namespace TrocaLivro.Dominio.Entidades
{
    public class Imagem : Entidade
    {
        public int Id { get; set; }
        public int LivroId { get; private set; }        
        public byte[] Nome { get; private set; }
        public int Altura { get; private set; }
        public int Largura { get; private set; }
        public Livro Livro { get; private set; }

        protected Imagem() { }

        public Imagem(byte[] nome, int altura, int largura)
        {
            this.Nome = nome;
            this.Altura = altura;
            this.Largura = largura;
        }

        public Imagem(int livroId, byte[] nome, int altura, int largura)
        {
            this.LivroId = livroId;
            this.Nome = nome;
            this.Altura = altura;
            this.Largura = largura;
        }

        public override bool TaValido()
        {
            return true;
        }
    }
}
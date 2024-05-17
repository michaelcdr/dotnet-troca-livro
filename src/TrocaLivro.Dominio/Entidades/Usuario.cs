using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public int Pontos { get; private set; }
        public string Avatar { get; private set; }
        public List<Troca> TrocasDisponibilizadas { get; set; }
        public List<Troca> TrocasSolicitadas { get; set; }
        public List<Endereco> Endereco { get; private set; }
        public List<Avaliacao> AvaliacoesFeitas { get; private set; }
        
        private List<Notificacao> _erros { get; set; }

        public Usuario(string nome, string userName, string email, string sobrenome)
        {
            this.Nome = nome;
            this.UserName = userName;
            this.Email = email;
            this.Sobrenome = sobrenome;
            this.TrocasDisponibilizadas = new List<Troca>();
            this.TrocasSolicitadas = new List<Troca>();
            this.Endereco = new List<Endereco>();
            this.AvaliacoesFeitas = new List<Avaliacao>();
        }

        public void Atualizar(string nome, string sobrenome, string email, string avatar)
        {
            this.Nome = nome;
            this.Email = email;
            this.Sobrenome = sobrenome;
            this.Avatar = avatar;
        }

        public void AdicionarEndereco(string usuarioId, 
                                      string bairro, 
                                      string cEP, 
                                      string complemento, 
                                      string uF, 
                                      string logradouro, 
                                      int numero, 
                                      string cidade)
        {
            this.Endereco.Add(new Endereco(
                usuarioId, 
                bairro, 
                cEP, 
                complemento, 
                uF, 
                logradouro, 
                numero, 
                cidade
            ));
        }

        public void DebitarPontos(int pontos)
        {
            if (this.Pontos < pontos)
                throw new InvalidOperationException("Você não tem pontos suficiente");

            this.Pontos -= pontos;
        }

        public void AdicionarPontos(int pontos)
        {
            this.Pontos += pontos;
        }

        public string ObterNomeCompleto()
        {
            return this.Nome + " " + this.Sobrenome;
        }

        public void AvaliarLivro(int livroId,string titulo, string descricao, NotaLivroEnum nota)
        {
            AvaliacoesFeitas.Add(new Avaliacao(
                livroId,
                this.Id,
                titulo,
                descricao,
                nota
            ));
        }

        public List<Notificacao> ObterErros()
        {
            return this._erros;
        }

        public bool TaValido()
        {
            bool retorno = true;

            this._erros = new List<Notificacao>();

            if (string.IsNullOrEmpty(Nome))
            {
                _erros.Add(new Notificacao("Nome não informado.", "Nome"));
                retorno = false;
            }

            if (string.IsNullOrEmpty(UserName))
            {
                _erros.Add(new Notificacao("UserName não informado.", ""));
                retorno = false;
            }

            return retorno;
        }
    }
}

using System;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocaResultado
    {
        public ObterTrocaResultado(
            int disponibilizacaoTrocaId, int pontos, StatusTroca status, string descritivo, string disponibilizadoPor, 
            int livroId, string tituloLivro, DateTime disponibilizadoEm, string capa)
        {
            DisponibilizacaoTrocaId = disponibilizacaoTrocaId;
            Pontos = pontos;
            Status = status;
            Descritivo = descritivo;
            DisponibilizadoPor = disponibilizadoPor;
            LivroId = livroId;
            TituloLivro = tituloLivro;
            DisponibilizadoEm = disponibilizadoEm;
            Capa = capa;
        }
        public int DisponibilizacaoTrocaId { get; private set; }
        public int Pontos { get; private set; }
        public StatusTroca Status { get; private set; }
        public string Descritivo { get; private set; }
        public string DisponibilizadoPor { get; private set; }
        public int LivroId { get; private set; }
        public string TituloLivro { get; private set; }
        public DateTime DisponibilizadoEm { get; private set; }
        public string Capa { get; private set; }

        public static ObterTrocaResultado CriarPor(Troca livroDisponibilizado)
        {
            string imgBase64 = livroDisponibilizado.Livro.ObterCapaEmBase64();

            return new ObterTrocaResultado(
                livroDisponibilizado.Id,
                livroDisponibilizado.Pontos,
                livroDisponibilizado.Status,
                livroDisponibilizado.Descritivo,
                livroDisponibilizado.UsuarioQueDisponibilizouParaTroca.UserName,
                livroDisponibilizado.LivroId,
                livroDisponibilizado.Livro.Titulo,
                livroDisponibilizado.DisponibilizadoEm,
                imgBase64
            );
        }
    }
}
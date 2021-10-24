using System;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocaResultado
    {
        public ObterTrocaResultado(int pontos, StatusTroca status, string descritivo, string disponibilizadoPor, 
            int livroId, string tituloLivro, DateTime disponibilizadoEm)
        {
            Pontos = pontos;
            Status = status;
            Descritivo = descritivo;
            DisponibilizadoPor = disponibilizadoPor;
            LivroId = livroId;
            TituloLivro = tituloLivro;
            DisponibilizadoEm = disponibilizadoEm;
        }

        public int Pontos { get; private set; }
        public StatusTroca Status { get; private set; }
        public string Descritivo { get; private set; }
        public string DisponibilizadoPor { get; private set; }
        public int LivroId { get; private set; }
        public string TituloLivro { get; private set; }
        public DateTime DisponibilizadoEm { get; private set; }

        public static ObterTrocaResultado CriarPor(LivroDisponibilizadoParaTroca livroDisponibilizado)
        {
            return new ObterTrocaResultado(
                livroDisponibilizado.Pontos,
                livroDisponibilizado.Status,
                livroDisponibilizado.Descritivo,
                livroDisponibilizado.UsuarioQueDisponibilizouParaTroca.UserName,
                livroDisponibilizado.LivroId,
                livroDisponibilizado.Livro.Titulo,
                livroDisponibilizado.DisponibilizadoEm
            );
        }
    }
}
using System;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Dominio.Entidades
{
    public class LivroDisponibilizadoParaTroca
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int Pontos { get; set; }
        public DateTime DisponibilizadoEm { get; set; }
        public string DisponibilizadoPor { get; set; }
        public Usuario UsuarioQueDisponibilizouParaTroca { get; set; }
        public StatusTroca Status { get; set; }
        public string Descritivo { get; set; }
    }
}
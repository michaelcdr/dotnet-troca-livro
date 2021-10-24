using System;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class TrocarLivroViewModel
    {
        public TrocarLivroViewModel()
        {

        }

        public int Pontos { get;  set; }
        public StatusTroca Status { get;  set; }
        public string Descritivo { get;  set; }
        public string DisponibilizadoPor { get;  set; }
        public int LivroId { get;  set; }
        public string TituloLivro { get;  set; }
        public DateTime DisponibilizadoEm { get;  set; }
    }
}
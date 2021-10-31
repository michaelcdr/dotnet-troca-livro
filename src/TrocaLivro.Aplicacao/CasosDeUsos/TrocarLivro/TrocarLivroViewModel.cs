using System;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class TrocarLivroViewModel
    {
        public TrocarLivroViewModel()
        {

        }
        public int DisponibilizacaoTrocaId { get; set; }
        public int Pontos { get;  set; }
        public StatusTroca Status { get;  set; }
        public string Descritivo { get;  set; }
        public string DisponibilizadoPor { get;  set; }
        public int LivroId { get;  set; }
        public string TituloLivro { get;  set; }
        public DateTime DisponibilizadoEm { get;  set; }
        public string Capa { get; set; }
        public string TituloDestaque 
        { 
            get { 
                return $"Você está prestes a obter o livro <strong>{this.TituloLivro}</strong> por "+
                       $"<strong>{(this.Pontos > 1 ? this.Pontos + " pontos" : this.Pontos + " ponto")}</strong>."; 
            }
        }
    }
}
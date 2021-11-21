using System;
using System.Collections.Generic;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.ViewModels
{
    public class TrocaViewModel
    {
        public int TrocaId { get; private set; }
        public int Pontos { get; private set; }
        public StatusTroca Status { get; private set; }
        public string Descritivo { get; private set; }
        public string DisponibilizadoPor { get; private set; }
        public int LivroId { get; private set; }
        public string TituloLivro { get; private set; }
        public DateTime DisponibilizadoEm { get; private set; }
        public DateTime? DataAprovacaoTroca { get; private set; }
        public string Capa { get; private set; }
        public List<string> Imagens { get; set; }
        public string EnderecoEntrega { get; set; }
        public bool PodeMarcarComoEnviado()
        {
            return this.Status == StatusTroca.TrocaAprovada;
        }
        public string ObterStatusFormatado()
        {
            switch (this.Status)
            {
                case StatusTroca.Disponibilizado:
                    return "Disponibilizado para troca";
                case StatusTroca.TrocaSolicitada:
                    return "Troca solicitada";
                case StatusTroca.TrocaAprovada:
                    return "Troca aprovada";
                case StatusTroca.LivroEnviado:
                    return "Enviado";
                case StatusTroca.LivroRecebido:
                    return "Livro recebido";
                default:
                    return "";
            }
        }
    }
}
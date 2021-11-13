using System;
using System.Collections.Generic;
using System.Linq;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocaResultado
    {
        public ObterTrocaResultado(
            int trocaId, int pontos, StatusTroca status, string descritivo, string disponibilizadoPor, 
            int livroId, string tituloLivro, DateTime disponibilizadoEm, string capa)
        {
            TrocaId = trocaId;
            Pontos = pontos;
            Status = status;
            Descritivo = descritivo;
            DisponibilizadoPor = disponibilizadoPor;
            LivroId = livroId;
            TituloLivro = tituloLivro;
            DisponibilizadoEm = disponibilizadoEm;
            Capa = capa;
        }
        public int TrocaId { get; private set; }
        public int Pontos { get; private set; }
        public StatusTroca Status { get; private set; }
        public string Descritivo { get; private set; }
        public string DisponibilizadoPor { get; private set; }
        public int LivroId { get; private set; }
        public string TituloLivro { get; private set; }
        public DateTime DisponibilizadoEm { get; private set; }
        public string Capa { get; private set; }
        public List<string> Imagens { get;  set; }

        public static ObterTrocaResultado CriarPor(Troca troca)
        {
            string imgBase64 = troca.Livro.ObterCapaEmBase64();

            var imagensTroca = new List<string>();

            if (troca.Imagens != null)
                foreach (var imagemTroca in troca.Imagens)
                {
                    int imgLength = imagemTroca.Nome.Length;
                    var imgData = imagemTroca.Nome;
                    string base64String = "data:image/jpg;base64," + Convert.ToBase64String(imgData, 0, imgLength);

                    imagensTroca.Add(base64String);
                }

            return new ObterTrocaResultado(
                troca.Id,
                troca.Pontos,
                troca.Status,
                troca.Descritivo,
                troca.UsuarioQueDisponibilizouParaTroca.UserName,
                troca.LivroId,
                troca.Livro.Titulo,
                troca.DisponibilizadoEm,
                imgBase64
            )
            {
                Imagens = imagensTroca
            };
        }
    }
}
using System;

namespace WebApp.Models
{
    public class LivroCard
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Capa { get; private set; } 

        public LivroCard(int livroId, string titulo, string descricao, string capaBase64)
        {
            this.Id = livroId;
            this.Titulo = titulo;
            this.Capa = capaBase64;
            this.Descricao = FormatarDescricao(descricao);
        }

        private string FormatarDescricao(string descricaoCompleta) 
        {
            int totalCaracteres = 100;

            if (descricaoCompleta.Length < 100)
                totalCaracteres = descricaoCompleta.Length;

            return descricaoCompleta.Substring(0, totalCaracteres);
        }
    }
}

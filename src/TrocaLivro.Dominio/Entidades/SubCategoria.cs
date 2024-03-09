using System;
using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class SubCategoria : Entidade
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string UrlAmigavel { get; private set; }
        public List<Livro> Livros { get; set; }
        public Categoria Categoria { get; set; }

        public SubCategoria(string nome)
        {
            this.Nome = nome;

            GerarUrlAmigavel();
        }
        
        public SubCategoria(string nome, int categoriaId)
        {
            this.Nome = nome;
            this.CategoriaId = categoriaId;

            GerarUrlAmigavel();
        }

        public override bool TaValido()
        {
            return !string.IsNullOrEmpty(this.Nome) &&
                this.CategoriaId > 0;
        }

        public void GerarUrlAmigavel()
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            string urlAmigavel = this.Nome;
            for (int i = 0; i < comAcentos.Length; i++)
                urlAmigavel = urlAmigavel.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());

            urlAmigavel = urlAmigavel.ToLower().Replace(" ", "-");

            this.UrlAmigavel = urlAmigavel;
        }
    }
}

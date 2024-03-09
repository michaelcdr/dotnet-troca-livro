using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Autor : Entidade
    {
        public Autor(string nome)
        {
            Nome = nome;
        }
        protected Autor() { }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<LivroAutor> LivrosAutores { get; private set; }
        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this._erros.Add(new Notificacao("O nome deve ser informado", "Nome"));

            return this._erros.Count == 0;
        } 
    }
}
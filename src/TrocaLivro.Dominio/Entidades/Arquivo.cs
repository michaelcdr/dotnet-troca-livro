namespace TrocaLivro.Dominio.Entidades
{
    public class Arquivo : Entidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public Livro Livro { get; set; }

        public override bool TaValido()
        {
            return true;
        }
    }
}

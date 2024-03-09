namespace TrocaLivro.Dominio.Entidades
{
    public class ImagemLivroEmTroca : Entidade
    {
        public int Id { get; set; }
        public int TrocaId { get; private set; }
        public byte[] Nome { get; private set; }
        public Troca Troca { get; set; }

        protected ImagemLivroEmTroca() { }

        public ImagemLivroEmTroca(int trocaId, byte[] nome)
        {
            this.TrocaId = trocaId;
            this.Nome = nome;
        }

        public ImagemLivroEmTroca( byte[] nome)
        {
            this.Nome = nome;
        }

        public override bool TaValido()
        {
            return true;
        }
    }
}
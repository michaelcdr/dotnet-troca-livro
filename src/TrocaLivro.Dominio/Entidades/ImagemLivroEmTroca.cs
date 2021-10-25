namespace TrocaLivro.Dominio.Entidades
{
    public class ImagemLivroEmTroca : EntidadeBase
    {
        public int Id { get;  set; }
        public int TrocaId { get; private set; }
        public byte[] Nome { get; private set; }
        public Troca Troca { get; set; }

        public ImagemLivroEmTroca()
        {

        }

        public ImagemLivroEmTroca(int trocaId, byte[] nome)
        {
            this.TrocaId = trocaId;
            this.Nome = nome;
        }

        public override bool TaValido()
        {
            return true;
        }
    }
}
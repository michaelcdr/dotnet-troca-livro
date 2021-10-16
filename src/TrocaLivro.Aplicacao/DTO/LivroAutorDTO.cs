namespace TrocaLivro.Aplicacao.DTO
{
    public class LivroAutorDTO
    {
        public int AutorId { get; set; }
        public string Nome { get; set; }
        public LivroAutorDTO(int autorId, string nome)
        {
            this.AutorId = autorId;
            this.Nome = nome;
        }
    }
}
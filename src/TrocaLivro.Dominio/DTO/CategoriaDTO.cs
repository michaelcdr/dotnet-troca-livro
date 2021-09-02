namespace TrocaLivro.Dominio.DTO
{
    public class CategoriaDTO
    {
        public CategoriaDTO(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
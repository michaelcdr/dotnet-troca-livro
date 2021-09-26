namespace TrocaLivro.Aplicacao.DTO
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

    public class SubCategoriaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeCategoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
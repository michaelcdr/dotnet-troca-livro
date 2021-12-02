namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class SubCategoriaViewModel
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public int SubCategoriaId { get; set; }
        public string UrlAmigavel { get; set; }
        public SubCategoriaViewModel()
        {

        }

        public SubCategoriaViewModel(int categoriaId, string nome, int subCategoriaId, string urlAmigavel)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            SubCategoriaId = subCategoriaId;
            UrlAmigavel = urlAmigavel;
        }
    }
}

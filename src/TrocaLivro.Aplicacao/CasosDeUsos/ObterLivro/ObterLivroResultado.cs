using TrocaLivro.Aplicacao.DTO;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivroResultado
    {
        public ObterLivroResultado(LivroDTO livro)
        {
            Livro = livro;
        }
        public LivroDTO Livro { get;  private set; }
    }
}
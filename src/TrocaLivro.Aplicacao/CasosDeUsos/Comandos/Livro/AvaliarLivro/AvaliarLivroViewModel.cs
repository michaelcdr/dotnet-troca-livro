using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class AvaliarLivroViewModel
    { 
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public NotaLivroEnum? Nota { get; set; }
        public int LivroId { get; private set; }
        public string TituloLivro { get; private set; }

        public AvaliarLivroViewModel(int id, string tituloLivro)
        {
            this.LivroId = id;
            this.TituloLivro = tituloLivro; 
        }
    }
}
namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterPacoteResultado
    {
        public ObterPacoteResultado(int id, string titulo, string descritivo, decimal valor, int pontos)
        {
            Id = id;
            Titulo = titulo;
            Descritivo = descritivo;
            Valor = valor;
            Pontos = pontos;
        }
        public ObterPacoteResultado()
        {

        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descritivo { get; set; }
        public decimal Valor { get; set; }
        public int Pontos { get; set; }
    }
}

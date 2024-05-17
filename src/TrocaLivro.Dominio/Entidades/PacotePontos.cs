namespace TrocaLivro.Dominio.Entidades
{
    public class PacotePontos
    {
        public PacotePontos() { }

        public PacotePontos(int id, string titulo, string descritivo, decimal valor, int pontos)
        {
            Id = id;
            Titulo = titulo;
            Descritivo = descritivo;
            Valor = valor;
            Pontos = pontos;
        }
       
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descritivo { get; set; }
        public decimal Valor { get; set; }
        public int Pontos { get; set; }
    }
}
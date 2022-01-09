namespace TrocaLivro.Dominio
{
    public class Notificacao
    {
        public Notificacao(string mensagem, string propriedade)
        {
            Mensagem = mensagem;
            Propriedade = propriedade;
        }

        public string Mensagem { get; set; }
        public string Propriedade { get; set; }
    }
}

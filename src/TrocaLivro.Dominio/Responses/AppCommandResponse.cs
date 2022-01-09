using System.Collections.Generic;

namespace TrocaLivro.Dominio.Responses
{
    public class AppCommandResponse
    {
        public List<Notificacao> Erros { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public AppCommandResponse(bool sucesso, string mensagem)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
        }
        public AppCommandResponse(string mensagem, bool sucesso, List<Notificacao> erros)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Erros = erros;
        }
        public AppCommandResponse()
        {

        }
    }
}
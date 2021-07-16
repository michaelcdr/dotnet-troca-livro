using System.Collections.Generic;

namespace TrocaLivro.Dominio.Responses
{
    public class AppResponse<T>
    {
        public List<Notificacao> Erros { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get;  set; }
        public T Dados { get;  set; }
        public AppResponse(string mensagem, bool sucesso, List<Notificacao> erros)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Erros = erros;
        }

        public AppResponse(bool sucesso, string mensagem)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
        }

        public AppResponse(bool sucesso, string mensagem, T dados)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Dados = dados;
        }

        public AppResponse()
        {

        }
    }
}

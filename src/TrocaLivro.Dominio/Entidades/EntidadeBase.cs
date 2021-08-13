using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public List<Notificacao> _erros { get; private set; } = new List<Notificacao>();

        public List<Notificacao> ObterErros()
        {
            return this._erros;
        }

        public abstract bool TaValido();

        public void AdicionarErro(string mensagem, string propriedade)
        {
            _erros.Add(new Notificacao(mensagem, propriedade));
        }
        public void LimparErros()
        {
            _erros = new List<Notificacao>();
        }
    }
}

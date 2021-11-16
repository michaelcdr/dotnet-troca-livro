using System.Collections.Generic;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasAoUsuarioLogadoResultado
    {
        public List<TrocaSolicitadaViewModel> Solicitacoes { get; set; }

        public ObterTrocasSolicitadasAoUsuarioLogadoResultado(List<TrocaSolicitadaViewModel> solicitacoes)
        {
            this.Solicitacoes = solicitacoes;
        }
    }
}

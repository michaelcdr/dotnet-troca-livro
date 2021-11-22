using System.Collections.Generic;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasPeloUsuarioLogadoResultado
    {
        public List<TrocaSolicitadaViewModel> Solicitacoes { get; set; }
        public ObterTrocasSolicitadasPeloUsuarioLogadoResultado(List<TrocaSolicitadaViewModel> solicitacoes)
        {
            this.Solicitacoes = solicitacoes;
        }
    }
}

using System.Collections.Generic;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasResultado
    {
        public List<TrocaSolicitadaViewModel> Solicitacoes { get; set; }

        public ObterTrocasSolicitadasResultado(List<TrocaSolicitadaViewModel> solicitacoes)
        {
            this.Solicitacoes = solicitacoes;
        }
    }
}

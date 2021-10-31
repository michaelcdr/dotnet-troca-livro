using System.Collections.Generic;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterDadosDashboardResultado
    {
        public int QtdLivrosTrocados { get; set; }
        public int QtdLivrosDisponiveis { get; set; }
        public int QtdLivrosCadastrados { get; set; }
        public List<PacotePontosViewModel> Pacotes { get; set; }
    }
}

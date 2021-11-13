using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class SolicitarTrocaCommand : IRequest<AppResponse<SolicitarTrocaResultado>>
    {
        public int TrocaId { get; set; }
        public string Usuario { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }

        public SolicitarTrocaCommand()
        {

        }

    }
}
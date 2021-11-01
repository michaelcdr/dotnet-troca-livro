using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Infra.Repositorios.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterPacoteQueryHandler : IRequestHandler<ObterPacoteQuery, ObterPacoteResultado>
    {
        private readonly PacotesRepositorio _pacotesRepositorio;
        private readonly IMapper _mapper;

        public ObterPacoteQueryHandler(IMapper mapper)
        {
            this._pacotesRepositorio = new PacotesRepositorio();
            this._mapper = mapper;
        }

        public async Task<ObterPacoteResultado> Handle(ObterPacoteQuery request, CancellationToken cancellationToken)
        {
            PacotePontos pacote = _pacotesRepositorio.Obter(request.PacoteId);
            ObterPacoteResultado resultado = _mapper.Map<ObterPacoteResultado>(pacote);

            return resultado;
        }
    }
}

using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Infra.Repositorios.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterPacoteCommandHandler : IRequestHandler<ObterPacoteCommand, ObterPacoteResultado>
    {
        private readonly PacotesRepositorio _pacotesRepositorio;
        private readonly IMapper _mapper;

        public ObterPacoteCommandHandler(IMapper mapper)
        {
            this._pacotesRepositorio = new PacotesRepositorio();
            this._mapper = mapper;
        }

        public async Task<ObterPacoteResultado> Handle(ObterPacoteCommand request, CancellationToken cancellationToken)
        {
            PacotePontos pacote = _pacotesRepositorio.Obter(request.PacoteId);
            ObterPacoteResultado resultado = _mapper.Map<ObterPacoteResultado>(pacote);

            return resultado;
        }
    }
}

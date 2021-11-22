using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterPontosQuery : IRequest<AppResponse<ObterPontosResultado>>
    {
        public ObterPontosQuery(string usuario)
        {
            Usuario = usuario;
        }

        public string Usuario { get; set; }
    }
}

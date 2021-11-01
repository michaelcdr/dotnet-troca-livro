using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterUsuarioQuery : IRequest<AppResponse<ObterUsuarioResultado>>
    {
        public string UserName { get; set; }

        public ObterUsuarioQuery(string userName)
        {
            this.UserName = userName;   
        }
    }
}

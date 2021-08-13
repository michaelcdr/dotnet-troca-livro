using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Api.Interfaces
{
    public interface IGeradorToken
    {
        Task<string> Gerar(string userName, Usuario usuarioEncontrado);
    }
}

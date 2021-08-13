using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrocaLivro.Api.InputModel
{
    public class AutenticacaoModel
    {
        public string Password { get; internal set; }
        public string Login { get; internal set; }
    }
}

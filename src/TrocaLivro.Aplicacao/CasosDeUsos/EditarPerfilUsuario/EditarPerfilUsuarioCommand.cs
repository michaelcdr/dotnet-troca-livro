﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class EditarPerfilUsuarioCommand : IRequest<AppResponse<EditarPerfilUsuarioResultado>>
    {
    }
}

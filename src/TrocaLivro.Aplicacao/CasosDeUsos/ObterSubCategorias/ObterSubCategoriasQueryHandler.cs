using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriasQueryHandler : IRequestHandler<ObterSubCategoriasQuery, ObterSubCategoriasResultado>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork _uow;
        public ObterSubCategoriasQueryHandler(IMapper mapper, IUnitOfWork uow)
        {
            this.mapper = mapper;
            this._uow = uow;
        }
        
        public async Task<ObterSubCategoriasResultado> Handle(ObterSubCategoriasQuery request, CancellationToken cancellationToken)
        {
            IList<SubCategoria> subCategorias = await _uow.Categorias.ObterSubCategorias(request.CategoriaId);

            var subCategoriaDTOs = new List<SubCategoriaDTO>();

            if (subCategorias != null)
                subCategoriaDTOs = mapper.Map<List<SubCategoriaDTO>>(subCategorias);

            return new ObterSubCategoriasResultado(subCategoriaDTOs);
        }
    }
}

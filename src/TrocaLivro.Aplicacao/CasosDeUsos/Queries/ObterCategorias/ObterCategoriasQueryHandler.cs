﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterCategoriasQueryHandler : IRequestHandler<ObterCategoriasQuery, AppResponse<ObterCategoriasResultado>>
    {
        private readonly ApplicationDbContext _context;

        public ObterCategoriasQueryHandler(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<AppResponse<ObterCategoriasResultado>> Handle(ObterCategoriasQuery request, CancellationToken cancellationToken)
        {
            List<Categoria> categorias = await _context.Categorias
                .Include(categoriaAtual => categoriaAtual.SubCategorias)
                .OrderBy(categoriaAtual => categoriaAtual.Nome)
                .ToListAsync();

            List<CategoriaViewModel> categoriaViewModels = new List<CategoriaViewModel>();

            if (categorias.Any())
            {
                categoriaViewModels = categorias.Select(categoriaAtual => new CategoriaViewModel
                {
                    CategoriaId = categoriaAtual.Id,
                    Nome = categoriaAtual.Nome,
                    SubCategorias = categoriaAtual.SubCategorias.Any()
                        ? categoriaAtual.SubCategorias.Select(subcategoriaAtual => new SubCategoriaViewModel(
                                subcategoriaAtual.CategoriaId,
                                subcategoriaAtual.Nome,
                                subcategoriaAtual.Id,
                                subcategoriaAtual.UrlAmigavel)
                            ).ToList()
                        : new List<SubCategoriaViewModel>()

                }).ToList();
            }

            return new AppResponse<ObterCategoriasResultado>(true, "Categorias obtidas com sucesso.", new ObterCategoriasResultado
            {
                Categorias = categoriaViewModels
            });
        }
    }
}

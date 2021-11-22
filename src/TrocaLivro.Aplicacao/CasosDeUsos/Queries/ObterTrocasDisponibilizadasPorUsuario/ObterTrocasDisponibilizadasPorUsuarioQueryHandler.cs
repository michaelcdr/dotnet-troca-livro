using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasDisponibilizadasPorUsuarioQueryHandler
        : IRequestHandler<ObterTrocasDisponibilizadasPorUsuarioQuery, AppResponse<ObterTrocasDisponibilizadasPorUsuarioResultado>>
    {
        private readonly ApplicationDbContext db;
        private readonly string MSG = "Trocas obtidas com sucesso.";
        private readonly IMapper mapper;
        public ObterTrocasDisponibilizadasPorUsuarioQueryHandler(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<AppResponse<ObterTrocasDisponibilizadasPorUsuarioResultado>> Handle(
            ObterTrocasDisponibilizadasPorUsuarioQuery request, CancellationToken cancellationToken)
        {
            List<Troca> trocas = await db.Trocas.AsNoTracking()
                .Include(disponibilizacao => disponibilizacao.Livro).ThenInclude(livro => livro.Imagens)
                .Include(disponibilizacao => disponibilizacao.UsuarioQueDisponibilizouParaTroca)
                .Where(troca => troca.UsuarioQueDisponibilizouParaTroca.UserName == request.Usuario)
                .ToListAsync();

            var livros = new List<TrocaDisponibilizadaViewModel>();

            foreach (Troca troca in trocas)
            {
                LivroCardModel livro = this.mapper.Map<Livro, LivroCardModel>(troca.Livro);

                livros.Add(new TrocaDisponibilizadaViewModel(troca.Id, troca.Pontos, livro));
            }

            var resultado = new ObterTrocasDisponibilizadasPorUsuarioResultado(livros);

            return new AppResponse<ObterTrocasDisponibilizadasPorUsuarioResultado>(true,MSG,resultado);
        }
    }
}

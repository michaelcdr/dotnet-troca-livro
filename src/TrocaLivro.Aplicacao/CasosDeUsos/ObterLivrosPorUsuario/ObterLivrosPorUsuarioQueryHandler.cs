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
using TrocaLivro.Dominio.Transacoes;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosPorUsuarioQueryHandler : IRequestHandler<ObterLivrosPorUsuarioQuery, AppResponse<ObterLivrosPorUsuarioResultado>>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private const string MSG_SUCCESS = "Livros obtidos com sucesso.";

        public ObterLivrosPorUsuarioQueryHandler(ApplicationDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._db = context;
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<AppResponse<ObterLivrosPorUsuarioResultado>> Handle(ObterLivrosPorUsuarioQuery request, CancellationToken cancellationToken)
        {
            List<Livro> livros = await _db.Livros.Where(e => e.CadastradoPor == request.Usuario && !e.Deletado).ToListAsync();

            List<int> livrosIds = livros.Select(livro => livro.Id).ToList();

            List<Imagem> imagens = await _uow.Livros.ObterImagens(livrosIds);
            
            foreach (Livro livro in livros)
                livro.AdicionarImagens(imagens.Where(imagemAtual => imagemAtual.LivroId == livro.Id).ToList());

            var livrosCards = livros.Count > 0
                ? this._mapper.Map<List<Livro>, List<LivroCardModel>>(livros)
                : new List<LivroCardModel>();

            var resultado = new ObterLivrosPorUsuarioResultado(livrosCards);

            return new AppResponse<ObterLivrosPorUsuarioResultado>(true, MSG_SUCCESS, resultado);
        }
    }
}
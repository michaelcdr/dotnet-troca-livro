using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosQueryHandler : IRequestHandler<ObterLivrosQuery, AppResponse<ObterLivrosResultado>>
    {
        private readonly IUnitOfWork uow;
        private const string MSG_SUCCESS = "Livros obtidos com sucesso.";
        private readonly IMapper mapper;
        public ObterLivrosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.uow = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AppResponse<ObterLivrosResultado>> Handle(ObterLivrosQuery request, CancellationToken cancellationToken)
        {
            List<Livro> livros = await uow.Livros.ObterLivrosComAutores(
                request.TamanhoPagina,
                request.QuantidadeRegistrosAPular, 
                request.TermoPesquisa,
                request.SubCategoria);

            List<int> livrosIds = livros.Select(livro => livro.Id).ToList();

            List<LivroAutor> livrosAutores = await uow.Autores.ObterParaLivros(livrosIds);

            List<int> editorasIds = livros.Select(livros => livros.EditoraId).Distinct().ToList();

            List<Editora> editoras = await uow.Editoras.ObterPorIds(editorasIds);

            List<Imagem> imagens = await uow.Livros.ObterImagens(livrosIds);

            foreach (Livro livro in livros)
            {
                livro.AdicionarAutores(livrosAutores.Where(livroAutor => livroAutor.LivroId == livro.Id).ToList());

                livro.AdicionarEditora(editoras.Single(editoraAtual => editoraAtual.Id == livro.EditoraId));

                livro.AdicionarImagens(imagens.Where(imagemAtual => imagemAtual.LivroId == livro.Id).ToList());
            }

            List<LivroCardModel> livrosCards = this.mapper.Map<List<Livro>, List<LivroCardModel>>(livros);

            var resultado = new ObterLivrosResultado(livrosCards);

            return new AppResponse<ObterLivrosResultado>(true, MSG_SUCCESS, resultado);
        }
    }
}
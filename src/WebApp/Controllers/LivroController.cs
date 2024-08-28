using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.CasosDeUsos.EditarLivro;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class LivroController : BaseController
    {
        private readonly LivroApiClient api;
        private readonly CategoriaApiClient categoriasApi;
        private readonly EditoraApiClient editorasApi;
        private readonly AutorApiClient autoresApi;
        private readonly IMapper _mapper;

        public LivroController(LivroApiClient livroApiClient, 
                               CategoriaApiClient categoriasApi, 
                               EditoraApiClient editorasApi,
                               AutorApiClient autoresApi, 
                               IMapper mapper)
        {
            this._mapper = mapper;
            this.api = livroApiClient;
            this.categoriasApi = categoriasApi;
            this.editorasApi = editorasApi;
            this.autoresApi = autoresApi;
        }  
       
        public async Task<IActionResult> Detalhes(int id)
        {
            base.AtualizarToken(this.api);
            LivroDetalhes model = await ObterDetalhes(id);

            return View(model);
        }

        private async Task<LivroDetalhes> ObterDetalhes(int id)
        {
            LivroDTO livro = await api.ObterLivro(id);
            LivroDetalhes model = LivroDetalhes.GerarPorLivroDTO(livro);

            if (User.Identity.IsAuthenticated)
            {
                model.PodeAvaliar = true;
                model.PodeEditar = User.IsInRole("admin") || livro.CadastradoPor == User.Identity.Name;
                model.DisponibilizarParaTroca = true;
                model.PodeSolicitarTroca = true;
                model.LoginUsuarioLogado = User.Identity.Name;
            }
            return model;
        }

        public async Task<IActionResult> _Avaliacoes(int id) 
        {
            LivroDetalhes model = await ObterDetalhes(id);
            return PartialView(model);
        }

        public IActionResult _ListaUsuariosOfertando(LivroDetalhes model) => PartialView(model);

        public PartialViewResult Avaliar(int id, string tituloLivro) => PartialView(new AvaliarLivroViewModel(id, tituloLivro));

        [HttpPost, ModelStateValidador, AuthorizeCustomizado]
        public async Task<JsonResult> Avaliar(AvaliarLivroCommand command)
        {
            base.AtualizarToken(this.api);
            AppResponse<AvaliarLivroResultado> resposta = await api.AvaliarLivro(command);
            return Json(resposta);
        }
         
        public async Task<PartialViewResult> _ListaAvaliacoes(int id)
        {
            base.AtualizarToken(this.api);
            AppResponse<ObterAvaliacoesLivroResultado> resposta = await api.ObterAvaliacoes(id);
            return PartialView(resposta.Dados.AvaliacoesDoLivro);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> Cadastrar()
        {
            return View(new CadastrarLivroViewModel(
                await editorasApi.ObterEditoras(),
                await autoresApi.ObterAutores(),
                await categoriasApi.ObterCategorias()
            ));
        }

        [AuthorizeCustomizado, HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarLivroViewModel model)
        {
            base.AtualizarToken(this.api);

            model.Usuario = User.Identity.Name;
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ocorreram um ou mais erros de validação.");
                model.Autores = await autoresApi.ObterAutores();
                model.Editoras = await editorasApi.ObterEditoras();
                model.Categorias = await categoriasApi.ObterCategorias();
                return View(model);
            }

            AppResponse<CadastrarLivroResultado> resposta = await api.CadastrarLivro(model);
            
            if (!resposta.Sucesso)
            {
                ModelState.AddModelError("", string.Join("<br/>", resposta.Erros.Select(e =>e.Mensagem).ToList()));

                model.Autores = await autoresApi.ObterAutores();
                model.Editoras = await editorasApi.ObterEditoras();
                model.Categorias = await categoriasApi.ObterCategorias();
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }
        
        public async Task<JsonResult> ObterSubCategorias(int categoriaId)
        {
            var sub = await api.ObterSubCategorias(categoriaId);
            return Json(sub);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> Editar(int id)
        {
            LivroDTO livroDto = await api.ObterLivro(id);

            var model = new EditarLivroViewModel(
                livroDto,
                await api.ObterEditoras(),
                await api.ObterAutores(),
                await api.ObterCategorias(),
                await api.ObterSubCategorias(livroDto.CategoriaId)
            );
            return View(model);
        }

        [AuthorizeCustomizado, HttpPost]
        public async Task<IActionResult> Editar(EditarLivroViewModel model)
        {
            base.AtualizarToken(this.api);
            model.Usuario = User.Identity.Name;

            AppResponse<EditarLivroResultado> resposta = await api.EditarLivro(model);

            if (!resposta.Sucesso)
            {
                ModelState.AddModelError("", string.Join("<br/>", resposta.Erros.Select(e => e.Mensagem).ToList()));

                // em caso de erro obtemos novamente alguns dados pára a viewmodel
                model.Autores = await api.ObterAutores();
                model.Editoras = await api.ObterEditoras();
                model.Categorias = await api.ObterCategorias();
                model.SubCategorias = await api.ObterSubCategorias((int)model.CategoriaId);

                LivroDTO livroDadosAtuais = await api.ObterLivro(model.Id);

                model.ImagensAtuais = livroDadosAtuais.Imagens.Select(e => new ImagemDTO {
                    Id = e.Id, ImagemBase64 = e.ImagemBase64 
                }).ToList();

                return View(model);
            }
            return RedirectToAction("Detalhes", new { id = model.Id });
        }

        [AuthorizeCustomizado, HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            base.AtualizarToken(this.api);

            AppResponse<DeletarLivroResultado> resposta = await api.DeletarLivro(id);

            return Json(resposta);
        } 

        public async Task<IActionResult> Pesquisa(string termoPesquisa)
        {
            base.AtualizarToken(this.api);

            ViewBag.TermoPesquisa = termoPesquisa;
            List<LivroCardModel> livros = await api.ObterLivros(termoPesquisa);
            return View(livros);
        }

        public async Task<IActionResult> _ListAdicionadosRecentemente()
        {
            base.AtualizarToken(this.api);
            List<LivroCardModel> livros = await api.ObterLivrosAdicionadosRecentemente();
            return PartialView(livros);
        }

        public async Task<IActionResult> _CadastradosPeloUsuario()
        {
            base.AtualizarToken(this.api);
            List<LivroCardModel> livros = await api.ObterLivrosCadastradosPeloUsuario();
            return PartialView(livros);
        }

        public async Task<IActionResult> DisponibilizarParaTroca(int id)
        {
            LivroDTO livro = await api.ObterLivro(id);
            return View(new DisponibilizarLivroParaTrocaViewModel(livro));
        }
            
        [HttpPost, ModelStateValidador]
        public async Task<IActionResult> DisponibilizarParaTroca(DisponibilizarLivroParaTrocaCommand comando)
        {
            base.AtualizarToken(this.api);
            AppResponse<DisponibilizarLivroParaTrocaResultado> resposta = await api.DisponibilizarParaTroca(comando);
            return Json(resposta);
        }

        [Route("livros/{subcategoria}")]
        public async Task<IActionResult> Index(string subcategoria)
        {
            base.AtualizarToken(this.api);

            SubCategoriaDTO subCategoriaDTO = await api.ObterSubCategoria(subcategoria);
            ViewBag.Subcategoria = subCategoriaDTO.Nome;    

            List<LivroCardModel> livros = await api.ObterLivros(null, subcategoria);
            return View(livros);
        }
    }
}
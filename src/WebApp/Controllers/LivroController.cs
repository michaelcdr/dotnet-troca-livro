using Microsoft.AspNetCore.Mvc;
using System;
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
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LivroController : Controller
    {
        private readonly LivroApClient api;
       
        public LivroController(LivroApClient livroApClient)
        {
            this.api = livroApClient; 
        } 

        private void AtualizarToken() 
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                this.api.AtualizarToken(HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Token"));
        }

        public async Task<IActionResult> _ListAdicionadosRecentemente()
        {
            AtualizarToken();
            List<LivroCardModel> livros = await api.ObterLivrosAdicionadosRecentemente(); 
            return PartialView(livros);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            AtualizarToken();

            LivroDTO livro = await api.ObterLivro(id);
            LivroDetalhes model = LivroDetalhes.GerarPorLivroDTO(livro);
            
            if (User.Identity.IsAuthenticated)
            {
                model.PodeEditar = User.IsInRole("admin");
            }

            return View(model);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> Cadastrar()
        {
            Console.WriteLine("Cadastrar");
            return View(new CadastrarLivroViewModel(
                await api.ObterEditoras(),
                await api.ObterAutores(),
                await api.ObterCategorias()
            ));
        }

        [AuthorizeCustomizado, HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarLivroViewModel model)
        {
            AtualizarToken();

            model.Usuario = User.Identity.Name;
            
            AppResponse<CadastrarLivroResultado> resposta = await api.CadastrarLivro(model);
            
            if (!resposta.Sucesso)
            {
                ModelState.AddModelError("", string.Join("<br/>", resposta.Erros.Select(e =>e.Mensagem).ToList()));

                model.Autores = await api.ObterAutores();
                model.Editoras = await api.ObterEditoras();
                model.Categorias = await api.ObterCategorias();
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
            AtualizarToken();

            model.Usuario = User.Identity.Name;

            AppResponse<EditarLivroResultado> resposta = await api.EditarLivro(model);

            if (!resposta.Sucesso)
            {
                ModelState.AddModelError("", string.Join("<br/>", resposta.Erros.Select(e => e.Mensagem).ToList()));

                model.Autores = await api.ObterAutores();
                model.Editoras = await api.ObterEditoras();
                model.Categorias = await api.ObterCategorias();
                model.SubCategorias = await api.ObterSubCategorias((int)model.CategoriaId);

                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [AuthorizeCustomizado, HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            AtualizarToken();

            AppResponse<DeletarLivroResultado> resposta = await api.DeletarLivro(id);

            return Json(resposta);
        } 
    }
}

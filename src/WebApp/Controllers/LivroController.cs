using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;
using WebApp.Extensions;
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

        public async Task<IActionResult> _List()
        {
            var claimToken = HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Token");
            List<LivroDTO> livrosDTO = await api.ObterListaLivros(string.Empty);
            List<LivroCard> cards = livrosDTO.Select(livroDto => livroDto.ToModel()).ToList();
            return PartialView(cards);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            LivroDTO livro = await api.ObterLivros(id);
            LivroDetalhes model = LivroDetalhes.CriarUsandoLivro(livro);
            return View(model);
        }

        public async Task<IActionResult> Cadastrar()
        {
            List<EditoraDTO> editoras = await api.ObterEditoras();
            List<AutorDTO> autores = await api.ObterAutores();
            List<CategoriaDTO> categorias = await api.ObterCategorias();
            ViewBag.Editoras = new SelectList(editoras, "Id", "Nome");
            ViewBag.Autores = new SelectList(autores, "Id", "Nome");
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");
            return View(new CadastrarLivroViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(LivroRequest request)
        {
            AppResponse<Livro> resposta = await api.CadastrarLivro(request);
            
            if (!resposta.Sucesso)
            {
                foreach (var item in resposta.Erros)
                    ModelState.AddModelError(item.Propriedade, item.Mensagem);

                return View();
            }

            return RedirectToAction("Index","Home");
        }
    }
}

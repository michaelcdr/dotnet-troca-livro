using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Responses;
using WebApp.Extensions;
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

        [AuthorizeCustomizado]
        public async Task<IActionResult> Cadastrar()
        {
            return View(new CadastrarLivroViewModel(
                await api.ObterEditoras(),
                await api.ObterAutores(),
                await api.ObterCategorias()
            ));
        }

        [AuthorizeCustomizado, HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarLivroViewModel model)
        {
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
    }
}

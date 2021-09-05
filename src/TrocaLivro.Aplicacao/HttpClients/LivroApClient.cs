using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.HttpClients
{
    public class LivroApClient
    {
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;

        public LivroApClient(HttpClient httpClient, IMapper mapper)
        {
            this.httpClient = httpClient;
            this.mapper = mapper;
        }

        private HttpContent CriarMultipartFormDataParaLivro(CadastrarLivroCommand requisicao)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(requisicao.Titulo), "\"titulo\"");
            content.Add(new StringContent(requisicao.Subtitulo), BotarAspas("subtitulo"));
            content.Add(new StringContent(requisicao.Descricao), BotarAspas("descricao"));
            content.Add(new StringContent(requisicao.ISBN), BotarAspas("isbn"));
            content.Add(new StringContent(requisicao.CategoriaId.ToString()), BotarAspas("categoriaId"));

            foreach (var item in requisicao.AutorId)
                content.Add(new StringContent(item.ToString()), BotarAspas("autorId"));

            content.Add(new StringContent(requisicao.EditoraId.ToString()), BotarAspas("editoraId"));
            content.Add(new StringContent(requisicao.Ano.ToString()), BotarAspas("ano"));
            content.Add(new StringContent(requisicao.NumeroPaginas.ToString()), BotarAspas("numeroPaginas"));

            foreach (var imagem in requisicao.Imagens)
            {
                var imagemBytes = new ByteArrayContent(FileHelper.ConvertToBytes(imagem));
                imagemBytes.Headers.Add("content-type", "image/png");
                content.Add(imagemBytes, BotarAspas("imagens"), BotarAspas("imagem-atual.png"));
            }

            return content;
        }

        public async Task<AppResponse<CadastrarLivroResultado>> CadastrarLivro(CadastrarLivroViewModel request)
        {
            CadastrarLivroCommand comando = mapper.Map<CadastrarLivroCommand>(request);
            HttpContent content = CriarMultipartFormDataParaLivro(comando);
            HttpResponseMessage resposta = await httpClient.PostAsync("livro", content);

            var conteudoResposta = await resposta.Content.ReadFromJsonAsync<AppResponse<CadastrarLivroResultado>>();
            return conteudoResposta;
        }

        public async Task<List<LivroDTO>> ObterListaLivros(string termoPesquisa)
        {
            var resposta = await httpClient.GetAsync($"Livro?TermoPesquisa={termoPesquisa}");
            resposta.EnsureSuccessStatusCode();

            var livros = await resposta.Content.ReadFromJsonAsync<List<LivroDTO>>();
            return livros;
        }

        public async Task<List<EditoraDTO>> ObterEditoras()
        {
            HttpResponseMessage resposta = await httpClient.GetAsync("Editora");
            resposta.EnsureSuccessStatusCode();

            var dados = await resposta.Content.ReadFromJsonAsync<List<EditoraDTO>>();
            return dados;
        }

        public async Task<LivroDTO> ObterLivros(int id)
        {
            HttpResponseMessage resposta = await httpClient.GetAsync($"Livro/{id}");
            resposta.EnsureSuccessStatusCode();

            LivroDTO livros = await resposta.Content.ReadFromJsonAsync<LivroDTO>();
            return livros;
        }

        public async Task<List<AutorDTO>> ObterAutores()
        {
            HttpResponseMessage resposta = await httpClient.GetAsync("Autor");
            resposta.EnsureSuccessStatusCode();

            var dados = await resposta.Content.ReadFromJsonAsync<List<AutorDTO>>();
            return dados;
        }

        public async Task<List<CategoriaDTO>> ObterCategorias()
        {
            HttpResponseMessage resposta = await httpClient.GetAsync("Categoria");
            resposta.EnsureSuccessStatusCode();

            var dados = await resposta.Content.ReadFromJsonAsync<List<CategoriaDTO>>();
            return dados;
        }

        private string BotarAspas(string valor) => $"\"{valor}\"";        
    }
}
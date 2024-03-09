using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.Mapping;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Infra.Data;
using TrocaLivro.Infra.Repositorios.EF;
using TrocaLivro.Infra.Transacoes;
using Xunit;
using Moq;

namespace TrocaLivro.Testes.Integracao
{
    public class CadastrarLivroCommandHandleTestes
    {
        [Fact]
        public async Task DadoInformacoesValidasDeveCadastrarUmLivro()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DbContextoInMemory")
                .Options;

            var contexto = new ApplicationDbContext(options);
            var livrosRepositorio = new LivrosRepositorio(contexto);
            var usuariosRepositorio = new UsuariosRepositorio(contexto);
            var editorasRepositorio = new EditorasRepositorio(contexto);
            var autoresRepositorio = new AutoresRepositorio(contexto);
            var categoriasRepositorio = new CategoriasRepositorio(contexto);

            var unitOfWork = new UnitOfWork(
                contexto,
                livrosRepositorio,
                usuariosRepositorio,
                editorasRepositorio,
                autoresRepositorio,
                categoriasRepositorio
            );

            var autor = new Autor("Robert C. Martin");
            unitOfWork.Autores.Add(autor);

            var editora = new Editora("Editora Teste");
            unitOfWork.Editoras.Add(editora);

            var categoria = new Categoria("Categoria Teste 1");
            unitOfWork.Categorias.Add(categoria);

            var usuario = new Usuario("michael", "michael", "michaelcdr@hotmail.com", "reis");
            unitOfWork.Usuarios.Add(usuario);

            await unitOfWork.CommitAsync();
            
            var subCategoria = new SubCategoria("SubCategoria 1", categoria.Id);
            unitOfWork.Categorias.AdicionarSubCategoria(subCategoria);
            await unitOfWork.CommitAsync();

            var mockFormFile = new Mock<List<IFormFile>>();
            var imagens = mockFormFile.Object;

            IFormFile file = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("dummy image"))
                , 0, 0, "Data", "image.jpg"
            );
            imagens.Add(file);

            var comando = new CadastrarLivroCommand()
            {
                Id = 1,
                Titulo = "Livro XUnit",
                Ano = 2000,
                AutorId = new List<int> { autor.Id },
                EditoraId = editora.Id,
                SubCategoriaId = subCategoria.Id,
                Descricao = "Lorem ipsum dolor, Lorem ipsum dolor",
                NumeroPaginas = 1000,
                ISBN = "123456789",
                Imagens = imagens,
                Usuario = usuario.Id
            };

            var mapperConfigs = new MapperConfiguration(cfg => {
                cfg.AddProfile<LivroProfile>();
            });
            IMapper mapper = mapperConfigs.CreateMapper();

            var handler = new CadastrarLivroCommandHandler(mapper, unitOfWork);
            var cancellationToken = It.IsAny<CancellationToken>();

            // Act
            var resultado = await handler.Handle(comando, cancellationToken);

            // Assert
            Assert.True(resultado.Sucesso);

            Livro livroCadastrado = await unitOfWork.Livros.Obter(1);
            
            Assert.NotNull(livroCadastrado);
        }
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Infra.Data.EF.MapeamentoTabelas;

namespace TrocaLivro.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, TipoUsuario, string>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LivroAutor> LivrosAutores {  get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Troca> Trocas { get; set; }
        public DbSet<ImagemLivroEmTroca> ImagensDeLivrosEmTroca { get; set;  }
        public DbSet<Endereco> Enderecos { get; set; }
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Livro>().Ignore(e => e._erros);
            builder.Entity<Autor>().Ignore(e => e._erros);
            builder.Entity<Editora>().Ignore(e => e._erros);
            builder.Entity<Arquivo>().Ignore(e => e._erros);
            builder.Entity<Imagem>().Ignore(e => e._erros);
            builder.Entity<Categoria>().Ignore(e => e._erros);
            builder.Entity<SubCategoria>().Ignore(e => e._erros);
            builder.Entity<Avaliacao>().Ignore(e => e._erros);
            builder.Entity<Troca>().Ignore(e => e._erros);
            builder.Entity<Endereco>().Ignore(e => e._erros);
            builder.Entity<ImagemLivroEmTroca>().Ignore(e => e._erros);

            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new EnderecoMap());
            builder.ApplyConfiguration(new TipoUsuarioMap());
            builder.ApplyConfiguration(new LivroMap());
            builder.ApplyConfiguration(new AvaliacaoMap());
            builder.ApplyConfiguration(new TrocaMap());
            builder.ApplyConfiguration(new AutorMap());
            builder.ApplyConfiguration(new EditoraMap());
            builder.ApplyConfiguration(new LivroAutorMap());
            builder.ApplyConfiguration(new ArquivoMap());
            builder.ApplyConfiguration(new ImagemMap());
            builder.ApplyConfiguration(new CategoriaMap());
            builder.ApplyConfiguration(new SubCategoriaMap());
            builder.ApplyConfiguration(new ImagemLivroEmTrocaMap());
        }
    }
}

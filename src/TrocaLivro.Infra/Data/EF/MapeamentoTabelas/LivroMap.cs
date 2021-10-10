using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livros");
            builder.Property(e => e.Ano).IsRequired();
            builder.Property(e => e.Descricao).IsRequired();
            builder.Property(e => e.Titulo).HasMaxLength(100).IsRequired();
            builder.Property(e => e.ISBN).IsRequired();
            builder.Property(e => e.CadastradoPor).IsRequired();
            builder.HasMany(e => e.Autores).WithOne(e => e.Livro);
            builder.HasMany(e => e.Imagens).WithOne(e => e.Livro);
            builder.HasMany(e => e.Arquivos).WithOne(e => e.Livro);
            builder.HasOne(e => e.SubCategoria).WithMany(e => e.Livros).HasForeignKey(e=>e.SubCategoriaId);
        }
    }

    public class LivroDisponibilizadoParaTrocaMap : IEntityTypeConfiguration<LivroDisponibilizadoParaTroca>
    {
        public void Configure(EntityTypeBuilder<LivroDisponibilizadoParaTroca> builder)
        {
            builder.ToTable("LivrosDisponibilizadosParaTrocas");
            builder.HasOne(e => e.Livro).WithMany(e => e.DiponibilizacaoParaTrocas);
            builder.Property(e => e.Descritivo).IsRequired(true).HasMaxLength(500);
            builder.HasOne(e => e.UsuarioQueDisponibilizouParaTroca)
                .WithMany(e => e.Trocas)
                .HasForeignKey(e => e.UsuarioQueDisponibilizouParaTrocaId);
        }
    }

    public class ImagemMap : IEntityTypeConfiguration<Imagem>
    {
        public void Configure(EntityTypeBuilder<Imagem> builder)
        {
            builder.ToTable("Imagens");
            builder.Property(e => e.Nome).IsRequired();
            builder.Property(e => e.Altura).IsRequired();
            builder.Property(e => e.Largura).IsRequired();
            builder.HasOne(e => e.Livro).WithMany(e => e.Imagens);
        }
    }

    public class ArquivoMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.ToTable("Arquivos");
            builder.Property(e => e.Nome).IsRequired();
            builder.HasOne(e => e.Livro).WithMany(e => e.Arquivos);
        }
    }

    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");
            builder.Property(e => e.Nome).IsRequired();
            builder.HasMany(e => e.SubCategorias).WithOne(e => e.Categoria);
        }
    }

    public class SubCategoriaMap : IEntityTypeConfiguration<SubCategoria>
    {
        public void Configure(EntityTypeBuilder<SubCategoria> builder)
        {
            builder.ToTable("SubCategorias");
            builder.Property(e => e.Nome).IsRequired();
            builder.HasMany(e => e.Livros).WithOne(e => e.SubCategoria);
        }
    }
}

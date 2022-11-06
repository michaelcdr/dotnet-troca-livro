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
            builder.HasOne(e => e.SubCategoria)
                .WithMany(e => e.Livros).HasForeignKey(e=>e.SubCategoriaId);
        }
    }
}

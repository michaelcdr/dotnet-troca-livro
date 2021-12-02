using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class SubCategoriaMap : IEntityTypeConfiguration<SubCategoria>
    {
        public void Configure(EntityTypeBuilder<SubCategoria> builder)
        {
            builder.ToTable("SubCategorias");
            builder.Property(e => e.Nome).IsRequired();
            builder.Property(e => e.UrlAmigavel).HasMaxLength(255);
            builder.HasMany(e => e.Livros).WithOne(e => e.SubCategoria);
        }
    }
}

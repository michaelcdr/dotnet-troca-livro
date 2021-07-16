using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class LivroAutorMap : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.ToTable("LivrosAutores");
            builder.Property(e => e.AutorId).IsRequired();
            builder.Property(e => e.LivroId).IsRequired();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class ArquivoMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.ToTable("Arquivos");
            builder.Property(e => e.Nome).IsRequired();
            builder.HasOne(e => e.Livro).WithMany(e => e.Arquivos);
        }
    }
}

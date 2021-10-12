using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
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
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class ImagemLivroEmTrocaMap : IEntityTypeConfiguration<ImagemLivroEmTroca>
    {
        public void Configure(EntityTypeBuilder<ImagemLivroEmTroca> builder)
        {
            builder.ToTable("ImagensLivrosEmTroca");
            builder.Property(e => e.Nome).IsRequired();
            builder.HasOne(e => e.Troca).WithMany(e => e.Imagens);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class AvaliacaoMap : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliacoes");
            builder.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Descricao).IsRequired().HasMaxLength(1000);
            builder.HasOne(avaliacao => avaliacao.Livro)
                .WithMany(livro => livro.Avaliacoes)
                .HasForeignKey(avaliacao => avaliacao.LivroId);
        }
    }
}

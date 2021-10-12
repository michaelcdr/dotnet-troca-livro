using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
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
}

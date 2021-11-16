using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class TrocaMap : IEntityTypeConfiguration<Troca>
    {
        public void Configure(EntityTypeBuilder<Troca> builder)
        {
            builder.ToTable("Trocas");
            builder.HasOne(e => e.Livro).WithMany(e => e.DiponibilizacaoParaTrocas);
            builder.Property(e => e.Descritivo).IsRequired(true).HasMaxLength(500);
            
            builder.HasOne(e => e.UsuarioQueDisponibilizouParaTroca)
                .WithMany(e => e.TrocasDisponibilizadas)
                .HasForeignKey(e => e.UsuarioQueDisponibilizouParaTrocaId);

            builder.HasOne(e => e.UsuarioQueSolicitouTroca)
                .WithMany(e => e.TrocasSolicitadas)
                .HasForeignKey(e => e.UsuarioQueSolicitouTrocaId);

            builder.HasOne(e => e.Endereco).WithMany(e => e.Trocas);
        }
    }
}
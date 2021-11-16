using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");
            builder.Property(p => p.Bairro).HasMaxLength(150).IsRequired(true);
            builder.Property(p => p.CEP).HasMaxLength(10).IsRequired(true);
            builder.Property(p => p.UF).HasMaxLength(2).IsRequired(true);
            builder.Property(p => p.Numero).IsRequired(true);
            builder.Property(p => p.Logradouro).HasMaxLength(150).IsRequired(true);
            builder.Property(p => p.UsuarioId).IsRequired(true);
            builder.HasMany(p => p.Trocas).WithOne(e => e.Endereco).HasForeignKey(e => e.EnderecoId);
        }
    }
}

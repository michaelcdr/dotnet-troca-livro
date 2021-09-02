using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Infra.Data.EF.MapeamentoTabelas
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.Property(p => p.Nome).HasMaxLength(150).IsRequired(true);
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired(true);
            builder.Property(p => p.Sobrenome).HasMaxLength(100).IsRequired(true);
        }
    }
}

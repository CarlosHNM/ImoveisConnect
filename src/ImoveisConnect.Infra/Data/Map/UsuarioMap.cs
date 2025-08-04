using ImoveisConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImoveisConnect.Infra.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios", schema: "imoveis");

            builder.HasKey(u => u.UsuarioId);
            builder.Property(u => u.UsuarioId).ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.PasswordSalt)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(u => u.DataCadastro)
                .HasDefaultValueSql("GETDATE()");

            // Índices
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Role);
        }
    }
}
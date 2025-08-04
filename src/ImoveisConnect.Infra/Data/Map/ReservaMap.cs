using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImoveisConnect.Infra.Data.Map
{
    public class ReservaMap : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas", schema: "imoveis");

            builder.HasKey(r => r.ReservaId);
            builder.Property(r => r.ReservaId).ValueGeneratedOnAdd();

            builder.Property(r => r.DataReserva)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(r => r.DataExpiracao)
                .IsRequired();

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(r => r.ValorReserva)
                .HasPrecision(18, 2);

            builder.Property(r => r.Observacoes)
                .HasMaxLength(500);

            // Relacionamentos
            builder.HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Apartamento)
                .WithMany(a => a.Reservas)
                .HasForeignKey(r => r.ApartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(r => r.DataReserva);
            builder.HasIndex(r => r.DataExpiracao);
            builder.HasIndex(r => r.Status);
            builder.HasIndex(r => r.ClienteId);
            builder.HasIndex(r => r.ApartamentoId);
        }
    }
}
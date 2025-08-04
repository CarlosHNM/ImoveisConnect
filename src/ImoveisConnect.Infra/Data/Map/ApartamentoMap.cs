using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImoveisConnect.Infra.Data.Map
{
    public class ApartamentoMap : IEntityTypeConfiguration<Apartamento>
    {
        public void Configure(EntityTypeBuilder<Apartamento> builder)
        {
            builder.ToTable("Apartamentos", schema: "imoveis");

            builder.HasKey(a => a.ApartamentoId);
            builder.Property(a => a.ApartamentoId).ValueGeneratedOnAdd();

            builder.Property(a => a.CodigoApartamento)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(a => a.Endereco)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Preco)
                .HasPrecision(18, 2);

            builder.Property(a => a.Area)
                .HasPrecision(10, 2); 

            builder.Property(a => a.NumeroQuartos)
                .HasDefaultValue(1);

            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(DisponibilidadeStatus.Disponivel);

            builder.Property(a => a.Descricao)
                .HasMaxLength(500);

            builder.Property(a => a.DataCadastro)
                .HasDefaultValueSql("GETDATE()");

            // Índices
            builder.HasIndex(a => a.CodigoApartamento).IsUnique();
            builder.HasIndex(a => a.Status);

            // Relacionamentos
            builder.HasMany(a => a.Vendas)
                .WithOne(v => v.Apartamento)
                .HasForeignKey(v => v.ApartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Reservas)
                .WithOne(r => r.Apartamento)
                .HasForeignKey(r => r.ApartamentoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
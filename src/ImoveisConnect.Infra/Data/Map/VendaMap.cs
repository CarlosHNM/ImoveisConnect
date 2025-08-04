using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Enum;
using ImoveisConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImoveisConnect.Infra.Data.Map
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Vendas", schema: "imoveis");

            builder.HasKey(v => v.VendaId);
            builder.Property(v => v.VendaId).ValueGeneratedOnAdd();

            builder.Property(v => v.DataVenda)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(v => v.ValorEntrada)
                .HasPrecision(18, 2);

            builder.Property(v => v.StatusPagamento)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(StatusPagamentoEnum.Nullo);

            builder.Property(v => v.Observacoes)
                .HasMaxLength(500);

            // Relacionamentos
            builder.HasOne(v => v.Cliente)
                .WithMany(c => c.Vendas)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Apartamento)
                .WithMany(a => a.Vendas)
                .HasForeignKey(v => v.ApartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(v => v.DataVenda);
            builder.HasIndex(v => v.ClienteId);
            builder.HasIndex(v => v.ApartamentoId);
        }
    }
}
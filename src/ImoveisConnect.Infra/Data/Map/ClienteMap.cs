using ImoveisConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImoveisConnect.Infra.Data.Map
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Configuração da tabela
            builder.ToTable("Clientes", schema: "imoveis"); // Adicionando schema

            // Chave primária
            builder.HasKey(c => c.ClienteId);
            builder.Property(c => c.ClienteId)
                .ValueGeneratedOnAdd();

            // Configuração de propriedades
            builder.Property(c => c.ClienteNome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Nome"); // Mapeamento para nome de coluna diferente

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("CPF_Cliente"); // Exemplo de nome personalizado

            builder.Property(c => c.ClienteEmail)
                .HasMaxLength(100)
                .IsUnicode(false); // Otimização para varchar

            builder.Property(c => c.Telefone)
                .HasMaxLength(20);

            builder.Property(c => c.DataCadastro)
                .HasDefaultValueSql("GETDATE()"); // Valor padrão

            // Índices
            builder.HasIndex(c => c.CPF)
                .IsUnique()
                .HasDatabaseName("IX_Clientes_CPF");

            builder.HasIndex(c => c.ClienteEmail)
                .HasDatabaseName("IX_Clientes_Email");

            // Relacionamentos
            builder.HasMany(c => c.Vendas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // Comportamento de deleção

            builder.HasMany(c => c.Reservas)
                .WithOne(r => r.Cliente)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using ImoveisConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImoveisConnect.Infra.Data.Map
{
    internal class MenuMap : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            entity.ToTable("MENU", schema: "imoveis");

            entity.HasKey(e => e.MenuId)
                 .HasName("PK_TD_MENU");

            entity.Property(e => e.MenuId)
                .HasPrecision(8)
                .HasColumnName("ID_MENU");

            entity.Property(e => e.DsMenu)
                .HasMaxLength(30)
                .HasColumnName("DS_MENU");

            entity.Property(e => e.DsCaminho)
                .HasMaxLength(100)
                .HasColumnName("DS_CAMINHO");

            #region relacionamentos

            #endregion
        }
    }
}

using ImoveisConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImoveisConnect.Infra.Data.Map
{
    internal class SubMenuMap : IEntityTypeConfiguration<SubMenu>
    {
        public void Configure(EntityTypeBuilder<SubMenu> entity)
        {
            entity.ToTable("SUBMENU", schema: "imoveis");

            entity.HasKey(e => e.SubMenuId)
                 .HasName("PK_TD_SUBMENU");

            entity.Property(e => e.SubMenuId)
                .HasPrecision(8)
                .HasColumnName("ID_SUBMENU");

            entity.Property(e => e.MenuId)
                .HasPrecision(8)
                .HasColumnName("ID_MENU");

            entity.Property(e => e.DsSubMenu)
                .HasMaxLength(30)
                .HasColumnName("DS_SUBMENU");

            entity.Property(e => e.DsCaminho)
                .HasMaxLength(100)
                .HasColumnName("DS_CAMINHO");

            entity.Property(e => e.IsAtivo)
                .HasPrecision(1)
                .HasColumnName("FL_ATIVO");

            #region relacionamentos

            entity.HasOne(a => a.Menu)
                .WithMany(a => a.SubMenus)
                .HasForeignKey(a => a.MenuId)
                .HasConstraintName("FK_TD_SUBMENU_TD_MENU");

            #endregion
        }
    }
}

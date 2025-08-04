using ImoveisConnect.Application.Core;

namespace ImoveisConnect.Domain.Entities
{
    public class PerfilSubMenu : BaseEntity
    {
        public int PerfilSubMenuId { get; set; }
        public int PerfilId { get; set; }
        public int? SubMenuId { get; set; }
        public int? MenuId { get; set; }

        public Menu? Menu { get; set; } = null;
        public SubMenu? SubMenu { get; set; } = null;
        public Perfil Perfil { get; set; }
    }
}

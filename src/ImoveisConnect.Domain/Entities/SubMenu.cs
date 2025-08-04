using ImoveisConnect.Application.Core;

namespace ImoveisConnect.Domain.Entities
{
    public class SubMenu : BaseEntity
    {
        public int SubMenuId { get; set; }
        public string DsSubMenu { get; set; }
        public string? DsCaminho { get; set; }
        public int MenuId { get; set; }
        public bool IsAtivo { get; set; }

        public Menu Menu { get; set; }

    }
}

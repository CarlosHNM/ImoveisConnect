using ImoveisConnect.Application.Core;

namespace ImoveisConnect.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public int MenuId { get; set; }
        public string DsMenu { get; set; }
        public string? DsCaminho { get; set; }

        public List<SubMenu> SubMenus { get; set; } = new List<SubMenu>();
    }
}

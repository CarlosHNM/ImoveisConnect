using ImoveisConnect.Application.Core;

namespace ImoveisConnect.Domain.Entities
{
    public class Perfil : BaseEntity
    {
        public int PerfilId { get; set; }
        public string DsPerfil { get; set; }
        public string DsPerfilSistema { get; set; }
        public List<PerfilSubMenu> PerfilSubMenus { get; set; }
    }
}

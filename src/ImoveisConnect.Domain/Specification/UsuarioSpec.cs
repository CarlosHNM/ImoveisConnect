using ImoveisConnect.Domain.Entities;
using System.Linq.Expressions;

namespace ImoveisConnect.Domain.Specification
{
    internal class UsuarioSpec : BaseSpecification<Usuario>
    {
        public UsuarioSpec(Expression<Func<Usuario, bool>> criteria) : base(criteria)
        {
            AddInclude(x => x.Perfil);
            AddInclude("Perfil.PerfilSubMenus");
            AddInclude("Perfil.PerfilSubMenus.Menu");
            AddInclude("Perfil.PerfilSubMenus.SubMenu");

        }
    }
}

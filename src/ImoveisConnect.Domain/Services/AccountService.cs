using ImoveisConnect.Application.Core.Result;
using ImoveisConnect.Application.DTOS;
using ImoveisConnect.Domain.Entities;
using ImoveisConnect.Domain.Interfaces;
using ImoveisConnect.Domain.Interfaces.Services;
using ImoveisConnect.Domain.Specification;

namespace ImoveisConnect.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<AccountDTO>> PostLoginAsync(SignInDTO input,
            int tokenExpirationHours,
            string userByPass_HML = null)
        {
            Result<AccountDTO> result;

            if (string.IsNullOrWhiteSpace(input.Login) || string.IsNullOrWhiteSpace(input.Senha))
                return ResultHelper.Invalid<AccountDTO>("Usuário e senha são campos obrigatórios.");

            Usuario usuario = await _uow.Repository<Usuario>().FirstOrDefaultAsync(new UsuarioSpec(a => a.DsLogin.ToLower().Trim() == input.Login.ToLower().Trim()));

            bool doesUserExist = usuario != null;

            if (doesUserExist && !usuario.IsAtivo)
                return ResultHelper.Invalid<AccountDTO>($"Usuário bloqueado.");

            bool isByPass = !string.IsNullOrWhiteSpace(userByPass_HML) && userByPass_HML.Contains(input.Login.ToLower().Trim());

            bool isValid = isByPass || (doesUserExist);


            if (!isValid)
                result = ResultHelper.Invalid<AccountDTO>($"Usuário não autorizado.");
            else
                result = ResultHelper.Success<AccountDTO>(_getAccountDTO(usuario, tokenExpirationHours));

            return result;
        }

        private AccountDTO _getAccountDTO(Usuario usuario,int tokenExpirationHours)
        {
            

            var menus = usuario
                .Perfil
                .PerfilSubMenus
                .Where(a => a.MenuId.HasValue && !a.SubMenuId.HasValue && a.PerfilId == usuario.PerfilId)
                .Select(a => a.Menu)
                .ToList();

            var submenus = usuario
                .Perfil
                .PerfilSubMenus
                .Where(a => a.MenuId.HasValue && a.SubMenuId.HasValue && a.PerfilId == usuario.PerfilId)
                .Select(a => a.SubMenu)
                .ToList();

            List<MenuDTO> menusDto = new List<MenuDTO>();
            foreach (var menu in menus)
            {
                var subMenus = submenus
                    .Where(a => a.MenuId == menu.MenuId)
                    .Select(a => new SubMenuDTO(a.MenuId, a.SubMenuId, a.DsSubMenu, a.DsCaminho))
                    .ToList();

                menusDto.Add(new MenuDTO(menu.MenuId, menu.DsMenu, menu.DsCaminho, subMenus));
            }

            return new AccountDTO(usuario.Nome,
                usuario.DsLogin,
                menusDto,
                tokenExpirationHours,
                usuario.PerfilId,
                usuario.UsuarioId,
                usuario.Perfil.DsPerfil,
                usuario.Perfil.DsPerfilSistema);
        }
    }
}

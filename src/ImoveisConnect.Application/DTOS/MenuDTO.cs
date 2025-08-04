namespace ImoveisConnect.Application.DTOS
{
    /// <summary>
    /// Menu
    /// </summary>
    public class MenuDTO
    {
        public MenuDTO(MenuDTO menu, List<SubMenuDTO> subMenus)
        {
            this.MenuId = menu.MenuId;
            this.DsMenu = menu.DsMenu;
            this.SubMenus = subMenus;
        }

        public MenuDTO(int menuId, string dsMenu, string dsCaminho, List<SubMenuDTO> subMenus)
        {
            this.MenuId = menuId;
            this.DsMenu = dsMenu;
            this.DsCaminho = dsCaminho;
            this.SubMenus = subMenus;
        }

        /// <summary>
        /// IdMenu
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// DsMenu
        /// </summary>
        public string DsMenu { get; set; }

        public string DsCaminho { get; set; }

        /// <summary>
        /// SubMenus
        /// </summary>
        public List<SubMenuDTO> SubMenus { get; set; }
    }
}

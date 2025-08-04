namespace ImoveisConnect.Application.DTOS
{
    /// <summary>
    /// SubMenu
    /// </summary>
    public class SubMenuDTO
    {
        public SubMenuDTO(int menuId, int subMenuId, string dsSubMenu, string dsRecurso, List<SubMenuDTO> subMenus = default)
        {
            this.MenuId = menuId;
            this.SubMenuId = subMenuId;
            this.DsSubMenu = dsSubMenu;
            this.DsRecurso = dsRecurso;
            this.SubMenus = subMenus;
        }

        /// <summary>
        /// IdSubMenu
        /// </summary>
        public int SubMenuId { get; set; }

        /// <summary>
        /// DsMenu
        /// </summary>
        public string DsSubMenu { get; set; }

        /// <summary>
        /// DsRecurso
        /// </summary>
        public string DsRecurso { get; set; }

        /// <summary>
        /// IdMenu
        /// </summary>
        public int MenuId { get; set; }

        ///// <summary>
        ///// Menu
        ///// </summary>
        //public MenuDTO Menu { get; set; }

        public List<SubMenuDTO> SubMenus { get; set; }

    }
}

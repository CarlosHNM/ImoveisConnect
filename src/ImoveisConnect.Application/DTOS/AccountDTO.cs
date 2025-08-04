using System.Text.Json.Serialization;

namespace ImoveisConnect.Application.DTOS
{
    /// <summary>
    /// Conta do usuario no sistema
    /// </summary>
    public class AccountDTO
    {
        [JsonConstructorAttribute]
        public AccountDTO(string nome,
                   string login,
                   List<MenuDTO> menus,
                   int tokenExpirationhours,
                   int perfilId,
                   int usuarioId,
                   string dsPerfil,
                   string dsPerfilSistema)
        {
            this.Nome = nome;
            this.Login = login;
            this.Menus = menus;
            this.TokenExpirationHours = tokenExpirationhours;
            this.TokenExpiration = DateTime.Now.AddHours(tokenExpirationhours);
            this.PerfilId = perfilId;
            this.UsuarioId = usuarioId;
            this.DsPerfil = dsPerfil;
            this.DsPerfilSistema = dsPerfilSistema;

        }
        public int UsuarioId { get; set; }
        public int PerfilId { get; set; }
        public string DsPerfil { get; set; }
        public string DsPerfilSistema { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public int TokenExpirationHours { get; set; }
        public DateTime TokenExpiration { get; set; }
        public List<MenuDTO> Menus { get; set; }

        public string DsTokenExpiration
        {
            get
            {
                return this.TokenExpiration.ToString();
            }
        }


        public void SetToken(string token)
        {
            this.Token = token;
        }

        public void ClearDataForClientSide()
        {
            this.PerfilId = 0;
        }
    }
}

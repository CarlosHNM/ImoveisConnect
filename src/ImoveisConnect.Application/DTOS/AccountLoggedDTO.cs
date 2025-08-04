namespace ImoveisConnect.Application.DTOS
{
    public class AccountLoggedDTO
    {
        public AccountLoggedDTO(int usuarioId,
            int perfilId,
            string login)
        {
            this.UsuarioId = usuarioId;
            this.Login = login;
            this.PerfilId = perfilId;

        }

        public int UsuarioId { get; set; }
        public int PerfilId { get; set; }
        public string Login { get; set; }
    }
}

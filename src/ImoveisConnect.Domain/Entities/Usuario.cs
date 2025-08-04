using ImoveisConnect.Application.Core;

namespace ImoveisConnect.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string DsLogin { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool IsAtivo { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }

    }
}

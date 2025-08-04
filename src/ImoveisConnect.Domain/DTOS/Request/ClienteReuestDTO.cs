namespace ImoveisConnect.Domain.DTOS.Request
{
    public class ClienteRequestDTO
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public DateTime DataCadastro { get; set; }

        public Domain.Entities.Cliente GetModel()
        {
            return new Domain.Entities.Cliente
            {
                ClienteId = ClienteId,
                ClienteEmail = ClienteEmail,
                Telefone = Telefone,
                CPF = CPF,
                ClienteNome = ClienteNome,
                DataCadastro = DateTime.Now
            };
        }

    }
}

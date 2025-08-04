# 📦 Projeto Imóveis Conect

Este projeto é uma Web API desenvolvida em .NET, com autenticação JWT, persistência em banco de dados relacional e containerização com Docker. 
A API permite a gestão de entidades relacionadas a imóveis e vendas e cadastrar clientes novos ou atualiza-los.

---

## 🚀 Como rodar o projeto localmente utilizando Docker

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Instruções

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/imoveis-conect-api.git
   cd imoveis-conect-api
2. Suba os containers com Docker Compose:
   docker-compose up -d
   
3. A documentação Swagger pode ser acessada em:
http://localhost:5000/swagger

4. Para a autenticação poderá ser usado os seguintes dados 
POST /api/auth/login
Content-Type: application/json

{
  "dslogin": "admin",
  "senha": "Admin@1234"
}
Copie o token gerado e cole no botão de autenticação no topo da página e clique em Auhtorie. Pronto, vc estará apto a executar os metodos.

Considerações e decisões técnicas
🔧 .NET 9 foi escolhido pela performance e recursos atualizados.

Docker para garantir consistência entre os ambientes.

JWT foi adotado por ser leve e fácil de integrar com o ASP.NET.

Entity Framework Core com Migrations para versionamento de schema.

Swagger para facilitar o consumo da API e documentação de endpoints.

Arquitetura baseada em camadas: Domain, Application, Infra, API.

| Padrão                                      | Aplicação no Projeto                                | Justificativa                                           |
| ------------------------------------------- | --------------------------------------------------- | ------------------------------------------------------- |
| **DTO Pattern**                             | Uso de DTOs de entrada (Request) e saída (Response) | Isola a API das entidades do domínio                    |
| **Specification Pattern**                   | Criação de filtros e joins reutilizáveis            | Permite consultas complexas desacopladas do repositório |
| **Repository Pattern**                      | Abstração da persistência em repositórios           | Facilita testes e troca da camada de dados              |
| **Unit of Work Pattern**                    | Gerenciamento de transações                         | Garante consistência ao salvar múltiplas entidades      |
| **Service Layer Pattern**                   | Organização da lógica de aplicação em serviços      | Promove reutilização e clareza de responsabilidades     |
| **Dependency Injection**                    | Injeção de repositórios, serviços, UoW, etc.        | Reduz acoplamento e facilita testes                     |
| **Interface Segregation** (Princípio SOLID) | Interfaces específicas para cada contrato           | Evita dependências desnecessárias                       |
| **Clean Architecture (ou Onion)**           | Separação entre Domain, Application, Infra, API     | Facilita manutenção, testes e escalabilidade            |
| **Mapeamento Manual**                       | Conversão entre entidades e DTOs feita manualmente  | Maior controle e performance nas conversões             |

O projeto não utiliza bibliotecas de automapeamento (como AutoMapper). Em vez disso, o mapeamento entre entidades e DTOs é feito manualmente, garantindo maior controle sobre a lógica de transformação e evitando mágica implícita, o que melhora a performance e a legibilidade em projetos mais complexos.

# Projeto ImoveisConnect

Este projeto é uma Web API desenvolvida em .NET 9, com autenticação JWT, persistência em banco de dados relacional (MS SQL Server) e containerização com Docker.
A API permite a gestão de entidades relacionadas a imóveis, vendas e o cadastro/atualização de clientes.

------------------------------------------------------------

## Como rodar o projeto localmente utilizando Docker

Pré-requisitos:
- .NET 9 SDK (https://dotnet.microsoft.com/)
- Docker (https://www.docker.com/)
- Docker Compose (https://docs.docker.com/compose/)

Instruções:

1. Clone o repositório:
   git clone https://github.com/CarlosHNM/ImoveisConnect.git
   cd imoveis-conect-api

2. Suba os containers com Docker Compose:
   docker-compose up -d

3. Acesse a documentação Swagger:
   http://localhost:5000/swagger

------------------------------------------------------------

## Autenticação

Para testar a API, use o seguinte login:

POST /api/auth/login
Content-Type: application/json

{
  "dslogin": "admin",
  "senha": "Admin@1234"
}

Copie o token JWT gerado na resposta.
Na interface Swagger, clique no botão "Authorize" no topo da página.
Cole o token com o prefixo Bearer e clique em Authorize.
Agora você pode executar os métodos protegidos da API.

------------------------------------------------------------

## Considerações e decisões técnicas

- .NET 9 foi escolhido por sua performance e novos recursos.
- Docker garante consistência e portabilidade entre ambientes.
- JWT oferece autenticação leve e integrada com ASP.NET.
- EF Core com Migrations facilita o versionamento do schema.
- Swagger fornece documentação e testes rápidos de endpoints.
- Arquitetura em camadas separadas: Domain, Application, Infra, API.

------------------------------------------------------------

## Padrões de Projeto Utilizados

Padrão                                      | Aplicação no Projeto                                | Justificativa
------------------------------------------- | --------------------------------------------------- | -------------------------------------------------------
DTO Pattern                                 | Uso de DTOs de entrada (Request) e saída (Response) | Isola a API das entidades do domínio
Specification Pattern                       | Criação de filtros e joins reutilizáveis            | Permite consultas complexas desacopladas do repositório
Repository Pattern                          | Abstração da persistência em repositórios           | Facilita testes e troca da camada de dados
Unit of Work Pattern                        | Gerenciamento de transações                         | Garante consistência ao salvar múltiplas entidades
Service Layer Pattern                       | Organização da lógica de aplicação em serviços      | Promove reutilização e clareza de responsabilidades
Dependency Injection                        | Injeção de repositórios, serviços, UoW, etc.        | Reduz acoplamento e facilita testes
Interface Segregation (SOLID)               | Interfaces específicas para cada contrato           | Evita dependências desnecessárias
Clean Architecture (ou Onion)               | Separação entre Domain, Application, Infra, API     | Facilita manutenção, testes e escalabilidade
Mapeamento Manual                           | Conversão entre entidades e DTOs feita manualmente  | Maior controle e performance nas conversões

Nota: O projeto não utiliza bibliotecas de automapeamento (como AutoMapper).
Todo o mapeamento entre entidades e DTOs é feito manual e explicitamente, garantindo maior controle, performance e clareza.

------------------------------------------------------------

Em caso de dúvidas, sugestões ou melhorias, sinta-se à vontade para abrir uma issue ou contribuir com o projeto.

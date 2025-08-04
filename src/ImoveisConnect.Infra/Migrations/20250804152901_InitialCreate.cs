using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImoveisConnect.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "imoveis");

            migrationBuilder.CreateTable(
                name: "Apartamentos",
                schema: "imoveis",
                columns: table => new
                {
                    ApartamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoApartamento = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Disponivel"),
                    DataUltimaAtualizacaoStatus = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoIndisponibilidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NumeroQuartos = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Area = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartamentos", x => x.ApartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "imoveis",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClienteEmail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CPF_Cliente = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "MENU",
                schema: "imoveis",
                columns: table => new
                {
                    ID_MENU = table.Column<int>(type: "int", precision: 8, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DS_MENU = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DS_CAMINHO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TD_MENU", x => x.ID_MENU);
                });

            migrationBuilder.CreateTable(
                name: "Perfils",
                columns: table => new
                {
                    PerfilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DsPerfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DsPerfilSistema = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfils", x => x.PerfilId);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                schema: "imoveis",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ApartamentoId = table.Column<int>(type: "int", nullable: false),
                    DataReserva = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ValorReserva = table.Column<string>(type: "nvarchar(max)", precision: 18, scale: 2, nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reservas_Apartamentos_ApartamentoId",
                        column: x => x.ApartamentoId,
                        principalSchema: "imoveis",
                        principalTable: "Apartamentos",
                        principalColumn: "ApartamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "imoveis",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                schema: "imoveis",
                columns: table => new
                {
                    VendaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ApartamentoId = table.Column<int>(type: "int", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ValorEntrada = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StatusPagamento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Nullo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_Vendas_Apartamentos_ApartamentoId",
                        column: x => x.ApartamentoId,
                        principalSchema: "imoveis",
                        principalTable: "Apartamentos",
                        principalColumn: "ApartamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "imoveis",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SUBMENU",
                schema: "imoveis",
                columns: table => new
                {
                    ID_SUBMENU = table.Column<int>(type: "int", precision: 8, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DS_SUBMENU = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DS_CAMINHO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ID_MENU = table.Column<int>(type: "int", precision: 8, nullable: false),
                    FL_ATIVO = table.Column<bool>(type: "bit", precision: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TD_SUBMENU", x => x.ID_SUBMENU);
                    table.ForeignKey(
                        name: "FK_TD_SUBMENU_TD_MENU",
                        column: x => x.ID_MENU,
                        principalSchema: "imoveis",
                        principalTable: "MENU",
                        principalColumn: "ID_MENU",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "imoveis",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    DsLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsAtivo = table.Column<bool>(type: "bit", nullable: false),
                    PerfilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfils_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfils",
                        principalColumn: "PerfilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilSubMenu",
                columns: table => new
                {
                    PerfilSubMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    SubMenuId = table.Column<int>(type: "int", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilSubMenu", x => x.PerfilSubMenuId);
                    table.ForeignKey(
                        name: "FK_PerfilSubMenu_MENU_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "imoveis",
                        principalTable: "MENU",
                        principalColumn: "ID_MENU");
                    table.ForeignKey(
                        name: "FK_PerfilSubMenu_Perfils_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfils",
                        principalColumn: "PerfilId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilSubMenu_SUBMENU_SubMenuId",
                        column: x => x.SubMenuId,
                        principalSchema: "imoveis",
                        principalTable: "SUBMENU",
                        principalColumn: "ID_SUBMENU");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartamentos_CodigoApartamento",
                schema: "imoveis",
                table: "Apartamentos",
                column: "CodigoApartamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartamentos_Status",
                schema: "imoveis",
                table: "Apartamentos",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CPF",
                schema: "imoveis",
                table: "Clientes",
                column: "CPF_Cliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Email",
                schema: "imoveis",
                table: "Clientes",
                column: "ClienteEmail");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilSubMenu_MenuId",
                table: "PerfilSubMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilSubMenu_PerfilId",
                table: "PerfilSubMenu",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilSubMenu_SubMenuId",
                table: "PerfilSubMenu",
                column: "SubMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ApartamentoId",
                schema: "imoveis",
                table: "Reservas",
                column: "ApartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId",
                schema: "imoveis",
                table: "Reservas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_DataExpiracao",
                schema: "imoveis",
                table: "Reservas",
                column: "DataExpiracao");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_DataReserva",
                schema: "imoveis",
                table: "Reservas",
                column: "DataReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Status",
                schema: "imoveis",
                table: "Reservas",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SUBMENU_ID_MENU",
                schema: "imoveis",
                table: "SUBMENU",
                column: "ID_MENU");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                schema: "imoveis",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PerfilId",
                schema: "imoveis",
                table: "Usuarios",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Role",
                schema: "imoveis",
                table: "Usuarios",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ApartamentoId",
                schema: "imoveis",
                table: "Vendas",
                column: "ApartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteId",
                schema: "imoveis",
                table: "Vendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_DataVenda",
                schema: "imoveis",
                table: "Vendas",
                column: "DataVenda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilSubMenu");

            migrationBuilder.DropTable(
                name: "Reservas",
                schema: "imoveis");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "imoveis");

            migrationBuilder.DropTable(
                name: "Vendas",
                schema: "imoveis");

            migrationBuilder.DropTable(
                name: "SUBMENU",
                schema: "imoveis");

            migrationBuilder.DropTable(
                name: "Perfils");

            migrationBuilder.DropTable(
                name: "Apartamentos",
                schema: "imoveis");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "imoveis");

            migrationBuilder.DropTable(
                name: "MENU",
                schema: "imoveis");
        }
    }
}

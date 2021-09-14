using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SG.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(type: "varchar(150)", nullable: true),
                    Setor = table.Column<string>(type: "varchar(50)", nullable: true),
                    Funcao = table.Column<string>(type: "varchar(80)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clientes = table.Column<string>(maxLength: 45, nullable: false),
                    Cep = table.Column<string>(maxLength: 20, nullable: true),
                    Endereco = table.Column<string>(maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(maxLength: 45, nullable: true),
                    Cidade = table.Column<string>(maxLength: 45, nullable: false),
                    Estado = table.Column<string>(maxLength: 2, nullable: false),
                    Numero = table.Column<string>(maxLength: 10, nullable: true),
                    Area = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContatoClientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 45, nullable: false),
                    EmailPrinc = table.Column<string>(maxLength: 65, nullable: false),
                    EmailSec = table.Column<string>(maxLength: 65, nullable: true),
                    FonePrinc = table.Column<string>(maxLength: 16, nullable: false),
                    FoneSecun = table.Column<string>(maxLength: 16, nullable: true),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContatoClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodProjeto = table.Column<string>(maxLength: 5, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: true),
                    RecursosPrev = table.Column<int>(nullable: false),
                    RecursosUtil = table.Column<int>(nullable: false),
                    MobilizaPrev = table.Column<int>(nullable: false),
                    MobilizaUtili = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    HorasPrevDesen = table.Column<int>(nullable: false),
                    HorasUtilDesenv = table.Column<int>(nullable: false),
                    HorasPrevImplement = table.Column<int>(nullable: false),
                    HorasUtilImplement = table.Column<int>(nullable: false),
                    Concluido = table.Column<bool>(nullable: true, defaultValue: false),
                    DataConclusao = table.Column<DateTime>(type: "Date", nullable: true),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projetos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CadernoHoras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(type: "Date", nullable: false),
                    HorasDia = table.Column<TimeSpan>(nullable: true),
                    Deslocamento = table.Column<TimeSpan>(nullable: true),
                    HorasTrab = table.Column<TimeSpan>(nullable: true),
                    AtvDia = table.Column<string>(nullable: true),
                    ProjetosId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadernoHoras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CadernoHoras_Projetos_ProjetosId",
                        column: x => x.ProjetosId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CadernoHoras_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadosDias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(type: "Date", nullable: false),
                    SaidaHotel = table.Column<TimeSpan>(nullable: true),
                    EntraFabrica = table.Column<TimeSpan>(nullable: true),
                    SaidaAlmo = table.Column<TimeSpan>(nullable: true),
                    RetorAlmo = table.Column<TimeSpan>(nullable: true),
                    SaidaLanche = table.Column<TimeSpan>(nullable: true),
                    RetorLanche = table.Column<TimeSpan>(nullable: true),
                    SaidaFabrica = table.Column<TimeSpan>(nullable: true),
                    ChegaHotel = table.Column<TimeSpan>(nullable: true),
                    AtvDia = table.Column<string>(nullable: true),
                    Interno = table.Column<bool>(nullable: true),
                    HorasInterno = table.Column<TimeSpan>(nullable: true),
                    ProjetosId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosDias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DadosDias_Projetos_ProjetosId",
                        column: x => x.ProjetosId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DadosDias_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PontoExternos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(type: "Date", nullable: false),
                    EntraFabrica = table.Column<TimeSpan>(nullable: true),
                    SaidaAlmo = table.Column<TimeSpan>(nullable: true),
                    RetorAlmo = table.Column<TimeSpan>(nullable: true),
                    SaidaFabrica = table.Column<TimeSpan>(nullable: true),
                    AtvDia = table.Column<string>(maxLength: 255, nullable: false),
                    ProjetosId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoExternos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontoExternos_Projetos_ProjetosId",
                        column: x => x.ProjetosId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontoExternos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "222b3a82-7bb6-40ea-b183-19718df3c39d", "Admin", "ADMIN" },
                    { 2, "cf8b9f3f-0d37-4749-b4bb-8f895fcce308", "Gestor", "GESTOR" },
                    { 3, "b6139df6-7e8c-440c-a490-9fbffd386eeb", "RH", "RH" },
                    { 4, "fa1e7cc6-2354-4443-92de-e122489c84c6", "RUser", "RUSER" },
                    { 5, "6df125b0-a6cd-43c4-9aee-9830589ce049", "Cliente", "CLIENTE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CadernoHoras_ProjetosId",
                table: "CadernoHoras",
                column: "ProjetosId");

            migrationBuilder.CreateIndex(
                name: "IX_CadernoHoras_UserId",
                table: "CadernoHoras",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContatoClientes_ClienteId",
                table: "ContatoClientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosDias_ProjetosId",
                table: "DadosDias",
                column: "ProjetosId");

            migrationBuilder.CreateIndex(
                name: "IX_DadosDias_UserId",
                table: "DadosDias",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoExternos_ProjetosId",
                table: "PontoExternos",
                column: "ProjetosId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoExternos_UserId",
                table: "PontoExternos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_ClienteId",
                table: "Projetos",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CadernoHoras");

            migrationBuilder.DropTable(
                name: "ContatoClientes");

            migrationBuilder.DropTable(
                name: "DadosDias");

            migrationBuilder.DropTable(
                name: "PontoExternos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}

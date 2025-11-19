using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillBridge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TRILHAS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    NIVEL = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CARGA_HORARIA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FOCO_PRINCIPAL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRILHAS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    AREA_ATUACAO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    NIVEL_CARREIRA = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    DATA_CADASTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MATRICULAS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USUARIO_ID = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    TRILHA_ID = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DATA_INSCRICAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATRICULAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MATRICULA_TRILHA",
                        column: x => x.TRILHA_ID,
                        principalTable: "TRILHAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MATRICULA_USUARIO",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MATRICULAS_TRILHA_ID",
                table: "MATRICULAS",
                column: "TRILHA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATRICULAS_USUARIO_ID",
                table: "MATRICULAS",
                column: "USUARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MATRICULAS");

            migrationBuilder.DropTable(
                name: "TRILHAS");

            migrationBuilder.DropTable(
                name: "USUARIOS");
        }
    }
}

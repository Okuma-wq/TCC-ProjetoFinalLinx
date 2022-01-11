using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoLinx.webApi.Migrations
{
    public partial class AddAnexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anexos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Caminho = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    ReuniaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anexos_Reunioes_ReuniaoId",
                        column: x => x.ReuniaoId,
                        principalTable: "Reunioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_ReuniaoId",
                table: "Anexos",
                column: "ReuniaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anexos");
        }
    }
}

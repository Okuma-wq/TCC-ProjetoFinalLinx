using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoLinx.webApi.Migrations
{
    public partial class AddReuniao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reunioes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataReuniao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Local = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LojistaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reunioes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reunioes_Usuarios_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reunioes_Usuarios_LojistaId",
                        column: x => x.LojistaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reunioes_FornecedorId",
                table: "Reunioes",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reunioes_LojistaId",
                table: "Reunioes",
                column: "LojistaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reunioes");
        }
    }
}

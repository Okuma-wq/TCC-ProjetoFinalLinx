using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoLinx.webApi.Migrations
{
    public partial class AddTabelaTopico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reunioes_Usuarios_LojistaId",
                table: "Reunioes");

            migrationBuilder.AlterColumn<Guid>(
                name: "LojistaId",
                table: "Reunioes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Topicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ReuniaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topicos_Reunioes_ReuniaoId",
                        column: x => x.ReuniaoId,
                        principalTable: "Reunioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topicos_ReuniaoId",
                table: "Topicos",
                column: "ReuniaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reunioes_Usuarios_LojistaId",
                table: "Reunioes",
                column: "LojistaId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reunioes_Usuarios_LojistaId",
                table: "Reunioes");

            migrationBuilder.DropTable(
                name: "Topicos");

            migrationBuilder.AlterColumn<Guid>(
                name: "LojistaId",
                table: "Reunioes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Reunioes_Usuarios_LojistaId",
                table: "Reunioes",
                column: "LojistaId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

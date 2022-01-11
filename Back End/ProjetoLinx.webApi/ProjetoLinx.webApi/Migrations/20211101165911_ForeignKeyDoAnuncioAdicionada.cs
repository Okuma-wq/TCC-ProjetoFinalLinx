using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoLinx.webApi.Migrations
{
    public partial class ForeignKeyDoAnuncioAdicionada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuario",
                table: "Anuncios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Anuncios_IdUsuario",
                table: "Anuncios",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Anuncios_Usuarios_IdUsuario",
                table: "Anuncios",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anuncios_Usuarios_IdUsuario",
                table: "Anuncios");

            migrationBuilder.DropIndex(
                name: "IX_Anuncios_IdUsuario",
                table: "Anuncios");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Anuncios");
        }
    }
}

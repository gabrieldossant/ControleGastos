using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleGastos.Migrations
{
    /// <inheritdoc />
    public partial class RelacaoNasModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transacoes_categorias_CategoriaId",
                table: "transacoes");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "transacoes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "transacoes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "transacoes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Finalidade",
                table: "categorias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_transacoes_categorias_CategoriaId",
                table: "transacoes",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transacoes_categorias_CategoriaId",
                table: "transacoes");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "transacoes");

            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "transacoes",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "transacoes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Finalidade",
                table: "categorias",

                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_transacoes_categorias_CategoriaId",
                table: "transacoes",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

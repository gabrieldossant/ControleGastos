using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleGastos.Migrations
{
    /// <inheritdoc />
    public partial class ControleGastos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Finalidade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.CategoriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pessoas",
                columns: table => new
                {
                    PessoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Idade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoas", x => x.PessoaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transacoes",
                columns: table => new
                {
                    TransacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<double>(type: "double", nullable: false),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacoes", x => x.TransacaoId);
                    table.ForeignKey(
                        name: "FK_transacoes_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transacoes_pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_transacoes_CategoriaId",
                table: "transacoes",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_transacoes_PessoaId",
                table: "transacoes",
                column: "PessoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transacoes");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "pessoas");
        }
    }
}

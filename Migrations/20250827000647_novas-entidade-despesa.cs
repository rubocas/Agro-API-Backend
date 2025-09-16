using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agro.Migrations
{
    /// <inheritdoc />
    public partial class novasentidadedespesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnoSafraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DespesaCategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_AnoSafras_AnoSafraId",
                        column: x => x.AnoSafraId,
                        principalTable: "AnoSafras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesas_DespesaCategorias_DespesaCategoriaId",
                        column: x => x.DespesaCategoriaId,
                        principalTable: "DespesaCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_AnoSafraId",
                table: "Despesas",
                column: "AnoSafraId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_DespesaCategoriaId",
                table: "Despesas",
                column: "DespesaCategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");
        }
    }
}

// Importando o namespace necessário para migrações do Entity Framework Core
using Microsoft.EntityFrameworkCore.Migrations;

// Desabilitando a verificação de valores nulos (apenas para este arquivo)
#nullable disable

namespace DevFreela.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    // Definição da classe de migração AddLoginColums, que herda de Migration
    public partial class AddLoginColums : Migration
    {
        /// <inheritdoc />
        // Método Up, que define as operações a serem executadas na migração para cima
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adiciona uma coluna "Password" à tabela "Users"
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            // Adiciona uma coluna "Role" à tabela "Users"
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        // Método Down, que define as operações a serem executadas na migração para baixo (rollback)
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove a coluna "Password" da tabela "Users"
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            // Remove a coluna "Role" da tabela "Users"
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}

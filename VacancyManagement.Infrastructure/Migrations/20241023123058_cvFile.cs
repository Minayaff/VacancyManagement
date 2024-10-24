using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacancyManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cvFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVFilePath",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVFilePath",
                table: "Candidates");
        }
    }
}

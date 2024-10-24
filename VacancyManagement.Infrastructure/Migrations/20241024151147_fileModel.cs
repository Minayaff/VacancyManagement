using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacancyManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVFilePath",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "FileEntityId",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Base64Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_FileEntityId",
                table: "Candidates",
                column: "FileEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_FileEntity_FileEntityId",
                table: "Candidates",
                column: "FileEntityId",
                principalTable: "FileEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_FileEntity_FileEntityId",
                table: "Candidates");

            migrationBuilder.DropTable(
                name: "FileEntity");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_FileEntityId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "FileEntityId",
                table: "Candidates");

            migrationBuilder.AddColumn<string>(
                name: "CVFilePath",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

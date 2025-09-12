using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.API.Migrations
{
    /// <inheritdoc />
    public partial class updatebookStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Books",
                type: "tinyint(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Books",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

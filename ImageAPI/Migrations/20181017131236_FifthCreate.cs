using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageAPI.Migrations
{
    public partial class FifthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Placeholder",
                table: "Images",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Placeholder",
                table: "Images",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60);
        }
    }
}

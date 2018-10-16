using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageAPI.Migrations
{
    public partial class Third_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Images",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Images");
        }
    }
}

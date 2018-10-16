using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageAPI.Migrations
{
    public partial class FourthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadedPID",
                table: "Images",
                newName: "UploadedByPID");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Images",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Placeholder",
                table: "Images",
                column: "Placeholder",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_Placeholder",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "UploadedByPID",
                table: "Images",
                newName: "UploadedPID");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Images",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

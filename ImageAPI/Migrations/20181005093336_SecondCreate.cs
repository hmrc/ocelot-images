using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageAPI.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltText",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "UploadDate",
                table: "Images",
                newName: "ApprovedDate");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByPID",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Images",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Placeholder",
                table: "Images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedDate",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UploadedPID",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedByPID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Placeholder",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UploadedDate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UploadedPID",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ApprovedDate",
                table: "Images",
                newName: "UploadDate");

            migrationBuilder.AddColumn<string>(
                name: "AltText",
                table: "Images",
                nullable: true);
        }
    }
}

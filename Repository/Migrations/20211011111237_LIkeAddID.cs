using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class LIkeAddID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikeId",
                table: "Likes",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "Likes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Likes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Likes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Likes",
                newName: "LikeId");
        }
    }
}

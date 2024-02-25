using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UserImageRemoveMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_UserImage<TEntity>_ImageId",
                table: "Category");

            migrationBuilder.DropTable(
                name: "UserImage<TEntity>");

            migrationBuilder.DropIndex(
                name: "IX_Category_ImageId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "Src",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Src",
                table: "Category");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserImage<TEntity>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImage<TEntity>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImage<TEntity>_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ImageId",
                table: "Category",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserImage<TEntity>_UserId",
                table: "UserImage<TEntity>",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_UserImage<TEntity>_ImageId",
                table: "Category",
                column: "ImageId",
                principalTable: "UserImage<TEntity>",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

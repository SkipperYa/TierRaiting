using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UserImageUserIdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserImage<TEntity>",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserImage<TEntity>_UserId",
                table: "UserImage<TEntity>",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImage<TEntity>_AspNetUsers_UserId",
                table: "UserImage<TEntity>",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImage<TEntity>_AspNetUsers_UserId",
                table: "UserImage<TEntity>");

            migrationBuilder.DropIndex(
                name: "IX_UserImage<TEntity>_UserId",
                table: "UserImage<TEntity>");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserImage<TEntity>");
        }
    }
}

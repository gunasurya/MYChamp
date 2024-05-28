using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYChamp.Migrations
{
    /// <inheritdoc />
    public partial class userinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sessionlog_AspNetUsers_UserId",
                table: "sessionlog");

            migrationBuilder.DropIndex(
                name: "IX_sessionlog_UserId",
                table: "sessionlog");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "sessionlog");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUsersId",
                table: "sessionlog",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sessionlog_AspNetUsersId",
                table: "sessionlog",
                column: "AspNetUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_sessionlog_AspNetUsers_AspNetUsersId",
                table: "sessionlog",
                column: "AspNetUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sessionlog_AspNetUsers_AspNetUsersId",
                table: "sessionlog");

            migrationBuilder.DropIndex(
                name: "IX_sessionlog_AspNetUsersId",
                table: "sessionlog");

            migrationBuilder.DropColumn(
                name: "AspNetUsersId",
                table: "sessionlog");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "sessionlog",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_sessionlog_UserId",
                table: "sessionlog",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_sessionlog_AspNetUsers_UserId",
                table: "sessionlog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

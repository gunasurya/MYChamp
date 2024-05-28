using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYChamp.Migrations
{
    /// <inheritdoc />
    public partial class newuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

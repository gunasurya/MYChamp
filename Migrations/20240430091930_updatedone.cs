using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MYChamp.Migrations
{
    /// <inheritdoc />
    public partial class updatedone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Model_AspNetUsers_UserId",
                table: "Session_Model");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session_Model",
                table: "Session_Model");

            migrationBuilder.RenameTable(
                name: "Session_Model",
                newName: "Session_model");

            migrationBuilder.RenameIndex(
                name: "IX_Session_Model_UserId",
                table: "Session_model",
                newName: "IX_Session_model_UserId");

            migrationBuilder.AddColumn<string>(
                name: "LogoutTime",
                table: "Session_model",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session_model",
                table: "Session_model",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "signinaccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EmailId = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_signinaccounts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Session_model_AspNetUsers_UserId",
                table: "Session_model",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_model_AspNetUsers_UserId",
                table: "Session_model");

            migrationBuilder.DropTable(
                name: "signinaccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session_model",
                table: "Session_model");

            migrationBuilder.DropColumn(
                name: "LogoutTime",
                table: "Session_model");

            migrationBuilder.RenameTable(
                name: "Session_model",
                newName: "Session_Model");

            migrationBuilder.RenameIndex(
                name: "IX_Session_model_UserId",
                table: "Session_Model",
                newName: "IX_Session_Model_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session_Model",
                table: "Session_Model",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Model_AspNetUsers_UserId",
                table: "Session_Model",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

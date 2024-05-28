using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYChamp.Migrations
{
    /// <inheritdoc />
    public partial class sessionlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_model_AspNetUsers_UserId",
                table: "Session_model");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session_model",
                table: "Session_model");

            migrationBuilder.RenameTable(
                name: "Session_model",
                newName: "sessionlog");

            migrationBuilder.RenameColumn(
                name: "ConfirmPassword",
                table: "signinaccounts",
                newName: "confirmpassword");

            migrationBuilder.RenameIndex(
                name: "IX_Session_model_UserId",
                table: "sessionlog",
                newName: "IX_sessionlog_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployee",
                table: "signinaccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Otp",
                table: "signinaccounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpValiditity",
                table: "signinaccounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "signinaccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "passwordvalidity",
                table: "signinaccounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessionlog",
                table: "sessionlog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sessionlog_AspNetUsers_UserId",
                table: "sessionlog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sessionlog_AspNetUsers_UserId",
                table: "sessionlog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessionlog",
                table: "sessionlog");

            migrationBuilder.DropColumn(
                name: "IsEmployee",
                table: "signinaccounts");

            migrationBuilder.DropColumn(
                name: "Otp",
                table: "signinaccounts");

            migrationBuilder.DropColumn(
                name: "OtpValiditity",
                table: "signinaccounts");

            migrationBuilder.DropColumn(
                name: "active",
                table: "signinaccounts");

            migrationBuilder.DropColumn(
                name: "passwordvalidity",
                table: "signinaccounts");

            migrationBuilder.RenameTable(
                name: "sessionlog",
                newName: "Session_model");

            migrationBuilder.RenameColumn(
                name: "confirmpassword",
                table: "signinaccounts",
                newName: "ConfirmPassword");

            migrationBuilder.RenameIndex(
                name: "IX_sessionlog_UserId",
                table: "Session_model",
                newName: "IX_Session_model_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session_model",
                table: "Session_model",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_model_AspNetUsers_UserId",
                table: "Session_model",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

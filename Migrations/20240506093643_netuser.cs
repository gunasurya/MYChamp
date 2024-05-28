using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYChamp.Migrations
{
    /// <inheritdoc />
    public partial class netuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "signinaccounts",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "signinaccounts",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Otp",
                table: "signinaccounts",
                newName: "otp");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "signinaccounts",
                newName: "middlename");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "signinaccounts",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "IsEmployee",
                table: "signinaccounts",
                newName: "isemployee");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "signinaccounts",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "signinaccounts",
                newName: "emailid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "signinaccounts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OtpValiditity",
                table: "signinaccounts",
                newName: "otpvalidity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LogoutTime",
                table: "sessionlog",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phonenumber",
                table: "signinaccounts",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "signinaccounts",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "otp",
                table: "signinaccounts",
                newName: "Otp");

            migrationBuilder.RenameColumn(
                name: "middlename",
                table: "signinaccounts",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "signinaccounts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "isemployee",
                table: "signinaccounts",
                newName: "IsEmployee");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "signinaccounts",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "emailid",
                table: "signinaccounts",
                newName: "EmailId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "signinaccounts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "otpvalidity",
                table: "signinaccounts",
                newName: "OtpValiditity");

            migrationBuilder.AlterColumn<string>(
                name: "LogoutTime",
                table: "sessionlog",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}

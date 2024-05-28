using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYChamp.Migrations
{
    /// <inheritdoc />
    public partial class register_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Consultees",
                table: "Consultees");

            migrationBuilder.RenameTable(
                name: "Consultees",
                newName: "consultees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_consultees",
                table: "consultees",
                column: "consulteeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_consultees",
                table: "consultees");

            migrationBuilder.RenameTable(
                name: "consultees",
                newName: "Consultees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consultees",
                table: "Consultees",
                column: "consulteeId");
        }
    }
}

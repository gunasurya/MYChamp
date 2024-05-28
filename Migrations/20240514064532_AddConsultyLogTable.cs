using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MYChamp.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultyLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "costPerHour",
                table: "consultees",
                newName: "costperhour");

            migrationBuilder.RenameColumn(
                name: "consulteeName",
                table: "consultees",
                newName: "consulteename");

            migrationBuilder.RenameColumn(
                name: "consulteeEmail",
                table: "consultees",
                newName: "consulteeemail");

            migrationBuilder.RenameColumn(
                name: "UnitOfCurrency",
                table: "consultees",
                newName: "unitofcurrency");

            migrationBuilder.RenameColumn(
                name: "BillingDate",
                table: "consultees",
                newName: "billingdate");

            migrationBuilder.RenameColumn(
                name: "consulteeId",
                table: "consultees",
                newName: "consulteeid");

            migrationBuilder.CreateTable(
                name: "ConsultyLogs",
                columns: table => new
                {
                    logid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    consulteeid = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fromdatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    todatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    duration = table.Column<double>(type: "double precision", nullable: false),
                    cost = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultyLogs", x => x.logid);
                    table.ForeignKey(
                        name: "FK_ConsultyLogs_consultees_consulteeid",
                        column: x => x.consulteeid,
                        principalTable: "consultees",
                        principalColumn: "consulteeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultyLogs_consulteeid",
                table: "ConsultyLogs",
                column: "consulteeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultyLogs");

            migrationBuilder.RenameColumn(
                name: "unitofcurrency",
                table: "consultees",
                newName: "UnitOfCurrency");

            migrationBuilder.RenameColumn(
                name: "costperhour",
                table: "consultees",
                newName: "costPerHour");

            migrationBuilder.RenameColumn(
                name: "consulteename",
                table: "consultees",
                newName: "consulteeName");

            migrationBuilder.RenameColumn(
                name: "consulteeemail",
                table: "consultees",
                newName: "consulteeEmail");

            migrationBuilder.RenameColumn(
                name: "billingdate",
                table: "consultees",
                newName: "BillingDate");

            migrationBuilder.RenameColumn(
                name: "consulteeid",
                table: "consultees",
                newName: "consulteeId");
        }
    }
}

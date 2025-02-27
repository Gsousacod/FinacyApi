using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinacyApi.Migrations
{
    /// <inheritdoc />
    public partial class migrationRefatored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId1",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_MetaFinancials_Users_UserId1",
                table: "MetaFinancials");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Users_UserId1",
                table: "Revenues");

            migrationBuilder.DropTable(
                name: "EmergencyReservation");

            migrationBuilder.DropIndex(
                name: "IX_Revenues_UserId1",
                table: "Revenues");

            migrationBuilder.DropIndex(
                name: "IX_MetaFinancials_UserId1",
                table: "MetaFinancials");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId1",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Revenues");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MetaFinancials");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Expenses");

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyValue",
                table: "MetaFinancials",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyValue",
                table: "MetaFinancials");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Revenues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "MetaFinancials",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmergencyReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    value = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyReservation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_UserId1",
                table: "Revenues",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MetaFinancials_UserId1",
                table: "MetaFinancials",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId1",
                table: "Expenses",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyReservation_UserId",
                table: "EmergencyReservation",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId1",
                table: "Expenses",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MetaFinancials_Users_UserId1",
                table: "MetaFinancials",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Users_UserId1",
                table: "Revenues",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

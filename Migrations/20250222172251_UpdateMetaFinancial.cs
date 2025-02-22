using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinacyApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMetaFinancial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ano",
                table: "MetaFinancials",
                newName: "Year");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "MetaFinancials",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MetaFinancials",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MetaFinancials");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MetaFinancials");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "MetaFinancials",
                newName: "Ano");
        }
    }
}

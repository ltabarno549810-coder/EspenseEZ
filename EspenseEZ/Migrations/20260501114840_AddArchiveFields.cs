using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspenseEZ.Migrations
{
    /// <inheritdoc />
    public partial class AddArchiveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAt",
                table: "Departments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAt",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAt",
                table: "Budgets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Budgets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ArchivedAt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ArchivedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ArchivedAt",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Budgets");
        }
    }
}

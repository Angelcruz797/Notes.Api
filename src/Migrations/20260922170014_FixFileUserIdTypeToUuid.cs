using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Api.src.Migrations
{
    /// <inheritdoc />
    public partial class FixFileUserIdTypeToUuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UserId", table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UserId", table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

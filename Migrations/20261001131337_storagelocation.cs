using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starbase.StationOps.Migrations
{
    /// <inheritdoc />
    public partial class storagelocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Visitors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Visitors");
        }
    }
}

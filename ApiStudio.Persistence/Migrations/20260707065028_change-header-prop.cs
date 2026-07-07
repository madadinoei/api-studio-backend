using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiStudio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeheaderprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ApiRequestHeaders",
                newName: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "ApiRequestHeaders",
                newName: "Name");
        }
    }
}

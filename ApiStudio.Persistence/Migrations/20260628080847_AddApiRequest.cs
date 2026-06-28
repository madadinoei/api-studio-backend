using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiStudio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddApiRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Body_Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Body_Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiRequests_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiRequestHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ApiRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiRequestHeaders_ApiRequests_ApiRequestId",
                        column: x => x.ApiRequestId,
                        principalTable: "ApiRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiRequestQueryParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ApiRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequestQueryParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiRequestQueryParameters_ApiRequests_ApiRequestId",
                        column: x => x.ApiRequestId,
                        principalTable: "ApiRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestHeaders_ApiRequestId",
                table: "ApiRequestHeaders",
                column: "ApiRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequestQueryParameters_ApiRequestId",
                table: "ApiRequestQueryParameters",
                column: "ApiRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiRequests_CollectionId",
                table: "ApiRequests",
                column: "CollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiRequestHeaders");

            migrationBuilder.DropTable(
                name: "ApiRequestQueryParameters");

            migrationBuilder.DropTable(
                name: "ApiRequests");
        }
    }
}

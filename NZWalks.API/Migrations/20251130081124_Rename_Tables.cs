using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_difficulties_DifficultyId",
                table: "walks");

            migrationBuilder.DropForeignKey(
                name: "FK_walks_regions_RegionId",
                table: "walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_walks",
                table: "walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regions",
                table: "regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_difficulties",
                table: "difficulties");

            migrationBuilder.RenameTable(
                name: "walks",
                newName: "Walks");

            migrationBuilder.RenameTable(
                name: "regions",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "difficulties",
                newName: "Difficulties");

            migrationBuilder.RenameIndex(
                name: "IX_walks_RegionId",
                table: "Walks",
                newName: "IX_Walks_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_walks_DifficultyId",
                table: "Walks",
                newName: "IX_Walks_DifficultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Walks",
                table: "Walks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks");

            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Walks",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties");

            migrationBuilder.RenameTable(
                name: "Walks",
                newName: "walks");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "regions");

            migrationBuilder.RenameTable(
                name: "Difficulties",
                newName: "difficulties");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_RegionId",
                table: "walks",
                newName: "IX_walks_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_DifficultyId",
                table: "walks",
                newName: "IX_walks_DifficultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_walks",
                table: "walks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_regions",
                table: "regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_difficulties",
                table: "difficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_walks_difficulties_DifficultyId",
                table: "walks",
                column: "DifficultyId",
                principalTable: "difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_walks_regions_RegionId",
                table: "walks",
                column: "RegionId",
                principalTable: "regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

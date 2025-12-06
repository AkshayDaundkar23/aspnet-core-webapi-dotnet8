using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("093c07df-965f-4fb6-9100-8267090daa6b"), "Hard" },
                    { new Guid("56767633-3468-43af-92b8-7e04ffb2998f"), "Easy" },
                    { new Guid("da76105e-3023-4d57-b647-19e121be308f"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("44fff2a4-40e2-4570-836e-25064d25e9d2"), "AKL", "Auckland", "https://via.placeholder.com/150" },
                    { new Guid("4aa50c3e-3f2c-4776-bd1b-ae6578e0ef56"), "CAN", "Canterbury", "https://via.placeholder.com/800x600" },
                    { new Guid("88930b7e-fb8c-440f-950f-073ec90d8f97"), "NTL", "Northland", "https://via.placeholder.com/300" },
                    { new Guid("9265e976-76b4-4a4e-9abe-ab8b9f1ac9e6"), "WLG", "Wellington", "https://picsum.photos/300/200" },
                    { new Guid("cd03fda5-385b-4426-afd3-9633cd4bdf7b"), "BOP", "Bay of Plenty", "https://via.placeholder.com/600x400" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("093c07df-965f-4fb6-9100-8267090daa6b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("56767633-3468-43af-92b8-7e04ffb2998f"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("da76105e-3023-4d57-b647-19e121be308f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("44fff2a4-40e2-4570-836e-25064d25e9d2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4aa50c3e-3f2c-4776-bd1b-ae6578e0ef56"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("88930b7e-fb8c-440f-950f-073ec90d8f97"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9265e976-76b4-4a4e-9abe-ab8b9f1ac9e6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cd03fda5-385b-4426-afd3-9633cd4bdf7b"));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandLeaser.API.Migrations
{
    /// <inheritdoc />
    public partial class FixedDatabaseRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingImage_Listings_ListingId",
                table: "ListingImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingImage",
                table: "ListingImage");

            migrationBuilder.RenameTable(
                name: "ListingImage",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_ListingImage_ListingId",
                table: "Images",
                newName: "IX_Images_ListingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ListingImage");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ListingId",
                table: "ListingImage",
                newName: "IX_ListingImage_ListingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingImage",
                table: "ListingImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingImage_Listings_ListingId",
                table: "ListingImage",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

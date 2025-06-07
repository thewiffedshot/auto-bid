using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoBid.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Auction1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarAuctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    StartingPrice = table.Column<decimal>(nullable: true),
                    CurrentPrice = table.Column<decimal>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarAuctions", x => x.Id);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "CarAuctionId",
                table: "CarOffers",
                nullable: true,
                defaultValue: Guid.NewGuid());

            migrationBuilder.CreateIndex(
                name: "IX_CarOffers_CarAuctionId",
                table: "CarOffers",
                column: "CarAuctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarAuctions");

            migrationBuilder.DropIndex(
                name: "IX_CarOffers_CarAuctionId",
                table: "CarOffers"
            );

            migrationBuilder.DropColumn(
                name: "CarAuctionId",
                table: "CarOffers"
            );
        }
    }
}

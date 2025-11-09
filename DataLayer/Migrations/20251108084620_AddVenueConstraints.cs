using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddVenueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Venue_Latitude",
                table: "Venues",
                sql: "Latitude >= -90 AND Latitude <= 90");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Venue_Longitude",
                table: "Venues",
                sql: "Longitude >= -180 AND Longitude <= 180");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Venue_Latitude",
                table: "Venues");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Venue_Longitude",
                table: "Venues");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirportSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing tables
            migrationBuilder.DropTable(name: "AirportGroupAirport");
            migrationBuilder.DropTable(name: "Route");
            migrationBuilder.DropTable(name: "Airport");
            migrationBuilder.DropTable(name: "AirportGroup");
            migrationBuilder.DropTable(name: "GeographyLevel1");

            migrationBuilder.CreateTable(
                name: "AirportGroup",
                columns: table => new
                {
                    AirportGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportGroup", x => x.AirportGroupID);
                });

            migrationBuilder.CreateTable(
                name: "GeographyLevel1",
                columns: table => new
                {
                    GeographyLevel1ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeographyLevel1", x => x.GeographyLevel1ID);
                });

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    AirportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IATACode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeographyLevel1ID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.AirportID);
                    table.ForeignKey(
                        name: "FK_Airport_GeographyLevel1_GeographyLevel1ID",
                        column: x => x.GeographyLevel1ID,
                        principalTable: "GeographyLevel1",
                        principalColumn: "GeographyLevel1ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirportGroupAirport",
                columns: table => new
                {
                    AirportGroupID = table.Column<int>(type: "int", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportGroupAirport", x => new { x.AirportGroupID, x.AirportID });
                    table.ForeignKey(
                        name: "FK_AirportGroupAirport_AirportGroup_AirportGroupID",
                        column: x => x.AirportGroupID,
                        principalTable: "AirportGroup",
                        principalColumn: "AirportGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirportGroupAirport_Airport_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airport",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureAirportID = table.Column<int>(type: "int", nullable: false),
                    ArrivalAirportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK_Route_Airport_ArrivalAirportID",
                        column: x => x.ArrivalAirportID,
                        principalTable: "Airport",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Route_Airport_DepartureAirportID",
                        column: x => x.DepartureAirportID,
                        principalTable: "Airport",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AirportGroup",
                columns: new[] { "AirportGroupID", "Name" },
                values: new object[,]
                {
                    { 1, "Group A" },
                    { 2, "Group B" }
                });

            migrationBuilder.InsertData(
                table: "GeographyLevel1",
                columns: new[] { "GeographyLevel1ID", "Name" },
                values: new object[,]
                {
                    { 1, "United Kingdom" },
                    { 2, "Spain" },
                    { 3, "United States" },
                    { 4, "Turkey" }
                });

            migrationBuilder.InsertData(
                table: "Airport",
                columns: new[] { "AirportID", "GeographyLevel1ID", "IATACode", "Type" },
                values: new object[,]
                {
                    { 1, 1, "LGW", "Departure and Arrival" },
                    { 2, 2, "PMI", "Arrival Only" },
                    { 3, 3, "LAX", "Arrival Only" }
                });

            migrationBuilder.InsertData(
                table: "AirportGroupAirport",
                columns: new[] { "AirportGroupID", "AirportID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Route",
                columns: new[] { "RouteID", "ArrivalAirportID", "DepartureAirportID" },
                values: new object[] { 1, 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Airport_GeographyLevel1ID",
                table: "Airport",
                column: "GeographyLevel1ID");

            migrationBuilder.CreateIndex(
                name: "IX_AirportGroupAirport_AirportID",
                table: "AirportGroupAirport",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Route_ArrivalAirportID",
                table: "Route",
                column: "ArrivalAirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Route_DepartureAirportID",
                table: "Route",
                column: "DepartureAirportID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop tables
            migrationBuilder.DropTable(name: "AirportGroupAirport");
            migrationBuilder.DropTable(name: "Route");
            migrationBuilder.DropTable(name: "Airport");
            migrationBuilder.DropTable(name: "AirportGroup");
            migrationBuilder.DropTable(name: "GeographyLevel1");
        }
    }
}

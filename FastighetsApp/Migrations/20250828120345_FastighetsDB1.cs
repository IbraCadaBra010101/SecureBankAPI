using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastighetsAPI.Migrations
{
    /// <inheritdoc />
    public partial class FastighetsDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    ApartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    RentPerMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeaseStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaseEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.ApartmentId);
                    table.ForeignKey(
                        name: "FK_Apartments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Email", "Name", "OrganizationNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("41c47814-079a-45b1-9969-e12ba6707636"), "kontakt@skandiabostad.se", "Skandia Bostad", "556000-0004", "+46 8 765 432" },
                    { new Guid("bc62289a-fa3e-473e-a2a1-c0f3fd8d0d8d"), "contact@cityhomes.se", "City Homes Sverige", "556000-0002", "+46 31 987 654" },
                    { new Guid("c009e702-4806-47fc-83b6-c056418492fe"), "info@nordicestates.se", "Nordic Estates AB", "556000-0001", "+46 8 123 456" },
                    { new Guid("cb0f419b-bab2-4364-9048-7a875aab2677"), "info@riksbyggen.se", "Riksbyggen Testfastigheter", "716000-0003", "+46 10 123 45 67" }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "ApartmentId", "Address", "CompanyId", "Floor", "IsOccupied", "LeaseEndDate", "LeaseStartDate", "RentPerMonth", "Rooms" },
                values: new object[,]
                {
                    { new Guid("72579826-b4e3-4940-9f30-1636d73288ad"), "Linnégatan 22, Göteborg", new Guid("bc62289a-fa3e-473e-a2a1-c0f3fd8d0d8d"), 4, true, new DateTime(2025, 11, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(609), new DateTime(2024, 5, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(608), 11250m, 2 },
                    { new Guid("76a40193-b6d0-407a-ac9c-0e3c00850e7c"), "Avenyn 10, Göteborg", new Guid("bc62289a-fa3e-473e-a2a1-c0f3fd8d0d8d"), 3, false, new DateTime(2025, 9, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(606), new DateTime(2024, 10, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(605), 9500m, 1 },
                    { new Guid("a44efcf4-162c-4d33-bcf3-8637c173cd55"), "Södra Vägen 7, Malmö", new Guid("cb0f419b-bab2-4364-9048-7a875aab2677"), 1, true, new DateTime(2026, 1, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(611), new DateTime(2022, 8, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(611), 8200m, 1 },
                    { new Guid("c71316e7-b6f5-4ddd-a1da-449352c2fbb5"), "Sveavägen 100, Stockholm", new Guid("41c47814-079a-45b1-9969-e12ba6707636"), 7, false, new DateTime(2026, 7, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(616), new DateTime(2025, 2, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(615), 17200m, 3 },
                    { new Guid("de5e1fee-f23b-498d-823a-139daef7625e"), "Lilla Nygatan 5, Stockholm", new Guid("c009e702-4806-47fc-83b6-c056418492fe"), 5, true, new DateTime(2026, 4, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(603), new DateTime(2023, 8, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(602), 16500m, 3 },
                    { new Guid("e775375f-3fe8-45dd-8fa6-0d4d8c04bf0e"), "Kungsportsavenyen 3, Malmö", new Guid("cb0f419b-bab2-4364-9048-7a875aab2677"), 6, true, new DateTime(2025, 9, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(614), new DateTime(2024, 8, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(613), 18950m, 4 },
                    { new Guid("f437fd52-af7a-4f50-baea-4a31467ef24b"), "Storgatan 1, Stockholm", new Guid("c009e702-4806-47fc-83b6-c056418492fe"), 2, true, new DateTime(2025, 10, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(598), new DateTime(2024, 8, 28, 12, 3, 44, 774, DateTimeKind.Utc).AddTicks(590), 12000m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_CompanyId",
                table: "Apartments",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}

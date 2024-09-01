using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecureBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    InvestmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvestmentCategory = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateInvested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RiskLevel = table.Column<int>(type: "int", nullable: false),
                    InvestmentStatus = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.InvestmentId);
                    table.ForeignKey(
                        name: "FK_Investments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "DateOfBirth", "DateRegistered", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("078d3967-e133-42a9-b9b4-22803be54c98"), new DateTime(1982, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1068), "client3@example.com", "ClientFirstName3", "ClientLastName3", "555-0103" },
                    { new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1012), "client1@example.com", "ClientFirstName1", "ClientLastName1", "555-0101" },
                    { new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"), new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1097), "client8@example.com", "ClientFirstName8", "ClientLastName8", "555-0108" },
                    { new Guid("41098a16-6690-4683-8263-1c3c81705cd5"), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1116), "client10@example.com", "ClientFirstName10", "ClientLastName10", "555-01010" },
                    { new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"), new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1073), "client4@example.com", "ClientFirstName4", "ClientLastName4", "555-0104" },
                    { new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"), new DateTime(1981, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1062), "client2@example.com", "ClientFirstName2", "ClientLastName2", "555-0102" },
                    { new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1083), "client6@example.com", "ClientFirstName6", "ClientLastName6", "555-0106" },
                    { new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"), new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1078), "client5@example.com", "ClientFirstName5", "ClientLastName5", "555-0105" },
                    { new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"), new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1105), "client9@example.com", "ClientFirstName9", "ClientLastName9", "555-0109" },
                    { new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"), new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1089), "client7@example.com", "ClientFirstName7", "ClientLastName7", "555-0107" }
                });

            migrationBuilder.InsertData(
                table: "Investments",
                columns: new[] { "InvestmentId", "Amount", "ClientId", "CurrentValue", "DateInvested", "InvestmentCategory", "InvestmentStatus", "RiskLevel" },
                values: new object[,]
                {
                    { new Guid("0067c6d3-f262-4e17-96a5-bf929d238d99"), 26879m, new Guid("078d3967-e133-42a9-b9b4-22803be54c98"), 9578m, new DateTime(2024, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1314), 1, 0, 1 },
                    { new Guid("19931a6a-34c3-4ee5-a011-12640d7ae603"), 41970m, new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"), 25101m, new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1300), 1, 0, 3 },
                    { new Guid("1a9867b4-91eb-451e-9f53-da7cd02bc0ce"), 33736m, new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"), 20467m, new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1409), 2, 0, 3 },
                    { new Guid("22a555af-dd79-4961-ab02-2a1a574fdd7c"), 17083m, new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"), 17505m, new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1309), 4, 0, 1 },
                    { new Guid("320f98fb-a452-4631-bf15-25ef1ad7132e"), 31838m, new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"), 11045m, new DateTime(2023, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1342), 3, 0, 2 },
                    { new Guid("334664b3-5742-4fbb-8f78-261bb9878f22"), 1489m, new Guid("078d3967-e133-42a9-b9b4-22803be54c98"), 8473m, new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1317), 1, 0, 1 },
                    { new Guid("339f2074-c5bb-42fd-9960-35e89dcc82c6"), 33094m, new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"), 35138m, new DateTime(2024, 1, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1391), 3, 0, 2 },
                    { new Guid("3ad9c048-e583-48a1-a960-50433a2837ff"), 13564m, new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"), 4907m, new DateTime(2024, 4, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1398), 4, 0, 3 },
                    { new Guid("3f7e8b85-29eb-4102-98cf-2c10e2ac7f84"), 32419m, new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"), 11816m, new DateTime(2023, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1396), 2, 0, 3 },
                    { new Guid("4665cd39-da2c-4010-bd4e-5ca1882e78c9"), 2228m, new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"), 4678m, new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1334), 3, 0, 1 },
                    { new Guid("49467973-7709-4cf0-86af-5128a2762b32"), 40427m, new Guid("41098a16-6690-4683-8263-1c3c81705cd5"), 25755m, new DateTime(2024, 8, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1422), 3, 0, 3 },
                    { new Guid("49f0973a-9e79-487c-8bbd-fd6a28ffaeb7"), 49764m, new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"), 29591m, new DateTime(2023, 11, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1401), 3, 0, 3 },
                    { new Guid("62e70d31-4ee9-41d5-aa25-3084549e60ac"), 10800m, new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"), 3807m, new DateTime(2024, 1, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1324), 2, 0, 2 },
                    { new Guid("6ffe3309-92b1-4811-aff2-6e07757b97ed"), 12446m, new Guid("41098a16-6690-4683-8263-1c3c81705cd5"), 28768m, new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1420), 1, 0, 1 },
                    { new Guid("714b7418-91f6-4734-8708-827895bc9eee"), 9189m, new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"), 42777m, new DateTime(2023, 12, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1297), 1, 0, 3 },
                    { new Guid("7fb95ea4-c9a6-4c77-b8e8-71a7f9329a0c"), 46528m, new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"), 6382m, new DateTime(2024, 4, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1412), 1, 0, 3 },
                    { new Guid("7fe61357-d75f-4d9f-a8ba-f24ca8b2038b"), 13971m, new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"), 45995m, new DateTime(2023, 10, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1339), 1, 0, 2 },
                    { new Guid("8819962f-7568-4b61-bf69-d8dccdced7a4"), 37088m, new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"), 10133m, new DateTime(2024, 1, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1404), 4, 0, 1 },
                    { new Guid("90652a5c-a4f9-4281-a92d-75556fb6c71f"), 21853m, new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"), 49166m, new DateTime(2024, 8, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1321), 4, 0, 3 },
                    { new Guid("91b860c0-04f6-41ff-b08c-3a700cf151e8"), 12011m, new Guid("078d3967-e133-42a9-b9b4-22803be54c98"), 28851m, new DateTime(2023, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1312), 3, 0, 3 },
                    { new Guid("95f5acf2-f82b-4a51-b543-c6489ae24b9a"), 1766m, new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"), 33301m, new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1385), 3, 0, 3 },
                    { new Guid("a0b0c145-0136-4e38-bbc9-be684f146b92"), 39339m, new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"), 24341m, new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1331), 2, 0, 2 },
                    { new Guid("af9bb4b0-1743-4f88-890c-308b4f6e8e5a"), 38048m, new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"), 30977m, new DateTime(2024, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1294), 1, 0, 1 },
                    { new Guid("b66e2213-1d8b-4249-95ef-08132728f592"), 33118m, new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"), 31148m, new DateTime(2023, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1337), 1, 0, 3 },
                    { new Guid("b7ce5c5b-e5b4-485c-9c10-994d0c6de459"), 48763m, new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"), 23128m, new DateTime(2024, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1406), 3, 0, 2 },
                    { new Guid("c7cbfd70-3bc8-49c0-9355-e00f819d7e6b"), 38871m, new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"), 4625m, new DateTime(2024, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1285), 2, 0, 1 },
                    { new Guid("cc41e557-0ff8-4e27-81f1-e428fff5cd8e"), 7643m, new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"), 26048m, new DateTime(2023, 10, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1388), 3, 0, 2 },
                    { new Guid("d2468ad8-d567-4a01-a81e-afe2754f65dd"), 3179m, new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"), 45862m, new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1306), 4, 0, 1 },
                    { new Guid("d5902825-3733-4c85-886c-ee9bbc73fffe"), 7976m, new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"), 18764m, new DateTime(2024, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1326), 2, 0, 1 },
                    { new Guid("dd522f27-d9ba-491c-9aa4-f640ebb42b17"), 42294m, new Guid("41098a16-6690-4683-8263-1c3c81705cd5"), 16166m, new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1415), 4, 0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investments_ClientId",
                table: "Investments",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}

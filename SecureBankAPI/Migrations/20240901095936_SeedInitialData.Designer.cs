﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecureBankAPI.Data;

#nullable disable

namespace SecureBankAPI.Migrations
{
    [DbContext(typeof(SecureBankDBContext))]
    [Migration("20240901095936_SeedInitialData")]
    partial class SeedInitialData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SecureBankAPI.Models.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRegistered")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ClientId = new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"),
                            DateOfBirth = new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1012),
                            Email = "client1@example.com",
                            FirstName = "ClientFirstName1",
                            LastName = "ClientLastName1",
                            PhoneNumber = "555-0101"
                        },
                        new
                        {
                            ClientId = new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"),
                            DateOfBirth = new DateTime(1981, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1062),
                            Email = "client2@example.com",
                            FirstName = "ClientFirstName2",
                            LastName = "ClientLastName2",
                            PhoneNumber = "555-0102"
                        },
                        new
                        {
                            ClientId = new Guid("078d3967-e133-42a9-b9b4-22803be54c98"),
                            DateOfBirth = new DateTime(1982, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1068),
                            Email = "client3@example.com",
                            FirstName = "ClientFirstName3",
                            LastName = "ClientLastName3",
                            PhoneNumber = "555-0103"
                        },
                        new
                        {
                            ClientId = new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"),
                            DateOfBirth = new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1073),
                            Email = "client4@example.com",
                            FirstName = "ClientFirstName4",
                            LastName = "ClientLastName4",
                            PhoneNumber = "555-0104"
                        },
                        new
                        {
                            ClientId = new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"),
                            DateOfBirth = new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1078),
                            Email = "client5@example.com",
                            FirstName = "ClientFirstName5",
                            LastName = "ClientLastName5",
                            PhoneNumber = "555-0105"
                        },
                        new
                        {
                            ClientId = new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"),
                            DateOfBirth = new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1083),
                            Email = "client6@example.com",
                            FirstName = "ClientFirstName6",
                            LastName = "ClientLastName6",
                            PhoneNumber = "555-0106"
                        },
                        new
                        {
                            ClientId = new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"),
                            DateOfBirth = new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1089),
                            Email = "client7@example.com",
                            FirstName = "ClientFirstName7",
                            LastName = "ClientLastName7",
                            PhoneNumber = "555-0107"
                        },
                        new
                        {
                            ClientId = new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"),
                            DateOfBirth = new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1097),
                            Email = "client8@example.com",
                            FirstName = "ClientFirstName8",
                            LastName = "ClientLastName8",
                            PhoneNumber = "555-0108"
                        },
                        new
                        {
                            ClientId = new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"),
                            DateOfBirth = new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1105),
                            Email = "client9@example.com",
                            FirstName = "ClientFirstName9",
                            LastName = "ClientLastName9",
                            PhoneNumber = "555-0109"
                        },
                        new
                        {
                            ClientId = new Guid("41098a16-6690-4683-8263-1c3c81705cd5"),
                            DateOfBirth = new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateRegistered = new DateTime(2024, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1116),
                            Email = "client10@example.com",
                            FirstName = "ClientFirstName10",
                            LastName = "ClientLastName10",
                            PhoneNumber = "555-01010"
                        });
                });

            modelBuilder.Entity("SecureBankAPI.Models.Investment", b =>
                {
                    b.Property<Guid>("InvestmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateInvested")
                        .HasColumnType("datetime2");

                    b.Property<int>("InvestmentCategory")
                        .HasColumnType("int");

                    b.Property<int>("InvestmentStatus")
                        .HasColumnType("int");

                    b.Property<int>("RiskLevel")
                        .HasColumnType("int");

                    b.HasKey("InvestmentId");

                    b.HasIndex("ClientId");

                    b.ToTable("Investments");

                    b.HasData(
                        new
                        {
                            InvestmentId = new Guid("c7cbfd70-3bc8-49c0-9355-e00f819d7e6b"),
                            Amount = 38871m,
                            ClientId = new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"),
                            CurrentValue = 4625m,
                            DateInvested = new DateTime(2024, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1285),
                            InvestmentCategory = 2,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("af9bb4b0-1743-4f88-890c-308b4f6e8e5a"),
                            Amount = 38048m,
                            ClientId = new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"),
                            CurrentValue = 30977m,
                            DateInvested = new DateTime(2024, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1294),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("714b7418-91f6-4734-8708-827895bc9eee"),
                            Amount = 9189m,
                            ClientId = new Guid("2cdbb611-9938-4256-8f7a-a9014da3d5a0"),
                            CurrentValue = 42777m,
                            DateInvested = new DateTime(2023, 12, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1297),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("19931a6a-34c3-4ee5-a011-12640d7ae603"),
                            Amount = 41970m,
                            ClientId = new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"),
                            CurrentValue = 25101m,
                            DateInvested = new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1300),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("d2468ad8-d567-4a01-a81e-afe2754f65dd"),
                            Amount = 3179m,
                            ClientId = new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"),
                            CurrentValue = 45862m,
                            DateInvested = new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1306),
                            InvestmentCategory = 4,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("22a555af-dd79-4961-ab02-2a1a574fdd7c"),
                            Amount = 17083m,
                            ClientId = new Guid("7afe780a-2c87-4bae-8c8a-6075b763fd77"),
                            CurrentValue = 17505m,
                            DateInvested = new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1309),
                            InvestmentCategory = 4,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("91b860c0-04f6-41ff-b08c-3a700cf151e8"),
                            Amount = 12011m,
                            ClientId = new Guid("078d3967-e133-42a9-b9b4-22803be54c98"),
                            CurrentValue = 28851m,
                            DateInvested = new DateTime(2023, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1312),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("0067c6d3-f262-4e17-96a5-bf929d238d99"),
                            Amount = 26879m,
                            ClientId = new Guid("078d3967-e133-42a9-b9b4-22803be54c98"),
                            CurrentValue = 9578m,
                            DateInvested = new DateTime(2024, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1314),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("334664b3-5742-4fbb-8f78-261bb9878f22"),
                            Amount = 1489m,
                            ClientId = new Guid("078d3967-e133-42a9-b9b4-22803be54c98"),
                            CurrentValue = 8473m,
                            DateInvested = new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1317),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("90652a5c-a4f9-4281-a92d-75556fb6c71f"),
                            Amount = 21853m,
                            ClientId = new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"),
                            CurrentValue = 49166m,
                            DateInvested = new DateTime(2024, 8, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1321),
                            InvestmentCategory = 4,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("62e70d31-4ee9-41d5-aa25-3084549e60ac"),
                            Amount = 10800m,
                            ClientId = new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"),
                            CurrentValue = 3807m,
                            DateInvested = new DateTime(2024, 1, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1324),
                            InvestmentCategory = 2,
                            InvestmentStatus = 0,
                            RiskLevel = 2
                        },
                        new
                        {
                            InvestmentId = new Guid("d5902825-3733-4c85-886c-ee9bbc73fffe"),
                            Amount = 7976m,
                            ClientId = new Guid("6dcae316-ed8e-4593-a9c0-4ae3cc48383f"),
                            CurrentValue = 18764m,
                            DateInvested = new DateTime(2024, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1326),
                            InvestmentCategory = 2,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("a0b0c145-0136-4e38-bbc9-be684f146b92"),
                            Amount = 39339m,
                            ClientId = new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"),
                            CurrentValue = 24341m,
                            DateInvested = new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1331),
                            InvestmentCategory = 2,
                            InvestmentStatus = 0,
                            RiskLevel = 2
                        },
                        new
                        {
                            InvestmentId = new Guid("4665cd39-da2c-4010-bd4e-5ca1882e78c9"),
                            Amount = 2228m,
                            ClientId = new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"),
                            CurrentValue = 4678m,
                            DateInvested = new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1334),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("b66e2213-1d8b-4249-95ef-08132728f592"),
                            Amount = 33118m,
                            ClientId = new Guid("d72c6377-804f-48e2-843a-d6d73d139a5d"),
                            CurrentValue = 31148m,
                            DateInvested = new DateTime(2023, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1337),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("7fe61357-d75f-4d9f-a8ba-f24ca8b2038b"),
                            Amount = 13971m,
                            ClientId = new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"),
                            CurrentValue = 45995m,
                            DateInvested = new DateTime(2023, 10, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1339),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 2
                        },
                        new
                        {
                            InvestmentId = new Guid("320f98fb-a452-4631-bf15-25ef1ad7132e"),
                            Amount = 31838m,
                            ClientId = new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"),
                            CurrentValue = 11045m,
                            DateInvested = new DateTime(2023, 9, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1342),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 2
                        },
                        new
                        {
                            InvestmentId = new Guid("95f5acf2-f82b-4a51-b543-c6489ae24b9a"),
                            Amount = 1766m,
                            ClientId = new Guid("8db5a940-42d2-41f4-b819-c8bbb4758b5b"),
                            CurrentValue = 33301m,
                            DateInvested = new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1385),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("cc41e557-0ff8-4e27-81f1-e428fff5cd8e"),
                            Amount = 7643m,
                            ClientId = new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"),
                            CurrentValue = 26048m,
                            DateInvested = new DateTime(2023, 10, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1388),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 2
                        },
                        new
                        {
                            InvestmentId = new Guid("339f2074-c5bb-42fd-9960-35e89dcc82c6"),
                            Amount = 33094m,
                            ClientId = new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"),
                            CurrentValue = 35138m,
                            DateInvested = new DateTime(2024, 1, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1391),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 2
                        },
                        new
                        {
                            InvestmentId = new Guid("3f7e8b85-29eb-4102-98cf-2c10e2ac7f84"),
                            Amount = 32419m,
                            ClientId = new Guid("e6d041a8-6275-4a5e-8d05-2831b3c9a360"),
                            CurrentValue = 11816m,
                            DateInvested = new DateTime(2023, 7, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1396),
                            InvestmentCategory = 2,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("3ad9c048-e583-48a1-a960-50433a2837ff"),
                            Amount = 13564m,
                            ClientId = new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"),
                            CurrentValue = 4907m,
                            DateInvested = new DateTime(2024, 4, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1398),
                            InvestmentCategory = 4,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("49f0973a-9e79-487c-8bbd-fd6a28ffaeb7"),
                            Amount = 49764m,
                            ClientId = new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"),
                            CurrentValue = 29591m,
                            DateInvested = new DateTime(2023, 11, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1401),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("8819962f-7568-4b61-bf69-d8dccdced7a4"),
                            Amount = 37088m,
                            ClientId = new Guid("33faaa84-67c2-4a03-b0c4-18dcb739583f"),
                            CurrentValue = 10133m,
                            DateInvested = new DateTime(2024, 1, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1404),
                            InvestmentCategory = 4,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("b7ce5c5b-e5b4-485c-9c10-994d0c6de459"),
                            Amount = 48763m,
                            ClientId = new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"),
                            CurrentValue = 23128m,
                            DateInvested = new DateTime(2024, 6, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1406),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 2
                        },
                        new
                        {
                            InvestmentId = new Guid("1a9867b4-91eb-451e-9f53-da7cd02bc0ce"),
                            Amount = 33736m,
                            ClientId = new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"),
                            CurrentValue = 20467m,
                            DateInvested = new DateTime(2023, 3, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1409),
                            InvestmentCategory = 2,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("7fb95ea4-c9a6-4c77-b8e8-71a7f9329a0c"),
                            Amount = 46528m,
                            ClientId = new Guid("dc61c81e-e77d-4a2f-b97a-9e0ffa39e7e3"),
                            CurrentValue = 6382m,
                            DateInvested = new DateTime(2024, 4, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1412),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        },
                        new
                        {
                            InvestmentId = new Guid("dd522f27-d9ba-491c-9aa4-f640ebb42b17"),
                            Amount = 42294m,
                            ClientId = new Guid("41098a16-6690-4683-8263-1c3c81705cd5"),
                            CurrentValue = 16166m,
                            DateInvested = new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1415),
                            InvestmentCategory = 4,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("6ffe3309-92b1-4811-aff2-6e07757b97ed"),
                            Amount = 12446m,
                            ClientId = new Guid("41098a16-6690-4683-8263-1c3c81705cd5"),
                            CurrentValue = 28768m,
                            DateInvested = new DateTime(2024, 2, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1420),
                            InvestmentCategory = 1,
                            InvestmentStatus = 0,
                            RiskLevel = 1
                        },
                        new
                        {
                            InvestmentId = new Guid("49467973-7709-4cf0-86af-5128a2762b32"),
                            Amount = 40427m,
                            ClientId = new Guid("41098a16-6690-4683-8263-1c3c81705cd5"),
                            CurrentValue = 25755m,
                            DateInvested = new DateTime(2024, 8, 1, 11, 59, 36, 245, DateTimeKind.Local).AddTicks(1422),
                            InvestmentCategory = 3,
                            InvestmentStatus = 0,
                            RiskLevel = 3
                        });
                });

            modelBuilder.Entity("SecureBankAPI.Models.Investment", b =>
                {
                    b.HasOne("SecureBankAPI.Models.Client", "Client")
                        .WithMany("Investments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("SecureBankAPI.Models.Client", b =>
                {
                    b.Navigation("Investments");
                });
#pragma warning restore 612, 618
        }
    }
}

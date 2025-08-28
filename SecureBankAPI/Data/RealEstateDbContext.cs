// <copyright file="RealEstateDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using RealEstateAPI.Models;

    /// <summary>
    /// Represents the session with the database, providing access to the data and handling transactions.
    /// </summary>
    public class RealEstateDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateDbContext"/> class.
        /// </summary>
        /// <param name="options">The options used to configure the context.</param>
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets companies entity.
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Gets or sets apartments entity.
        /// </summary>
        public DbSet<Apartment> Apartments { get; set; }

        /// <summary>
        /// Configures the entity mappings and relationships for the context.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(company =>
            {
                company.HasKey(c => c.CompanyId);
                company.Property(c => c.Name).IsRequired();
                company.HasMany(c => c.Apartments)
                       .WithOne(a => a.Company)
                       .HasForeignKey(a => a.CompanyId);

                var company1Id = Guid.NewGuid();
                var company2Id = Guid.NewGuid();
                var company3Id = Guid.NewGuid();
                var company4Id = Guid.NewGuid();

                modelBuilder.Entity<Company>().HasData(
                    new Company { CompanyId = company1Id, Name = "Nordic Estates AB", OrganizationNumber = "556000-0001", Email = "info@nordicestates.se", PhoneNumber = "+46 8 123 456" },
                    new Company { CompanyId = company2Id, Name = "City Homes Sverige", OrganizationNumber = "556000-0002", Email = "contact@cityhomes.se", PhoneNumber = "+46 31 987 654" },
                    new Company { CompanyId = company3Id, Name = "Riksbyggen Testfastigheter", OrganizationNumber = "716000-0003", Email = "info@riksbyggen.se", PhoneNumber = "+46 10 123 45 67" },
                    new Company { CompanyId = company4Id, Name = "Skandia Bostad", OrganizationNumber = "556000-0004", Email = "kontakt@skandiabostad.se", PhoneNumber = "+46 8 765 432" }
                );

                var apt1Id = Guid.NewGuid();
                var apt2Id = Guid.NewGuid();
                var apt3Id = Guid.NewGuid();
                var apt4Id = Guid.NewGuid();
                var apt5Id = Guid.NewGuid();
                var apt6Id = Guid.NewGuid();
                var apt7Id = Guid.NewGuid();

                modelBuilder.Entity<Apartment>().Property(a => a.RentPerMonth).HasColumnType("decimal(18,2)");

                modelBuilder.Entity<Apartment>().HasData(
                    new Apartment { ApartmentId = apt1Id, CompanyId = company1Id, Address = "Storgatan 1, Stockholm", Floor = 2, Rooms = 2, RentPerMonth = 12000, LeaseStartDate = DateTime.UtcNow.AddYears(-1), LeaseEndDate = DateTime.UtcNow.AddMonths(2), IsOccupied = true },
                    new Apartment { ApartmentId = apt2Id, CompanyId = company1Id, Address = "Lilla Nygatan 5, Stockholm", Floor = 5, Rooms = 3, RentPerMonth = 16500, LeaseStartDate = DateTime.UtcNow.AddYears(-2), LeaseEndDate = DateTime.UtcNow.AddMonths(8), IsOccupied = true },
                    new Apartment { ApartmentId = apt3Id, CompanyId = company2Id, Address = "Avenyn 10, Göteborg", Floor = 3, Rooms = 1, RentPerMonth = 9500, LeaseStartDate = DateTime.UtcNow.AddMonths(-10), LeaseEndDate = DateTime.UtcNow.AddMonths(1), IsOccupied = false },
                    new Apartment { ApartmentId = apt4Id, CompanyId = company2Id, Address = "Linnégatan 22, Göteborg", Floor = 4, Rooms = 2, RentPerMonth = 11250, LeaseStartDate = DateTime.UtcNow.AddMonths(-15), LeaseEndDate = DateTime.UtcNow.AddMonths(3), IsOccupied = true },
                    new Apartment { ApartmentId = apt5Id, CompanyId = company3Id, Address = "Södra Vägen 7, Malmö", Floor = 1, Rooms = 1, RentPerMonth = 8200, LeaseStartDate = DateTime.UtcNow.AddYears(-3), LeaseEndDate = DateTime.UtcNow.AddMonths(5), IsOccupied = true },
                    new Apartment { ApartmentId = apt6Id, CompanyId = company3Id, Address = "Kungsportsavenyen 3, Malmö", Floor = 6, Rooms = 4, RentPerMonth = 18950, LeaseStartDate = DateTime.UtcNow.AddYears(-1), LeaseEndDate = DateTime.UtcNow.AddMonths(1), IsOccupied = true },
                    new Apartment { ApartmentId = apt7Id, CompanyId = company4Id, Address = "Sveavägen 100, Stockholm", Floor = 7, Rooms = 3, RentPerMonth = 17200, LeaseStartDate = DateTime.UtcNow.AddMonths(-6), LeaseEndDate = DateTime.UtcNow.AddMonths(11), IsOccupied = false });
            });
        }
    }
}
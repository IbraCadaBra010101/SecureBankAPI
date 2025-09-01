// <copyright file="RealEstateDbContext.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Data
{
    using System;
    using FastighetsAPI.Models.DataModels;
    using Microsoft.EntityFrameworkCore;

    public class RealEstateDbContext : DbContext
    {

        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Apartment> Apartments { get; set; }

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
                    new Apartment { ApartmentId = apt3Id, CompanyId = company2Id, Address = "Avenyn 10, G�teborg", Floor = 3, Rooms = 1, RentPerMonth = 9500, LeaseStartDate = DateTime.UtcNow.AddMonths(-10), LeaseEndDate = DateTime.UtcNow.AddMonths(1), IsOccupied = false },
                    new Apartment { ApartmentId = apt4Id, CompanyId = company2Id, Address = "Linn�gatan 22, G�teborg", Floor = 4, Rooms = 2, RentPerMonth = 11250, LeaseStartDate = DateTime.UtcNow.AddMonths(-15), LeaseEndDate = DateTime.UtcNow.AddMonths(3), IsOccupied = true },
                    new Apartment { ApartmentId = apt5Id, CompanyId = company3Id, Address = "S�dra V�gen 7, Malm�", Floor = 1, Rooms = 1, RentPerMonth = 8200, LeaseStartDate = DateTime.UtcNow.AddYears(-3), LeaseEndDate = DateTime.UtcNow.AddMonths(5), IsOccupied = true },
                    new Apartment { ApartmentId = apt6Id, CompanyId = company3Id, Address = "Kungsportsavenyen 3, Malm�", Floor = 6, Rooms = 4, RentPerMonth = 18950, LeaseStartDate = DateTime.UtcNow.AddYears(-1), LeaseEndDate = DateTime.UtcNow.AddMonths(1), IsOccupied = true },
                    new Apartment { ApartmentId = apt7Id, CompanyId = company4Id, Address = "Sveav�gen 100, Stockholm", Floor = 7, Rooms = 3, RentPerMonth = 17200, LeaseStartDate = DateTime.UtcNow.AddMonths(-6), LeaseEndDate = DateTime.UtcNow.AddMonths(11), IsOccupied = false });
            });
        }
    }
}
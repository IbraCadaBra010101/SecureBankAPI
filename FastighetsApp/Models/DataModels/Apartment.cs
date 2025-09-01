// <copyright file="Apartment.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Apartment
    {
    
        [Required]
        public Guid ApartmentId { get; set; }

        public Guid CompanyId { get; set; }

        public string Address { get; set; } = string.Empty;

        public int Floor { get; set; }

        public int Rooms { get; set; }

        public decimal RentPerMonth { get; set; }

        public DateTime LeaseStartDate { get; set; }

        public DateTime LeaseEndDate { get; set; }

        public bool IsOccupied { get; set; }

        public Company Company { get; set; } = null!;
    }
}
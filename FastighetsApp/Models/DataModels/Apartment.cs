// <copyright file="Apartment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an apartment unit belonging to a company.
    /// </summary>
    public class Apartment
    {
        /// <summary>
        /// Gets or sets the apartment identifier.
        /// </summary>
        [Required]
        public Guid ApartmentId { get; set; }

        /// <summary>
        /// Gets or sets the owning company identifier.
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the street address.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the floor number.
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Gets or sets number of rooms.
        /// </summary>
        public int Rooms { get; set; }

        /// <summary>
        /// Gets or sets the rent per month in SEK.
        /// </summary>
        public decimal RentPerMonth { get; set; }

        /// <summary>
        /// Gets or sets the lease start date (UTC).
        /// </summary>
        public DateTime LeaseStartDate { get; set; }

        /// <summary>
        /// Gets or sets the lease end date (UTC).
        /// </summary>
        public DateTime LeaseEndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the apartment is currently occupied.
        /// </summary>
        public bool IsOccupied { get; set; }

        /// <summary>
        /// Gets or sets the navigation to the owning company.
        /// </summary>
        public Company Company { get; set; } = null!;
    }
}
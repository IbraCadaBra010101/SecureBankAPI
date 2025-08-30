// <copyright file="Company.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.DataModels
{ 
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a real estate company that owns apartments.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company display name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the organization number.
        /// </summary>
        public string OrganizationNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the apartments owned by this company.
        /// </summary>
        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
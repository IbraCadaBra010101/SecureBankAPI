// <copyright file="Company.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.DataModels
{
    using System;
    using System.Collections.Generic;

    public class Company
    {
    
        public Guid CompanyId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string OrganizationNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
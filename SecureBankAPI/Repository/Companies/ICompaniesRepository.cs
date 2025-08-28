// <copyright file="ICompaniesRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RealEstateAPI.Models;

    /// <summary>
    /// Repository for company data access.
    /// </summary>
    public interface ICompaniesRepository
    {
        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>List of companies.</returns>
        Task<IEnumerable<Company>> GetCompaniesAsync();

        /// <summary>
        /// Gets a company by id.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <returns>Company or null.</returns>
        Task<Company?> GetCompanyByIdAsync(Guid companyId);
    }
} 
// <copyright file="IRealEstateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Services.RealEstate
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RealEstateAPI.Models;

    /// <summary>
    /// Domain service for real estate operations.
    /// </summary>
    public interface IRealEstateService
    {
        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>List of companies.</returns>
        Task<IEnumerable<Company>> GetCompaniesAsync();

        /// <summary>
        /// Gets all apartments for a company.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <returns>List of apartments.</returns>
        Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId);

        /// <summary>
        /// Gets apartments with leases expiring within the specified time window.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <param name="within">Time window.</param>
        /// <returns>List of apartments with expiring leases.</returns>
        Task<IEnumerable<Apartment>> GetContractsExpiringWithinAsync(Guid companyId, TimeSpan within);

        /// <summary>
        /// Updates attributes on a given apartment using the provided update action.
        /// </summary>
        /// <param name="apartmentId">Apartment identifier.</param>
        /// <param name="updateAction">Mutation to apply to the apartment entity.</param>
        /// <returns>A task representing the async operation.</returns>
        Task UpdateApartmentAttributeAsync(Guid apartmentId, Action<Apartment> updateAction);
    }
} 
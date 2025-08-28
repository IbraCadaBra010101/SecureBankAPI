// <copyright file="IApartmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Apartments
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RealEstateAPI.Models;

    /// <summary>
    /// Repository for apartment data access.
    /// </summary>
    public interface IApartmentsRepository
    {
        /// <summary>
        /// Gets all apartments for a company.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <returns>List of apartments.</returns>
        Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId);

        /// <summary>
        /// Gets apartments with lease end date before a specified date.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <param name="beforeDate">Upper bound date (inclusive).</param>
        /// <returns>List of apartments.</returns>
        Task<IEnumerable<Apartment>> GetExpiringLeasesByCompanyAsync(Guid companyId, DateTime beforeDate);

        /// <summary>
        /// Gets an apartment by id.
        /// </summary>
        /// <param name="apartmentId">Apartment identifier.</param>
        /// <returns>Apartment or null.</returns>
        Task<Apartment?> GetByIdAsync(Guid apartmentId);

        /// <summary>
        /// Updates an apartment entity.
        /// </summary>
        /// <param name="apartment">Apartment to update.</param>
        /// <returns>A task representing the async operation.</returns>
        Task UpdateAsync(Apartment apartment);
    }
} 
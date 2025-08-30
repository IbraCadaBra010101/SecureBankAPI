// <copyright file="IRealEstateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Services.ApartmentService
{
    using FastighetsAPI.Models.DataModels;

    /// <summary>
    /// Service interface for real estate operations.
    /// </summary>
    public interface IApartmentService
    {
        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>Collection of all companies.</returns>
        Task<IEnumerable<Company>> GetCompaniesAsync();

        /// <summary>
        /// Gets an apartment by its unique identifier.
        /// </summary>
        /// <param name="apartmentId">The apartment identifier.</param>
        /// <returns>The apartment if found, null otherwise.</returns>
        Task<Apartment?> GetApartmentByIdAsync(Guid apartmentId);

        /// <summary>
        /// Gets all apartments for a specific company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of apartments for the company.</returns>
        Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId);

        /// <summary>
        /// Gets apartments with expiring contracts within a specified time period.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="timeSpan">The time period to check for expiring contracts.</param>
        /// <returns>List of apartments with expiring contracts.</returns>
        Task<IEnumerable<Apartment>> GetApartmentsWithExpiringContractsAsync(Guid companyId, TimeSpan timeSpan);
    }
}
// <copyright file="IApartmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Repository.Apartments
{
    using FastighetsAPI.Models.DataModels;

    /// <summary>
    /// Repository interface for apartment data operations.
    /// </summary>
    public interface IApartmentsRepository
    {
        /// <summary>
        /// Gets all apartments.
        /// </summary>
        /// <returns>Collection of all apartments.</returns>
        Task<IEnumerable<Apartment>> GetAllApartmentsAsync();

        /// <summary> 
        /// Gets an apartment by its unique identifier.
        /// </summary>
        /// <param name="id">The apartment identifier.</param>
        /// <returns>The apartment if found, null otherwise.</returns>
        Task<Apartment?> GetApartmentByIdAsync(Guid id);

        /// <summary>
        /// Gets all apartments for a specific company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>Collection of apartments for the company.</returns>
        Task<IEnumerable<Apartment>> GetByCompanyIdAsync(Guid companyId);

        /// <summary>
        /// Saves to the DB and returns an int signalling succesfull save 0 for false and 1 for true.
        /// </summary>
        /// <returns>The number of affected entries.</returns>
        Task<int> SaveChangesAsync();
    }
}
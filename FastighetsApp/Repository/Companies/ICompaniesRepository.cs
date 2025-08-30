// <copyright file="ICompaniesRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Repository.Companies
{
    using FastighetsAPI.Models.DataModels;

    /// <summary>
    /// Repository interface for company data operations.
    /// </summary>
    public interface ICompaniesRepository
    {
        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>Collection of all companies.</returns>
        Task<IEnumerable<Company>> GetAllAsync();

        /// <summary>
        /// Gets a company by its unique identifier.
        /// </summary>
        /// <param name="id">The company identifier.</param>
        /// <returns>The company if found, null otherwise.</returns>
        Task<Company?> GetByIdAsync(Guid id);

        /// <summary>
        /// Saves all pending changes to the database.
        /// </summary>
        /// <returns>The number of affected entries.</returns>
        Task<int> SaveChangesAsync();
    }
}
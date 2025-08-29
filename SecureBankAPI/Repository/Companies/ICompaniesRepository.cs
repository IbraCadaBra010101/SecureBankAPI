// <copyright file="ICompaniesRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Companies;

using RealEstateAPI.Models;

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
    /// Adds a new company.
    /// </summary>
    /// <param name="company">The company to add.</param>
    /// <returns>The added company.</returns>
    Task<Company> AddAsync(Company company);

    /// <summary>
    /// Updates an existing company.
    /// </summary>
    /// <param name="company">The company to update.</param>
    /// <returns>The updated company.</returns>
    Task<Company> UpdateAsync(Company company);

    /// <summary>
    /// Deletes a company.
    /// </summary>
    /// <param name="id">The identifier of the company to delete.</param>
    /// <returns>True if deleted, false otherwise.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Saves all pending changes to the database.
    /// </summary>
    /// <returns>The number of affected entries.</returns>
    Task<int> SaveChangesAsync();
}
// <copyright file="IApartmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Apartments;

using RealEstateAPI.Models;

/// <summary>
/// Repository interface for apartment data operations.
/// </summary>
public interface IApartmentsRepository
{
    /// <summary>
    /// Gets all apartments.
    /// </summary>
    /// <returns>Collection of all apartments.</returns>
    Task<IEnumerable<Apartment>> GetAllAsync();

    /// <summary>
    /// Gets an apartment by its unique identifier.
    /// </summary>
    /// <param name="id">The apartment identifier.</param>
    /// <returns>The apartment if found, null otherwise.</returns>
    Task<Apartment?> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets all apartments for a specific company.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <returns>Collection of apartments for the company.</returns>
    Task<IEnumerable<Apartment>> GetByCompanyIdAsync(Guid companyId);

    /// <summary>
    /// Adds a new apartment.
    /// </summary>
    /// <param name="apartment">The apartment to add.</param>
    /// <returns>The added apartment.</returns>
    Task<Apartment> AddAsync(Apartment apartment);

    /// <summary>
    /// Updates an existing apartment.
    /// </summary>
    /// <param name="apartment">The apartment to update.</param>
    /// <returns>The updated apartment.</returns>
    Task<Apartment> UpdateAsync(Apartment apartment);

    /// <summary>
    /// Deletes an apartment.
    /// </summary>
    /// <param name="id">The identifier of the apartment to delete.</param>
    /// <returns>True if deleted, false otherwise.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Saves all pending changes to the database.
    /// </summary>
    /// <returns>The number of affected entries.</returns>
    Task<int> SaveChangesAsync();
}
// <copyright file="IRealEstateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Services.RealEstate;

using RealEstateAPI.Models;

/// <summary>
/// Service interface for real estate operations.
/// </summary>
public interface IRealEstateService
{
    /// <summary>
    /// Gets all companies.
    /// </summary>
    /// <returns>Collection of all companies.</returns>
    Task<IEnumerable<Company>> GetCompaniesAsync();

    /// <summary>
    /// Gets a company by its unique identifier.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <returns>The company if found, null otherwise.</returns>
    Task<Company?> GetCompanyByIdAsync(Guid companyId);
}
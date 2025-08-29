// <copyright file="IApartmentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Services.Apartment;

using RealEstateAPI.Models;

/// <summary>
/// Service interface for apartment-related operations including webhook updates.
/// </summary>
public interface IApartmentService
{
    /// <summary>
    /// Updates apartment attributes via webhook with comprehensive validation and error handling.
    /// </summary>
    /// <param name="updateDto">The webhook payload containing apartment updates.</param>
    /// <returns>A detailed result indicating success or failure with specific error details.</returns>
    Task<WebhookUpdateResult> UpdateApartmentAttributesAsync(ApartmentAttributeUpdateDto updateDto);

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

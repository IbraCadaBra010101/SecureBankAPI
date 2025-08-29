// <copyright file="ApartmentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Services.Apartment;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RealEstateAPI.Models;
using RealEstateAPI.Repository.Apartments;
using RealEstateAPI.Repository.Companies;

/// <summary>
/// Service implementation for apartment-related operations including webhook updates.
/// </summary>
public class ApartmentService : IApartmentService
{
    private readonly ILogger<ApartmentService> logger;
    private readonly IApartmentsRepository apartmentsRepository;
    private readonly ICompaniesRepository companiesRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApartmentService"/> class.
    /// </summary>
    /// <param name="logger">Application logger.</param>
    /// <param name="apartmentsRepository">Repository for apartment operations.</param>
    /// <param name="companiesRepository">Repository for company operations.</param>
    public ApartmentService(
        ILogger<ApartmentService> logger,
        IApartmentsRepository apartmentsRepository,
        ICompaniesRepository companiesRepository)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.apartmentsRepository = apartmentsRepository ?? throw new ArgumentNullException(nameof(apartmentsRepository));
        this.companiesRepository = companiesRepository ?? throw new ArgumentNullException(nameof(companiesRepository));
    }

    /// <inheritdoc/>
    public async Task<Apartment?> GetApartmentByIdAsync(Guid apartmentId)
    {
        if (apartmentId == Guid.Empty)
        {
            throw new ArgumentException("Apartment ID cannot be empty", nameof(apartmentId));
        }

        return await this.apartmentsRepository.GetByIdAsync(apartmentId);
    }

    /// <inheritdoc/>
    public async Task<WebhookUpdateResult> UpdateApartmentAttributesAsync(ApartmentAttributeUpdateDto updateDto)
    {
        if (updateDto == null)
        {
            this.logger.LogError("Webhook received null payload");
            return WebhookUpdateResult.CreateSystemError(
                Guid.Empty,
                null,
                "Webhook payload is null",
                "NULL_PAYLOAD");
        }

        var validationResult = this.ValidateWebhookPayloadAsync(updateDto);

        if (!validationResult.IsValid)
        {
            return WebhookUpdateResult.CreateValidationError(
                updateDto.ApartmentId,
                updateDto.SourceId,
                validationResult.Errors,
                "VALIDATION_FAILED");
        }

        try
        {

            var apartment = await this.apartmentsRepository.GetByIdAsync(updateDto.ApartmentId).ConfigureAwait(false);

            if (apartment == null)
            {
                this.logger.LogWarning("Apartment {ApartmentId} not found for webhook update", updateDto.ApartmentId);

                return WebhookUpdateResult.CreateNotFound(updateDto.ApartmentId, updateDto.SourceId);
            }

            this.UpdateApartmentAttribute(apartment, updateDto);

            await this.apartmentsRepository.SaveChangesAsync();

            return WebhookUpdateResult.CreateSuccess(updateDto.ApartmentId, updateDto.SourceId);
        }
        catch (Exception ex)
        {
            return WebhookUpdateResult.CreateSystemError(
                updateDto.ApartmentId,
                updateDto.SourceId,
                $"Internal server error: {ex.Message}",
                "INTERNAL_ERROR");
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId)
    {
        if (companyId == Guid.Empty)
        {
            throw new ArgumentException("Company ID cannot be empty", nameof(companyId));
        }

        return await this.apartmentsRepository.GetByCompanyIdAsync(companyId);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Apartment>> GetApartmentsWithExpiringContractsAsync(Guid companyId, TimeSpan timeSpan)
    {
        if (companyId == Guid.Empty)
        {
            throw new ArgumentException("Company ID cannot be empty", nameof(companyId));
        }

        if (timeSpan <= TimeSpan.Zero)
        {
            throw new ArgumentException("Time span must be positive", nameof(timeSpan));
        }

        var apartments = await this.apartmentsRepository.GetByCompanyIdAsync(companyId);
        var cutoffDate = DateTime.UtcNow.Add(timeSpan);

        return apartments.Where(a => a.LeaseEndDate <= cutoffDate);
    }

    private ValidationResult ValidateWebhookPayloadAsync(ApartmentAttributeUpdateDto updateDto)
    {
        var errors = new List<ValidationError>();

        if (updateDto.ApartmentId == Guid.Empty)
        {
            errors.Add(new ValidationError
            {
                Field = "apartmentId",
                Message = "Apartment ID is required and cannot be empty",
                InvalidValue = updateDto.ApartmentId,
                ValidationRule = "Required",
                Context = "Apartment ID must be a valid GUID",
            });
        }

        if (updateDto.RentPerMonth.HasValue)
        {
            if (updateDto.RentPerMonth.Value < 0)
            {
                errors.Add(new ValidationError
                {
                    Field = "rentPerMonth",
                    Message = "Rent per month cannot be negative",
                    InvalidValue = updateDto.RentPerMonth.Value,
                    ValidationRule = "MinValue",
                    Context = "Rent must be a positive number",
                });
            }

            if (updateDto.RentPerMonth.Value > 1000000)
            {
                errors.Add(new ValidationError
                {
                    Field = "rentPerMonth",
                    Message = "Rent per month exceeds maximum allowed value of 1,000,000 SEK",
                    InvalidValue = updateDto.RentPerMonth.Value,
                    ValidationRule = "MaxValue",
                    Context = "Rent must be reasonable for the market",
                });
            }
        }

        return new ValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors,
        };
    }

    private void UpdateApartmentAttribute(Apartment apartment, ApartmentAttributeUpdateDto updateDto)
    {
        if (updateDto.IsOccupied.HasValue)
        {
            apartment.IsOccupied = updateDto.IsOccupied.Value;
        }

        if (updateDto.RentPerMonth.HasValue)
        {
            apartment.RentPerMonth = updateDto.RentPerMonth.Value;
        }
    }
}

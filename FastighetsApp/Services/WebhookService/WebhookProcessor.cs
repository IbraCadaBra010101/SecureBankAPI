// <copyright file="ApartmentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Services.WebhookService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FastighetsAPI.Models.DataModels;
    using FastighetsAPI.Models.DTO;
    using FastighetsAPI.Models.Validation;
    using FastighetsAPI.Models.WebhookModels;
    using FastighetsAPI.Repository.Apartments;
    using FastighetsAPI.Repository.Companies;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service for apartment-related operations including webhook updates.
    /// </summary>
    public class WebhookProcessor : IWebhookProcessor
    {
        private readonly ILogger<WebhookProcessor> logger;
        private readonly IApartmentsRepository apartmentsRepository;
        private readonly ICompaniesRepository companiesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookProcessor"/> class.
        /// </summary>
        /// <param name="logger">Application logger.</param>
        /// <param name="apartmentsRepository">Repository for apartment operations.</param>
        /// <param name="companiesRepository">Repository for company operations.</param>
        public WebhookProcessor(
            ILogger<WebhookProcessor> logger,
            IApartmentsRepository apartmentsRepository,
            ICompaniesRepository companiesRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.apartmentsRepository = apartmentsRepository ?? throw new ArgumentNullException(nameof(apartmentsRepository));
            this.companiesRepository = companiesRepository ?? throw new ArgumentNullException(nameof(companiesRepository));
        }

        /// <inheritdoc/>
        public async Task<WebhookUpdateResult> UpdateApartmentAttributesAsync(ApartmentAttributeUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                this.logger.LogError("Webhook received null payload");
                return WebhookUpdateResult.CreateSystemError(
                    Guid.Empty,
                    "Webhook payload is null",
                    "NULL_PAYLOAD",
                    500);
            }

            var validationResult = this.ValidateWebhookPayloadAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return WebhookUpdateResult.CreateValidationError(
                    updateDto.ApartmentId,
                    validationResult.Errors,
                    "VALIDATION_FAILED");
            }

            try
            {
                var apartment = await this.apartmentsRepository.GetApartmentByIdForUpdateAsync(updateDto.ApartmentId);

                if (apartment == null)
                {
                    this.logger.LogWarning("Apartment {ApartmentId} not found for webhook update", updateDto.ApartmentId);

                    return WebhookUpdateResult.CreateNotFound(updateDto.ApartmentId);
                }

                this.UpdateApartmentAttribute(apartment, updateDto);

                var result = await this.apartmentsRepository.UpdateApartmentAsync(apartment);

                if (result > 0)
                {
                    this.logger.LogInformation("Successfully updated apartment {ApartmentId} via webhook", updateDto.ApartmentId);
                    return WebhookUpdateResult.CreateSuccess(updateDto.ApartmentId);
                }
                else
                {
                    this.logger.LogWarning("No changes were saved for apartment {ApartmentId}", updateDto.ApartmentId);
                    return WebhookUpdateResult.CreateSystemError(
                        updateDto.ApartmentId,
                        "No changes were saved to the database",
                        "SAVE_FAILED",
                        500);
                }
            }
            catch (Exception ex)
            {
                return WebhookUpdateResult.CreateSystemError(
                    updateDto.ApartmentId,
                    $"Internal server error: {ex.Message}",
                    "INTERNAL_ERROR",
                    500);
            }
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
            var hasChanges = false;

            if (updateDto.RentPerMonth.HasValue && apartment.RentPerMonth != updateDto.RentPerMonth.Value)
            {
                apartment.RentPerMonth = updateDto.RentPerMonth.Value;
                hasChanges = true;
                this.logger.LogDebug("Updated rent per month to {RentPerMonth} for apartment {ApartmentId}", 
                    updateDto.RentPerMonth.Value, apartment.ApartmentId);
            }

            if (updateDto.IsOccupied.HasValue && apartment.IsOccupied != updateDto.IsOccupied.Value)
            {
                apartment.IsOccupied = updateDto.IsOccupied.Value;
                hasChanges = true;
                this.logger.LogDebug("Updated occupancy status to {IsOccupied} for apartment {ApartmentId}", 
                    updateDto.IsOccupied.Value, apartment.ApartmentId);
            }

            if (hasChanges)
            {
                this.logger.LogInformation("Updated apartment {ApartmentId} with {ChangeCount} changes", 
                    apartment.ApartmentId, hasChanges ? 1 : 0);
            }
            else
            {
                this.logger.LogDebug("No changes detected for apartment {ApartmentId}", apartment.ApartmentId);
            }
        }
    }
}

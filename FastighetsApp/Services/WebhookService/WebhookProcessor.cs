// <copyright file="ApartmentService.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
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
    using Microsoft.Extensions.Logging;

    public class WebhookProcessor : IWebhookProcessor
    {
        private readonly ILogger<WebhookProcessor> logger;
        private readonly IApartmentsRepository apartmentsRepository;

        /// <exception cref="ArgumentNullException">Argument null exception.</exception>
        public WebhookProcessor(ILogger<WebhookProcessor> logger, IApartmentsRepository apartmentsRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.apartmentsRepository = apartmentsRepository ?? throw new ArgumentNullException(nameof(apartmentsRepository));
        }

        public async Task<WebhookUpdateResult> UpdateApartmentAttributesAsync(ApartmentAttributeUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                this.logger.LogError("Webhook received null payload");

                return WebhookUpdateResult.CreateSystemError(Guid.Empty, "Webhook payload is null", "NULL_PAYLOAD", 500);
            }

            var validationResult = this.ValidateWebhookPayload(updateDto);

            if (!validationResult.IsValid)
            {
                return WebhookUpdateResult.CreateValidationError(updateDto.ApartmentId, validationResult.Errors, "VALIDATION_FAILED");
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

        private ValidationResult ValidateWebhookPayload(ApartmentAttributeUpdateDto updateDto)
        {
            // Define a list of "rules". Each rule is a function that takes the incoming DTO
            // (ApartmentAttributeUpdateDto) and either returns a ValidationError if the rule fails,
            // or null if the rule passes.
            var rules = new List<Func<ApartmentAttributeUpdateDto, ValidationError?>>
    {
        // Rule 1: ApartmentId must not be empty
        // "dto =>" declares a lambda (an inline function).
        // It takes a parameter dto (of type ApartmentAttributeUpdateDto).
        // Then we use the ternary operator (condition ? valueIfTrue : valueIfFalse).
        dto => dto.ApartmentId == Guid.Empty ? new ValidationError
        {
            Field = "apartmentId", // the name of the field being validated
            Message = "Apartment ID is required and cannot be empty", // user-friendly error
            InvalidValue = dto.ApartmentId, // the actual bad value
            ValidationRule = "Required", // which rule failed
            Context = "Apartment ID must be a valid GUID", // extra context about the rule
        }
        : null, // if dto.ApartmentId is not empty, return null (meaning no error)

        // Rule 2: RentPerMonth cannot be negative
        // "dto.RentPerMonth is { } rent" means: if RentPerMonth has a value,
        // then assign that value into the variable "rent". (Pattern matching)
        // Then check "rent < 0".
        dto => dto.RentPerMonth is { } rent && rent < 0
        ? new ValidationError
        {
            Field = "rentPerMonth",
            Message = "Rent per month cannot be negative",
            InvalidValue = rent, // using the extracted "rent" variable here
            ValidationRule = "MinValue",
            Context = "Rent must be a positive number",
        }
        : null, // if RentPerMonth is not negative, return null

        // Rule 3: RentPerMonth cannot exceed 1,000,000
        // Same syntax as above, but now checking rent > 1,000,000.
        dto => dto.RentPerMonth is { } rent && rent > 1_000_000 ? new ValidationError
        {
            Field = "rentPerMonth",
            Message = "Rent per month exceeds maximum allowed value of 1,000,000 SEK",
            InvalidValue = rent,
            ValidationRule = "MaxValue",
            Context = "Rent must be reasonable for the market",
        }
        : null,
    };

            // Apply all rules:
            // 1. For each rule in the list, call rule(updateDto).
            //    -> This produces either a ValidationError or null.
            // 2. Keep only the non-null results (failed validations).
            // 3. Cast them to ValidationError (since we've filtered out nulls).
            // 4. Collect them into a List<ValidationError>.
            var errors = rules
                .Select(rule => rule(updateDto))
                .Where(error => error is not null)
                .Cast<ValidationError>()
                .ToList();

            // Build and return a ValidationResult object:
            // - IsValid is true if no errors were found.
            // - Errors contains the list of all validation errors.
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

                this.logger.LogDebug("Updated rent per month to {RentPerMonth} for apartment {ApartmentId}", updateDto.RentPerMonth.Value, apartment.ApartmentId);
            }

            if (updateDto.IsOccupied.HasValue && apartment.IsOccupied != updateDto.IsOccupied.Value)
            {
                apartment.IsOccupied = updateDto.IsOccupied.Value;

                hasChanges = true;

                this.logger.LogDebug("Updated occupancy status to {IsOccupied} for apartment {ApartmentId}", updateDto.IsOccupied.Value, apartment.ApartmentId);
            }

            if (hasChanges)
            {
                this.logger.LogInformation("Updated apartment {ApartmentId} with changes", apartment.ApartmentId);
            }
            else
            {
                this.logger.LogDebug("No changes detected for apartment {ApartmentId}", apartment.ApartmentId);
            }
        }
    }
}

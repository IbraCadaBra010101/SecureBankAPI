// <copyright file="ApartmentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Services.WebhookService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using FastighetsAPI.Models.DataModels;
    using FastighetsAPI.Models.DTO;
    using FastighetsAPI.Models.Validation;
    using FastighetsAPI.Models.WebhookModels;
    using FastighetsAPI.Repository.Apartments;
    using FastighetsAPI.Repository.Companies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Servicee for apartment-related operations including webhook updatesb.
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
                    null,
                    "Webhook payload is null",
                    "NULL_PAYLOAD",
                    500);
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
                var apartment = await this.apartmentsRepository.GetApartmentByIdAsync(updateDto.ApartmentId);

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

        /// <inheritdoc/>
        public async Task<bool> ValidateWebhookSignatureAsync(HttpRequest request)
        {
            try
            {
                // Check if the signature header exists - this is important for säkerhet!
                if (!request.Headers.TryGetValue("X-Signature", out var signatureHeader))
                {
                    this.logger.LogWarning("Webhook request missing X-Signature header");
                    return false;
                }

                var signature = signatureHeader.ToString();
                if (string.IsNullOrEmpty(signature))
                {
                    this.logger.LogWarning("Webhook request has empty X-Signature header");
                    return false;
                }

                // Read the request body - we need this for signature validation
                request.EnableBuffering();
                request.Body.Position = 0;
                
                using var reader = new StreamReader(request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;

                // For test purposes, using a hardcoded secret
                // In production, this should come from configuration or secure storage
                var sharedSecret = "test-webhook-secret-key-2024";

                // Compute HMAC-SHA256 signature using the shared hemlighet (secret)
                var computedSignature = ComputeHmacSignature(body, sharedSecret);

                // Compare signatures to see if they match
                var isValid = string.Equals(signature, computedSignature, StringComparison.OrdinalIgnoreCase);

                if (isValid)
                {
                    this.logger.LogDebug("Webhook signature validation successful");
                }
                else
                {
                    this.logger.LogWarning("Webhook signature validation failed. Expected: {Expected}, Received: {Received}", 
                        computedSignature, signature);
                }

                return isValid;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error during webhook signature validation");
                return false;
            }
        }

        /// <summary>
        /// Computes HMAC-SHA256 signature for the given payload using the shared hemlighet (secret).
        /// This method creates a cryptographic signature that can be used to verify the authenticity of webhook requests.
        /// </summary>
        /// <param name="payload">The payload to sign. Cannot be null.</param>
        /// <param name="secret">The shared secret key used for signing.</param>
        /// <returns>The computed signature in hexadecimal format.</returns>
        private static string ComputeHmacSignature(string payload, string secret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToHexString(hash).ToLowerInvariant();
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
}

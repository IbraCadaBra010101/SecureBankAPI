// <copyright file="IWebhookProcessor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Services.WebhookService
{
    using FastighetsAPI.Models.DTO;
    using FastighetsAPI.Models.WebhookModels;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Service interface for apartment-related operations including webhook updates.
    /// This service handles all the webhook processing for lägenheter (apartments) updates.
    /// </summary>
    public interface IWebhookProcessor
    { 
        /// <summary>
        /// Updates apartment attributes via webhook with comprehensive validation and error handling.
        /// This method processes incoming webhook requests and updates the apartment data accordingly.
        /// </summary>
        /// <param name="updateDto">The webhook payload containing apartment updates. Cannot be null.</param>
        /// <returns>A detailed result indicating success or failure with specific error details.</returns>
        Task<WebhookUpdateResult> UpdateApartmentAttributesAsync(ApartmentAttributeUpdateDto updateDto);

        /// <summary>
        /// Validates the webhook request using HMAC signature for säkerhet (security).
        /// This method checks if the incoming webhook request is authentic and hasn't been tampered with.
        /// </summary>
        /// <param name="request">The HTTP request to validate. Must not be null.</param>
        /// <returns>True if the webhook is valid and authentic, false otherwise.</returns>
        Task<bool> ValidateWebhookSignatureAsync(HttpRequest request);
    }
}

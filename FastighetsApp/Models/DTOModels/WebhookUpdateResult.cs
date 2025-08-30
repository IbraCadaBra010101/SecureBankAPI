// <copyright file="WebhookUpdateResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.WebhookModels
{
    using FastighetsAPI.Models.Validation;

    /// <summary>
    /// Comprehensive result model for webhook operations with detailed success/failure information.
    /// </summary>
    public class WebhookUpdateResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether gets or sets whether the webhook operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the main message describing the result.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the apartment ID that was processed.
        /// </summary>
        public Guid ApartmentId { get; set; }

        /// <summary>
        /// Gets or sets when the webhook was processed.
        /// </summary>
        public DateTime ProcessedAt { get; set; }



        /// <summary>
        /// Gets or sets the HTTP status code that should be returned.
        /// </summary>
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets detailed error information if the operation failed.
        /// </summary>
        public List<ValidationError>? ValidationErrors { get; set; }

        /// <summary>
        /// Gets or sets the specific error code for programmatic handling.
        /// </summary>
        public string? ErrorCode { get; set; }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        /// <param name="apartmentId">The apartment ID that was updated.</param>
        /// <returns>A successful webhook result.</returns>
        public static WebhookUpdateResult CreateSuccess(Guid apartmentId)
        {
            return new WebhookUpdateResult
            {
                Success = true,
                Message = "Apartment attributes updated successfully",
                ApartmentId = apartmentId,
                ProcessedAt = DateTime.UtcNow,
                HttpStatusCode = 200,
            };
        }

        /// <summary>
        /// Creates a validation error result.
        /// </summary>
        /// <param name="apartmentId">The apartment ID that was attempted to update.</param>
        /// <param name="validationErrors">List of validation errors.</param>
        /// <param name="errorCode">The error code.</param>
        /// <returns>A validation error result.</returns>
        public static WebhookUpdateResult CreateValidationError(Guid apartmentId, List<ValidationError> validationErrors, string errorCode)
        {
            return new WebhookUpdateResult
            {
                Success = false,
                Message = "Validation errors occurred while processing webhook",
                ApartmentId = apartmentId,
                ProcessedAt = DateTime.UtcNow,
                HttpStatusCode = 400,
                ValidationErrors = validationErrors,
                ErrorCode = errorCode,
            };
        }

        /// <summary>
        /// Creates a not found result.
        /// </summary>
        /// <param name="apartmentId">The apartment ID that was not found.</param>
        /// <returns>A not found result.</returns>
        public static WebhookUpdateResult CreateNotFound(Guid apartmentId)
        {
            return new WebhookUpdateResult
            {
                Success = false,
                Message = $"Apartment {apartmentId} not found",
                ApartmentId = apartmentId,
                ProcessedAt = DateTime.UtcNow,
                HttpStatusCode = 404,
                ErrorCode = "APARTMENT_NOT_FOUND",
            };
        }

        /// <summary>
        /// Creates a system error result.
        /// </summary>
        /// <param name="apartmentId">The apartment ID that was being processed.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns>A system error result.</returns>
        public static WebhookUpdateResult CreateSystemError(Guid apartmentId, string errorMessage, string errorCode, int statusCode)
        {
            return new WebhookUpdateResult
            {
                Success = false,
                Message = errorMessage,
                ApartmentId = apartmentId,
                ProcessedAt = DateTime.UtcNow,
                HttpStatusCode = statusCode,
                ErrorCode = errorCode,
            };
        }
    }
}
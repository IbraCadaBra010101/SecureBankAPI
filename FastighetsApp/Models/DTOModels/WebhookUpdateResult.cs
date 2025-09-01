// <copyright file="WebhookUpdateResult.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.WebhookModels
{
    using FastighetsAPI.Models.Validation;

    public class WebhookUpdateResult
    {
    
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public Guid ApartmentId { get; set; }

        public DateTime ProcessedAt { get; set; }

        public int HttpStatusCode { get; set; }

        public List<ValidationError>? ValidationErrors { get; set; }

        public string? ErrorCode { get; set; }

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
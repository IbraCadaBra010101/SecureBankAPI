// <copyright file="WebhookController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Controllers;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstateAPI.Models;
using RealEstateAPI.Services.Apartment;

/// <summary>
/// Controller for handling webhook operations from external systems.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WebhookController : ControllerBase
{
    private readonly ILogger<WebhookController> logger;
    private readonly IApartmentService apartmentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebhookController"/> class.
    /// </summary>
    /// <param name="logger">Application logger.</param>
    /// <param name="apartmentService">Service for apartment operations.</param>
    public WebhookController(ILogger<WebhookController> logger, IApartmentService apartmentService)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.apartmentService = apartmentService ?? throw new ArgumentNullException(nameof(apartmentService));
    }

    /// <summary>
    /// Webhook endpoint to update apartment attributes from external systems.
    /// </summary>
    /// <param name="payload">The webhook payload containing apartment updates.</param>
    /// <returns>
    /// HTTP 200 with detailed success information on success;
    /// HTTP 400 with validation errors if payload is invalid;
    /// HTTP 404 if apartment not found;
    /// HTTP 500 for internal server errors.
    /// </returns>
    /// <remarks>
    /// This endpoint allows external systems to update apartment attributes in real-time.
    /// All updates are validated and logged for audit purposes.
    /// Example payload:
    /// <code>
    /// {
    ///   "apartmentId": "123e4567-e89b-12d3-a456-426614174000",
    ///   "isOccupied": true,
    ///   "rentPerMonth": 15000.00,
    ///   "sourceId": "property-mgmt-system-001"
    /// }
    /// </code>
    /// </remarks>
    [HttpPost("apartment-attribute")]
    public async Task<ActionResult<WebhookUpdateResult>> UpdateApartmentAttribute([FromBody] ApartmentAttributeUpdateDto payload)
    {
        if (payload == null)
        {
            this.logger.LogWarning("Webhook received null payload");

            var nullPayloadResult = WebhookUpdateResult.CreateSystemError(
                Guid.Empty,
                null,
                "Webhook payload is null or empty",
                "NULL_PAYLOAD");

            return this.StatusCode(nullPayloadResult.HttpStatusCode, nullPayloadResult);
        }

        this.logger.LogInformation(
            "Webhook received for apartment {ApartmentId} from source {SourceId}",
            payload.ApartmentId,
            payload.SourceId ?? "Unknown");

        try
        {
            var result = await this.apartmentService.UpdateApartmentAttributesAsync(payload);

            return this.StatusCode(result.HttpStatusCode, result);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Unexpected error processing webhook for apartment {ApartmentId}", payload.ApartmentId);

            var systemErrorResult = WebhookUpdateResult.CreateSystemError(
                payload.ApartmentId,
                payload.SourceId,
                $"Unexpected system error: {ex.Message}",
                "UNEXPECTED_ERROR");

            return this.StatusCode(systemErrorResult.HttpStatusCode, systemErrorResult);
        }
    }
}

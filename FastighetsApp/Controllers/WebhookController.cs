// <copyright file="WebhookController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FastighetsAPI.Models.DTO;
    using FastighetsAPI.Models.WebhookModels;
    using FastighetsAPI.Services.WebhookService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Controller for a exposed webhook for external systems.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> logger;
        private readonly IWebhookProcessor webhookProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookController"/> class.
        /// </summary>
        /// <param name="logger">Application logger.</param>
        /// <param name="webhookProcessor">Service for webhook operations.</param>
        public WebhookController(ILogger<WebhookController> logger, IWebhookProcessor webhookProcessor)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.webhookProcessor = webhookProcessor ?? throw new ArgumentNullException(nameof(webhookProcessor));
        }

        /// <summary>
        /// Webhook endpoint to update the apartment attributes from external systems.
        /// This endpoint is exposed for external systems to send updates about lägenheter (apartments).
        /// </summary>
        /// <param name="payload">The webhook payload containing apartment updates. Must not be null.</param>
        /// <returns>
        /// HTTP Responses with detailed information about the processing result.
        /// </returns>
        [HttpPost("apartment-attribute")]
        public async Task<ActionResult<WebhookUpdateResult>> UpdateApartmentAttribute([FromBody] ApartmentAttributeUpdateDto payload)
        {
            // Validate webhook signature first - this is important for säkerhet (security)!
            if (!await this.webhookProcessor.ValidateWebhookSignatureAsync(this.Request))
            {
                this.logger.LogWarning("Webhook request failed signature validation");
                var unauthorizedResult = WebhookUpdateResult.CreateSystemError(
                    Guid.Empty, 
                    null, 
                    "Webhook signature validation failed", 
                    "UNAUTHORIZED", 
                    401);
                return this.StatusCode(unauthorizedResult.HttpStatusCode, unauthorizedResult);
            }

            if (payload == null)
            {
                this.logger.LogWarning("Webhook received null or invalid payload");

                var nullPayloadResult = WebhookUpdateResult.CreateSystemError(Guid.Empty, null, "Webhook payload is null or ApartmentId is missing/empty", "INVALID_PAYLOAD", 400);

                return this.StatusCode(nullPayloadResult.HttpStatusCode, nullPayloadResult);
            }

            if (payload.ApartmentId == Guid.Empty)
            {
                this.logger.LogWarning("Webhook received payload with empty ApartmentId");
                var emptyIdResult = WebhookUpdateResult.CreateSystemError(
                    Guid.Empty, 
                    payload.SourceId, 
                    "ApartmentId cannot be empty", 
                    "INVALID_APARTMENT_ID", 
                    400);
                return this.StatusCode(emptyIdResult.HttpStatusCode, emptyIdResult);
            }

            this.logger.LogInformation(
                "Webhook received for apartment {ApartmentId}", payload.ApartmentId);

            try
            {
                var result = await this.webhookProcessor.UpdateApartmentAttributesAsync(payload);

                if (result.Success)
                {
                    this.logger.LogInformation("Webhook processed successfully for apartment {ApartmentId}", payload.ApartmentId);
                }
                else
                {
                    this.logger.LogWarning("Webhook processing failed for apartment {ApartmentId}: {Error}", 
                        payload.ApartmentId, result.Message);
                }

                return this.StatusCode(result.HttpStatusCode, result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Unexpected error processing webhook for apartment {ApartmentId}", payload.ApartmentId);

                var systemErrorResult = WebhookUpdateResult.CreateSystemError(payload.ApartmentId, payload.SourceId, $"Unexpected system error: {ex.Message}", "UNEXPECTED_ERROR", 500);

                return this.StatusCode(systemErrorResult.HttpStatusCode, systemErrorResult);
            }
        }

        /// <summary>
        /// Gets the shared secret for webhook testing purposes.
        /// Note: This endpoint should be removed in production - it's only for testing!
        /// The secret is used for HMAC signature validation of webhook requests.
        /// </summary>
        /// <returns>The shared secret key and other useful information for testing.</returns>
        [HttpGet("secret")]
        public ActionResult<string> GetSharedSecret()
        {
            // This endpoint is for testing purposes only
            // In production, this should be removed or secured with additional authentication
            return this.Ok(new { 
                message = "Shared secret for testing webhook signatures",
                secret = "test-webhook-secret-key-2024",
                algorithm = "HMAC-SHA256",
                header = "X-Signature"
            });
        }
    }
}

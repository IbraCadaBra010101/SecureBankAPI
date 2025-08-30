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
    /// Controller for exposed webhook endpoints - external systems can call these
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
            if (payload == null)
            {
                this.logger.LogWarning("Webhook got null payload - not good");

                var nullPayloadResult = WebhookUpdateResult.CreateSystemError(Guid.Empty, "Webhook payload is null or ApartmentId is missing/empty", "INVALID_PAYLOAD", 400);

                return this.StatusCode(nullPayloadResult.HttpStatusCode, nullPayloadResult);
            }

            if (payload.ApartmentId == Guid.Empty)
            {
                this.logger.LogWarning("Webhook received payload with empty ApartmentId");
                var emptyIdResult = WebhookUpdateResult.CreateSystemError(
                    Guid.Empty, 
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
                    this.logger.LogWarning("Webhook processing failed for apartment {ApartmentId}: {Error}", payload.ApartmentId, result.Message);
                }

                return this.StatusCode(result.HttpStatusCode, result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Unexpected error processing webhook for apartment {ApartmentId}", payload.ApartmentId);

                var systemErrorResult = WebhookUpdateResult.CreateSystemError(payload.ApartmentId, $"Unexpected system error: {ex.Message}", "UNEXPECTED_ERROR", 500);

                return this.StatusCode(systemErrorResult.HttpStatusCode, systemErrorResult);
            }
        }


    }
}

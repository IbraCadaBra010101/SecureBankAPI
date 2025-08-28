// <copyright file="RealEstateController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RealEstateAPI.Models;
    using RealEstateAPI.Services.RealEstate;

    /// <summary>
    /// REST endpoints for real estate operations: companies, apartments and webhooks.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RealEstateController : ControllerBase
    {
        private readonly ILogger<RealEstateController> logger;
        private readonly IRealEstateService realEstateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateController"/> class.
        /// </summary>
        /// <param name="logger">Application logger.</param>
        /// <param name="realEstateService">Domain service for real estate operations.</param>
        public RealEstateController(ILogger<RealEstateController> logger, IRealEstateService realEstateService)
        {
            this.logger = logger;
            this.realEstateService = realEstateService;
        }

        /// <summary>
        /// Returns all companies.
        /// </summary>
        /// <returns>List of companies.</returns>
        [HttpGet("companies")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesAsync()
        {
            var companies = await this.realEstateService.GetCompaniesAsync();
            return this.Ok(companies);
        }

        /// <summary>
        /// Returns all apartments for a specific company.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <returns>List of apartments for the company.</returns>
        [HttpGet("companies/{companyId}/apartments")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartmentsByCompanyAsync(Guid companyId)
        {
            var apartments = await this.realEstateService.GetApartmentsByCompanyAsync(companyId);
            return this.Ok(apartments);
        }

        /// <summary>
        /// Returns apartments under a company with leases expiring within the given number of months.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <param name="months">Threshold in months (defaults to 3).</param>
        /// <returns>List of apartments with expiring leases.</returns>
        [HttpGet("companies/{companyId}/contracts/expiring")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetContractsExpiringAsync(Guid companyId, int months = 3)
        {
            if (months <= 0)
            {
                return this.BadRequest("Months must be greater than zero");
            }

            var apartments = await this.realEstateService.GetContractsExpiringWithinAsync(companyId, TimeSpan.FromDays(30 * months));
            return this.Ok(apartments);
        }

        /// <summary>
        /// Webhook endpoint to update apartment attributes (e.g., occupancy, rent per month).
        /// </summary>
        /// <param name="payload">Update payload.</param>
        /// <returns>HTTP 200 on success; 404 if apartment not found; 400 for invalid payload.</returns>
        [HttpPost("webhooks/apartment-attribute")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateApartmentAttribute([FromBody] ApartmentAttributeUpdateDto payload)
        {
            if (payload == null || payload.ApartmentId == Guid.Empty)
            {
                return this.BadRequest("Invalid payload");
            }

            try
            {
                await this.realEstateService.UpdateApartmentAttributeAsync(payload.ApartmentId, apartment =>
                {
                    if (payload.IsOccupied.HasValue)
                    {
                        apartment.IsOccupied = payload.IsOccupied.Value;
                    }

                    if (payload.RentPerMonth.HasValue)
                    {
                        apartment.RentPerMonth = payload.RentPerMonth.Value;
                    }
                });

                return this.Ok();
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Webhook payload for updating apartment attributes.
        /// </summary>
        public class ApartmentAttributeUpdateDto
        {
            /// <summary>
            /// Gets or sets the target apartment identifier.
            /// </summary>
            public Guid ApartmentId { get; set; }

            /// <summary>
            /// Gets or sets the occupancy flag to update.
            /// </summary>
            public bool? IsOccupied { get; set; }

            /// <summary>
            /// Gets or sets the rent per month to update.
            /// </summary>
            public decimal? RentPerMonth { get; set; }
        }
    }
}
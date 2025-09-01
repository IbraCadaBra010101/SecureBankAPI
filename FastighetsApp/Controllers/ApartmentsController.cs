// <copyright file="ApartmentsController.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FastighetsAPI.Models.DataModels;
    using FastighetsAPI.Services.ApartmentService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentsController : ControllerBase
    {
        private readonly ILogger<ApartmentsController> logger;
        private readonly IApartmentService apartmentService;

        public ApartmentsController(
            ILogger<ApartmentsController> logger, IApartmentService apartmentService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.apartmentService = apartmentService ?? throw new ArgumentNullException(nameof(apartmentService));
        }

        [HttpGet("companies")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesAsync()
        {
            try
            {
                var companies = await this.apartmentService.GetCompaniesAsync();

                return companies == null ? this.NotFound() : this.Ok(companies);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving companies");

                return this.StatusCode(500, "Something went wrong while getting companies");
            }
        }

        [HttpGet("companies/{companyId}/apartments")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartmentsByCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                return this.BadRequest("Company ID cannot be empty");
            }

            try
            {
                var apartments = await this.apartmentService.GetApartmentsByCompanyAsync(companyId);

                if (apartments == null || !apartments.Any())
                {
                    return this.NotFound($"No apartments found for company {companyId}");
                }

                return this.Ok(apartments);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, "Invalid company ID provided: {CompanyId}", companyId);

                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving apartments for company {CompanyId}", companyId);

                return this.StatusCode(500, "Internal server error occurred while retrieving apartments");
            }
        }

        [HttpGet("companies/{companyId}/contracts/expiring")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetContractsExpiringAsync(Guid companyId, int months = 3)
        {
            if (companyId == Guid.Empty)
            {
                return this.BadRequest("Company ID cannot be empty");
            }

            try
            {
                var apartments = await this.apartmentService.GetApartmentsWithExpiringContractsAsync(companyId, TimeSpan.FromDays(30 * months));

                return this.Ok(apartments ?? Enumerable.Empty<Apartment>());
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, "Invalid parameters provided: CompanyId={CompanyId}, Months={Months}", companyId, months);
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving expiring contracts for company {CompanyId}", companyId);
                return this.StatusCode(500, "Internal server error occurred while retrieving expiring contracts");
            }
        }
    }
}
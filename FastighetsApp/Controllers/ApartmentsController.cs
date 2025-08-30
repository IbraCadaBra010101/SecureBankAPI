// <copyright file="RealEstateController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
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

    /// <summary>
    ///eendpoints for real estate operation.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentsController : ControllerBase
    {
        private readonly ILogger<ApartmentsController> logger;
        private readonly IApartmentService apartmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentsController"/> class.
        /// </summary>
        /// <param name="logger">Application logger.</param>
        /// <param name="apartmentService">Service for apartment operations.</param>
        public ApartmentsController(
            ILogger<ApartmentsController> logger, IApartmentService apartmentService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.apartmentService = apartmentService ?? throw new ArgumentNullException(nameof(apartmentService));
        }

        /// <summary>
        /// Returns companies/fastighetsbolag.
        /// </summary>
        /// <returns>List of companies.</returns>
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

                return this.StatusCode(500, "An error occurred while retrieving companies");
            }
        }

        /// <summary>
        /// Returns apartmentsf for a specific company.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <returns>List of apartments for the company.</returns>
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

        /// <summary>
        /// Returns apartments for a company with contracts expiring within the given number of months.
        /// defaults to 3 months.
        /// </summary>
        /// <param name="companyId">Company identifier.</param>
        /// <param name="months">Threshold in months (defaults to 3).</param>
        /// <returns>List of apartments with expiring leases.</returns>
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
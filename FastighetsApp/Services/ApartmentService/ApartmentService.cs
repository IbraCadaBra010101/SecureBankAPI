// <copyright file="RealEstateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Services.ApartmentService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FastighetsAPI.Models.DataModels;
    using FastighetsAPI.Repository.Apartments;
    using FastighetsAPI.Repository.Companies;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service implementation for apartment operations - handles the business logic
    /// </summary>
    public class ApartmentService : IApartmentService
    {
        private readonly ILogger<ApartmentService> logger;
        private readonly ICompaniesRepository companiesRepository;
        private readonly IApartmentsRepository apartmentsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentService"/> class.
        /// </summary>
        /// <param name="logger">Application logger.</param>
        /// <param name="companiesRepository">Repository for company repo.</param>
        /// <param name="apartmentsRepository">Repository for apartment repo.</param>
        public ApartmentService(ILogger<ApartmentService> logger, ICompaniesRepository companiesRepository, IApartmentsRepository apartmentsRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.companiesRepository = companiesRepository ?? throw new ArgumentNullException(nameof(companiesRepository));

            this.apartmentsRepository = apartmentsRepository ?? throw new ArgumentNullException(nameof(apartmentsRepository));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            try
            {
                this.logger.LogDebug("Retrieving all companies");

                var companies = await this.companiesRepository.GetAllAsync();

                this.logger.LogInformation("Retrieved {Count} companies", companies.Count());

                return companies;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error retrieving companies");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Apartment?> GetApartmentByIdAsync(Guid apartmentId)
        {
            if (apartmentId == Guid.Empty)
            {
                throw new ArgumentException("Apartment ID cannot be empty", nameof(apartmentId));
            }

            return await this.apartmentsRepository.GetApartmentByIdAsync(apartmentId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentException("Company ID cannot be empty", nameof(companyId));
            }

            return await this.apartmentsRepository.GetByCompanyIdAsync(companyId);
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
    }
}
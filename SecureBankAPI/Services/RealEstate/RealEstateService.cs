// <copyright file="RealEstateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Services.RealEstate
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RealEstateAPI.Models;
    using RealEstateAPI.Repository.Apartments;
    using RealEstateAPI.Repository.Companies;

    /// <summary>
    /// Default implementation of <see cref="IRealEstateService"/>.
    /// </summary>
    public class RealEstateService : IRealEstateService
    {
        private readonly ICompaniesRepository companiesRepository;
        private readonly IApartmentsRepository apartmentsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateService"/> class.
        /// </summary>
        /// <param name="companiesRepository">Companies repository.</param>
        /// <param name="apartmentsRepository">Apartments repository.</param>
        public RealEstateService(ICompaniesRepository companiesRepository, IApartmentsRepository apartmentsRepository)
        {
            this.companiesRepository = companiesRepository;
            this.apartmentsRepository = apartmentsRepository;
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return this.companiesRepository.GetCompaniesAsync();
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId)
        {
            return this.apartmentsRepository.GetApartmentsByCompanyAsync(companyId);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Apartment>> GetContractsExpiringWithinAsync(Guid companyId, TimeSpan within)
        {
            var beforeDate = DateTime.UtcNow.Add(within);
            return this.apartmentsRepository.GetExpiringLeasesByCompanyAsync(companyId, beforeDate);
        }

        /// <inheritdoc/>
        public async Task UpdateApartmentAttributeAsync(Guid apartmentId, Action<Apartment> updateAction)
        {
            var apartment = await this.apartmentsRepository.GetByIdAsync(apartmentId);
            if (apartment == null)
            {
                throw new KeyNotFoundException("Apartment not found");
            }

            updateAction(apartment);
            await this.apartmentsRepository.UpdateAsync(apartment);
        }
    }
} 
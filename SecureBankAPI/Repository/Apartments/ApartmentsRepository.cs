// <copyright file="ApartmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Apartments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using RealEstateAPI.Data;
    using RealEstateAPI.Models;

    /// <summary>
    /// EF Core implementation of <see cref="IApartmentsRepository"/>.
    /// </summary>
    public class ApartmentsRepository : IApartmentsRepository
    {
        private readonly RealEstateDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentsRepository"/> class.
        /// </summary>
        /// <param name="dbContext">EF DbContext.</param>
        public ApartmentsRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId)
        {
            return await this.dbContext.Apartments
                .AsNoTracking()
                .Where(a => a.CompanyId == companyId)
                .OrderBy(a => a.Address)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Apartment>> GetExpiringLeasesByCompanyAsync(Guid companyId, DateTime beforeDate)
        {
            return await this.dbContext.Apartments
                .AsNoTracking()
                .Where(a => a.CompanyId == companyId && a.LeaseEndDate <= beforeDate)
                .OrderBy(a => a.LeaseEndDate)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Apartment?> GetByIdAsync(Guid apartmentId)
        {
            return await this.dbContext.Apartments.FirstOrDefaultAsync(a => a.ApartmentId == apartmentId);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Apartment apartment)
        {
            this.dbContext.Apartments.Update(apartment);
            await this.dbContext.SaveChangesAsync();
        }
    }
} 
// <copyright file="ApartmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Repository.Apartments
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FastighetsAPI.Models.DataModels;
    using Microsoft.EntityFrameworkCore;
    using FastighetsAPI.Data;

    /// <summary>
    /// Repository for apartments.
    /// </summary>
    public class ApartmentsRepository : IApartmentsRepository
    {
        private readonly RealEstateDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentsRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ApartmentsRepository(RealEstateDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Apartment>> GetAllApartmentsAsync()
        {
            return await this.context.Apartments
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/> 
        public async Task<Apartment?> GetApartmentByIdAsync(Guid id)
        {
            return await this.context.Apartments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ApartmentId == id);
        }

        /// <inheritdoc/> 
        public async Task<Apartment?> GetApartmentByIdForUpdateAsync(Guid id)
        {
            return await this.context.Apartments
                .FirstOrDefaultAsync(a => a.ApartmentId == id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Apartment>> GetByCompanyIdAsync(Guid companyId)
        {
            return await this.context.Apartments
                .AsNoTracking()
                .Where(a => a.CompanyId == companyId)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateApartmentAsync(Apartment apartment)
        {
            this.context.Apartments.Update(apartment);

            return await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
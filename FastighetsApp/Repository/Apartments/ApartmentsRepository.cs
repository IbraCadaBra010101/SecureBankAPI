// <copyright file="ApartmentsRepository.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Repository.Apartments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FastighetsAPI.Data;
    using FastighetsAPI.Models.DataModels;
    using Microsoft.EntityFrameworkCore;

    public class ApartmentsRepository : IApartmentsRepository
    {
        private readonly RealEstateDbContext context;

        public ApartmentsRepository(RealEstateDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Apartment>> GetAllApartmentsAsync()
        {
            return await this.context.Apartments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Apartment?> GetApartmentByIdAsync(Guid id)
        {
            return await this.context.Apartments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ApartmentId == id);
        }

        public async Task<Apartment?> GetApartmentByIdForUpdateAsync(Guid id)
        {
            return await this.context.Apartments
                .FirstOrDefaultAsync(a => a.ApartmentId == id);
        }

        public async Task<IEnumerable<Apartment>> GetByCompanyIdAsync(Guid companyId)
        {
            return await this.context.Apartments
                .AsNoTracking()
                .Where(a => a.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<int> UpdateApartmentAsync(Apartment apartment)
        {
            this.context.Apartments.Update(apartment);

            return await this.context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
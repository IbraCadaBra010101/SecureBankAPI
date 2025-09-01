// <copyright file="CompaniesRepository.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Repository.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FastighetsAPI.Models.DataModels;
    using Microsoft.EntityFrameworkCore;
    using FastighetsAPI.Data;
    using FastighetsAPI.Repository.Companies;

    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly RealEstateDbContext context;

        public CompaniesRepository(RealEstateDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await this.context.Companies
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            return await this.context.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
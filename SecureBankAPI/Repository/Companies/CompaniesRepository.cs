// <copyright file="CompaniesRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using RealEstateAPI.Data;
    using RealEstateAPI.Models;

    /// <summary>
    /// EF Core implementation of <see cref="ICompaniesRepository"/>.
    /// </summary>
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly RealEstateDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesRepository"/> class.
        /// </summary>
        /// <param name="dbContext">EF DbContext.</param>
        public CompaniesRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await this.dbContext.Companies
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Company?> GetCompanyByIdAsync(Guid companyId)
        {
            return await this.dbContext.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
        }
    }
}
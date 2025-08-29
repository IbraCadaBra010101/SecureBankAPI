// <copyright file="CompaniesRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Companies;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models;

/// <summary>
/// Repository implementation for company data operations.
/// </summary>
public class CompaniesRepository : ICompaniesRepository
{
    private readonly RealEstateDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompaniesRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CompaniesRepository(RealEstateDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await this.context.Companies
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Company?> GetByIdAsync(Guid id)
    {
        return await this.context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyId == id);
    }

    /// <inheritdoc/>
    public async Task<Company> AddAsync(Company company)
    {
        this.context.Companies.Add(company);
        await this.context.SaveChangesAsync();
        return company;
    }

    /// <inheritdoc/>
    public async Task<Company> UpdateAsync(Company company)
    {
        this.context.Companies.Update(company);
        await this.context.SaveChangesAsync();
        return company;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var company = await this.GetByIdAsync(id);
        if (company == null)
        {
            return false;
        }

        this.context.Companies.Remove(company);
        await this.context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync()
    {
        return await this.context.SaveChangesAsync();
    }
}
// <copyright file="ApartmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Repository.Apartments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models;

/// <summary>
/// Repository implementation for apartment data operations.
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
    public async Task<IEnumerable<Apartment>> GetAllAsync()
    {
        return await this.context.Apartments
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Apartment?> GetByIdAsync(Guid id)
    {
        return await this.context.Apartments
            .AsNoTracking()
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
    public async Task<Apartment> AddAsync(Apartment apartment)
    {
        this.context.Apartments.Add(apartment);
        await this.context.SaveChangesAsync();
        return apartment;
    }

    /// <inheritdoc/>
    public async Task<Apartment> UpdateAsync(Apartment apartment)
    {
        this.context.Apartments.Update(apartment);
        await this.context.SaveChangesAsync();
        return apartment;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var apartment = await this.GetByIdAsync(id);
        if (apartment == null)
        {
            return false;
        }

        this.context.Apartments.Remove(apartment);
        await this.context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync()
    {
        return await this.context.SaveChangesAsync();
    }
}
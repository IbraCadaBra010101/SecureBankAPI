// <copyright file="RealEstateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Services.RealEstate;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RealEstateAPI.Models;
using RealEstateAPI.Repository.Companies;

/// <summary>
/// Service implementation for real estate operations.
/// </summary>
public class RealEstateService : IRealEstateService
{
    private readonly ILogger<RealEstateService> logger;
    private readonly ICompaniesRepository companiesRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RealEstateService"/> class.
    /// </summary>
    /// <param name="logger">Application logger.</param>
    /// <param name="companiesRepository">Repository for company operations.</param>
    public RealEstateService(ILogger<RealEstateService> logger, ICompaniesRepository companiesRepository)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.companiesRepository = companiesRepository ?? throw new ArgumentNullException(nameof(companiesRepository));
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
    public async Task<Company?> GetCompanyByIdAsync(Guid companyId)
    {
        if (companyId == Guid.Empty)
        {
            throw new ArgumentException("Company ID cannot be empty", nameof(companyId));
        }

        try
        {
            this.logger.LogDebug("Retrieving company with ID: {CompanyId}", companyId);
            var company = await this.companiesRepository.GetByIdAsync(companyId);
            
            if (company == null)
            {
                this.logger.LogWarning("Company with ID {CompanyId} not found", companyId);
            }
            else
            {
                this.logger.LogInformation("Retrieved company: {CompanyName}", company.Name);
            }
            
            return company;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error retrieving company with ID: {CompanyId}", companyId);
            throw;
        }
    }
} 